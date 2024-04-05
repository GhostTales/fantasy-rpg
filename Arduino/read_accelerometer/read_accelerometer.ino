// Basic demo for accelerometer readings from Adafruit MPU6050

#include <Adafruit_MPU6050.h>
#include <Adafruit_Sensor.h>
#include <Mouse.h>
#include <Wire.h>
#include <math.h>

Adafruit_MPU6050 mpu;
sensors_event_t g, a, temp;

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

  data();

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
