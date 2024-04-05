// Basic demo for accelerometer readings from Adafruit MPU6050

#include <Adafruit_MPU6050.h>
#include <Adafruit_Sensor.h>
#include <Mouse.h>
#include <Wire.h>
#include <math.h>

Adafruit_MPU6050 mpu;
sensors_event_t g, a, temp;
int x[2], y[2], index;
int j,k;

int XCoord = 0, YCoord = 0;
float ScreenX, ScreenY; //mouse.move works with screen being 1920x1080 even on 2560x1440 screens or larger as it does not use pixel coordinates

void setup(void) {
  Serial.begin(115200);
  while (!Serial) {
    delay(10); // will pause Zero, Leonardo, etc until serial console opens
  }

  // Try to initialize!
  if (!mpu.begin()) {
    Serial.println("Failed to find MPU6050 chip");
    while (1) {
      delay(10);
    }
  }

  mpu.setAccelerometerRange(MPU6050_RANGE_2_G);
  mpu.setGyroRange(MPU6050_RANGE_500_DEG);
  mpu.setFilterBandwidth(MPU6050_BAND_21_HZ);
  Serial.println("");
  delay(100);
}

void loop() {

  /* Get new sensor events with the readings */
  mpu.getEvent(&g, &a, &temp);

  ScreenX = constrain((1920 / 10.0) * constrain(g.gyro.y, -10, 10), -960, 960) + 960;
  ScreenY = constrain((1080 / 10.0) * constrain(g.gyro.z, -10, 10), -540, 540) + 540;

  
  
  while(j < 20) {
  Mouse.move(-100, -100, 0);
  j++;
  }
  
  while(k < 20) {
  Mouse.move((ScreenX / 20), (ScreenY / 20), 0);
  x[k % 2] = ScreenX;
  y[k % 2] = ScreenY;
  k++;
  }
  
  x[index % 2] = ScreenX;
  y[index % 2] = ScreenY;
  
  index++;

  Mouse.move((x[(index - 1) % 2] - x[index % 2]), (y[(index - 1) % 2] - y[index % 2]),0);

  Serial.print("X = ");
  Serial.print(ScreenX);
  Serial.print("\t");
  Serial.print("Y = ");
  Serial.println(ScreenY);

  //data();

  //Mouse.move(rounds(a.acceleration.z) * -1, rounds(a.acceleration.y) * -1 +1, 0);
  delay(10);
}


int rounds(float value) {
  return round(value * 10);
}

void data() {
  /* Print out the values */
  Serial.print("X = ");
  Serial.print(rounds(a.acceleration.x));
  Serial.print("\t");
  Serial.print("Y = ");
  Serial.print(rounds(a.acceleration.y));
  Serial.print("\t");
  Serial.print("Z = ");
  Serial.print(rounds(a.acceleration.z));
  Serial.print("\t");
  Serial.print("XG = ");
  Serial.print(constrain(g.gyro.x, -10, 10));
  Serial.print("\t");
  Serial.print("YG = ");
  Serial.print(constrain(g.gyro.y, -10, 10));
  Serial.print("\t");
  Serial.print("ZG = ");
  Serial.println(constrain(g.gyro.z, -10, 10));

}
