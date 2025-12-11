#include <WiFi.h>
#include <HTTPClient.h>
#include <BH1750.h>
#include <Adafruit_SSD1306.h>
#include <Adafruit_GFX.h>
#include <Wire.h>
#include <WebServer.h>

//  Pins & display config 
const int SOUND_PIN = 36;

const int SCREEN_WIDTH  = 128;
const int SCREEN_HEIGHT = 64;
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, -1);

BH1750 lightMeter;
WebServer server(80);

//  WiFi & API configuration (placeholders for GitHub) 
const char* ssid      = "YOUR_WIFI_SSID";
const char* password  = "YOUR_WIFI_PASSWORD";
const char* serverUrl = "http://YOUR_API_URL/api/measurements";

//  Measurement configuration 
const int CAFE_ID = 3;
const unsigned long INTERVAL = 5000;  // 5 seconds
unsigned long lastTime = 0;

// Forward declaration
void handleShow();

//  Helper functions for level classification 
String getNoiseLevel(int raw) {
  if (raw < 1400) return "QUIET";
  else if (raw < 2500) return "NORMAL";
  else return "LOUD";
}

String getLightLevel(int lux) {
  if (lux < 50) return "DARK";
  else if (lux < 500) return "NORMAL";
  else return "BRIGHT";
}

//  Helper to render status on OLED 
void showStatusOnDisplay(int noise, int light, const String& noiseLevel, const String& lightLevel) {
  display.clearDisplay();
  display.setTextSize(1);
  display.setTextColor(SSD1306_WHITE);

  int y = 0;
  display.setCursor(0, y);
  display.print("Cafe ");
  display.println(CAFE_ID);
  y += 10;

  display.setCursor(0, y);
  display.print("Noise: ");
  display.println(noiseLevel);
  y += 10;

  display.setCursor(0, y);
  display.print("Raw: ");
  display.println(noise);
  y += 10;

  display.setCursor(0, y);
  display.print("Light: ");
  display.println(lightLevel);
  y += 10;

  display.setCursor(0, y);
  display.print("Lux: ");
  display.println(light);

  display.display();
}

void setup() {
  Serial.begin(115200);

  pinMode(SOUND_PIN, INPUT);

  // I2C for BH1750 and OLED
  Wire.begin(21, 22);
  lightMeter.begin();

  Serial.println("Connecting to WiFi...");
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("\nWiFi connected!");
  Serial.print("ESP32 IP address: ");
  Serial.println(WiFi.localIP());

  if (!display.begin(SSD1306_SWITCHCAPVCC, 0x3C)) {
    Serial.println("OLED init failed!");
  } else {
    display.clearDisplay();
    display.setTextSize(1);
    display.setTextColor(SSD1306_WHITE);
    display.display();
  }

  // HTTP endpoint hosted on ESP32
  server.on("/show", handleShow);
  server.begin();
  Serial.println("HTTP server started on /show");
}

void loop() {
  // Handle incoming HTTP requests (e.g. from frontend)
  server.handleClient();

  // Periodic measurement + sending data to backend
  if (millis() - lastTime < INTERVAL) return;
  lastTime = millis();

  int noiseSum = 0;
  for (int i = 0; i < 10; i++) {
    noiseSum += analogRead(SOUND_PIN);
    delay(5);
  }
  int noise = noiseSum / 10;

  int light = (int)lightMeter.readLightLevel();

  String noiseLevel = getNoiseLevel(noise);
  String lightLevel = getLightLevel(light);

  // Build JSON payload
  String json = "{";
  json += "\"cafeId\":" + String(CAFE_ID) + ",";
  json += "\"noiseValue\":" + String(noise) + ",";
  json += "\"noiseLevel\":\"" + noiseLevel + "\",";
  json += "\"lightValue\":" + String(light) + ",";
  json += "\"lightLevel\":\"" + lightLevel + "\"";
  json += "}";

  HTTPClient http;
  http.begin(serverUrl);
  http.addHeader("Content-Type", "application/json");
  int status = http.POST(json);

  Serial.println("Sent payload: " + json);
  Serial.println("Response status: " + String(status));

  http.end();
}

//  HTTP handler for /show: display current values on OLED 
void handleShow() {
  int noiseSum = 0;
  for (int i = 0; i < 10; i++) {
    noiseSum += analogRead(SOUND_PIN);
    delay(5);
  }
  int noise = noiseSum / 10;
  int light = (int)lightMeter.readLightLevel();

  String noiseLevel = getNoiseLevel(noise);
  String lightLevel = getLightLevel(light);

  showStatusOnDisplay(noise, light, noiseLevel, lightLevel);

  server.send(200, "text/plain", "OK - displayed on OLED");
}
