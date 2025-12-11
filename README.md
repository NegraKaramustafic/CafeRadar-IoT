**🌟 CafeRadar – IoT Ambience Monitoring System**

CafeRadar is a complete IoT system for monitoring noise and light levels in cafés, helping users find a place that matches their preferred atmosphere — a quiet study spot, a bright coffee place, or a lively night-out location.

The project demonstrates the full IoT workflow:
🟦 ESP32 microcontroller with sound & light sensors + OLED display
🟧 .NET Web API backend receiving and storing measurements
🟩 Angular frontend showing real-time café ambience
🔁 Two-way communication between frontend and ESP32

**🔧 Technologies Used**

🖥 Hardware / IoT

ESP32
KY-037 sound sensor
BH1750 light sensor
SSD1306 128×64 OLED

🟧 Backend

ASP.NET Core Web API
Entity Framework Core
SQL Server

🟩 Frontend

Angular
TypeScript
HTML / SCSS

**📂 Project Structure**
CafeRadar-IoT/
├── backend/        # ASP.NET Core Web API
├── frontend/       # Angular application
└── esp32/          # ESP32 Arduino source code

**⚙️ How It Works**
1️⃣ ESP32 → Backend

ESP32 reads sensor data every few seconds and sends JSON:

{
  "cafeId": 3,
  "noiseValue": 1820,
  "noiseLevel": "NORMAL",
  "lightValue": 630,
  "lightLevel": "BRIGHT"
}


Noise → QUIET / NORMAL / LOUD
Light → DARK / NORMAL / BRIGHT

2️⃣ Backend → Frontend

The backend returns:
café info (name, description, image, address)
latest measured noise/light values
Angular displays all cafés with their current ambience.

3️⃣ Frontend → ESP32 (Two-Way Communication)

When a user clicks “Display on Device”, frontend sends:

GET http://ESP_IP/show


ESP32 then:

reads sensors
formats a short message
displays it on the OLED

📟 Example:

Cafe 3 — quiet (38 dB, 150 lux)

**🎛 Frontend Features**

List of cafés
Real-time ambience
Last measured timestamp

Filters:

Study & Focus

Bright Coffee Spot

Night Out

Show All

Button for sending command to ESP32

**🚀 Running the Project**
Backend – .NET API
cd backend
dotnet restore
dotnet run


Set your connection string:

"DefaultConnection": "Server=YOUR_SERVER;Database=CafeRadarDB;Trusted_Connection=True;"

Frontend – Angular
cd frontend
npm install
ng serve


Set backend URL:

factory: () => "http://YOUR_BACKEND_URL";


Set ESP32 IP:

private espUrl = "http://YOUR_DEVICE_IP";

ESP32

Install Arduino libraries:

WiFi
HTTPClient
BH1750
Adafruit_GFX
Adafruit_SSD1306
WebServer

Set credentials:

const char* ssid      = "YOUR_WIFI_SSID";
const char* password  = "YOUR_WIFI_PASSWORD";


**📌 Data Disclaimer**

Names, descriptions and images of cafés are example demo data based on local cafés.
Used only for educational purposes.

**👩‍💻 Author**

Created as a student IoT project combining:

ESP32 + .NET API + Angular

Feel free to explore, fork, or extend (e.g., add temperature, CO₂, air quality sensors).

📄 License

This project is licensed under the MIT License.
