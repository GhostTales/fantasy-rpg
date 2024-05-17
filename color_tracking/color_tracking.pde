import websockets.*;
import processing.video.*;

WebsocketServer ws;
Capture video;

color trackColor; 
float threshold = 15;

String packet;
boolean calibrating;

int[][] corners = {{50, 50}, {590, 50}, {50, 310}, {590, 310}};
int corner;


void setup() {
  size(640, 360);
  String[] cameras = Capture.list();
  printArray(cameras);
  video = new Capture(this, cameras[cameras.length-1]); //"pipeline:autovideosrc"
  video.start();
  trackColor = color(25, 200, 120);



  thread("Websocket");
}

void captureEvent(Capture video) {
  video.read();
}

void draw() {
  video.loadPixels();
  image(video, 0, 0);

  fill(0, 200, 0);

  calibration();

  //threshold = map(mouseX, 0, width, 0, 100);
  threshold = 15;

  float avgX = 0;
  float avgY = 0;

  int count = 0;

  // Begin loop to walk through every pixel
  for (int x = 0; x < video.width; x++ ) {
    for (int y = 0; y < video.height - 120; y++ ) {
      int loc = x + y * (video.width);
      // What is current color
      color currentColor = video.pixels[loc];
      float r1 = red(currentColor);
      float g1 = green(currentColor);
      float b1 = blue(currentColor);
      float r2 = red(trackColor);
      float g2 = green(trackColor);
      float b2 = blue(trackColor);

      float d = distSq(r1, g1, b1, r2, g2, b2); 

      if (d < threshold*threshold) {
        stroke(255);
        strokeWeight(1);
        point(x, y);
        avgX += x;
        avgY += y;
        count++;
      }
    }
  }

  // We only consider the color found if its color distance is less than 10. 
  // This threshold of 10 is arbitrary and you can adjust this number depending on how accurate you require the tracking to be.
  if (count > 0) { 
    avgX = avgX / count;
    avgY = avgY / count;
    // Draw a circle at the tracked pixel
    fill(255);
    strokeWeight(4.0);
    stroke(0);
    ellipse(avgX, avgY, 24, 24);

    if (avgX >= 0.0f || avgY >= 0.0f) {
      //ws.sendMessage(Float.toString(avgX)+","+Float.toString(avgY));
      String list = corners[0][0]+","+corners[0][1]+","+corners[1][0]+","+(corners[1][1])+","+(corners[2][0])+","+corners[2][1]+","+(corners[3][0])+","+ (corners[3][1]) ;
      packet = Float.toString(avgX)+","+Float.toString(avgY) + "," + list;
      //delay(25);
    }
  }
}

public float distSq(float x1, float y1, float z1, float x2, float y2, float z2) {
  float d = (x2-x1)*(x2-x1) + (y2-y1)*(y2-y1) +(z2-z1)*(z2-z1);
  return d;
}

void mousePressed() {
  if (calibrating != true) {
    // Save color where the mouse is clicked in trackColor variable
    int loc = mouseX + mouseY*video.width;
    trackColor = video.pixels[loc];
    println(red(trackColor) + ", " + green(trackColor) + ", " + blue(trackColor));
  }
}

void Websocket() {
  WebsocketServer.enableDebug();
  ws = new WebsocketServer(this, 8080, "/colorTracking");
}

void webSocketServerEvent(String msg) {
  println(msg + " => " + packet);
  if (packet != null) {
    ws.sendMessage(packet);
  }
}

public void webSocketConnectEvent(String uid, String ip) {
  println("Someone connected", uid, ip);
}

public void webSocketDisconnectEvent(String uid, String ip) {
  println("Someone disconnected", uid, ip);
}





public void calibration() {
  for (int i = 0; i < 4; i++) {
    circle(corners[i][0], corners[i][1], 10);

    if (dist(mouseX, mouseY, corners[i][0], corners[i][1]) <= 10) 
    {
      println("over cirvle: "+ i);
      corner = i;
      calibrating = true;
      if (mousePressed) {
        corners[i][0] = mouseX;
        corners[i][1] = mouseY;
        trackColor = color(25, 200, 120);
      }
    } else {
      calibrating = false;
    }
  }
}

public boolean overCircle(int x, int y, int diameter) {
  float disX = x - mouseX;
  float disY = y - mouseY;
  if (sqrt(sq(disX) + sq(disY)) < diameter/2 ) {
    return true;
  } else {
    return false;
  }
}
