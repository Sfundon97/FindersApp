# FindersApp

**FindersApp** is a full-stack solution featuring an **ASP.NET Core Web Application** backend alongside a **Flutter mobile client** integration. It provides identity management, structured data repository access, and Firebase authentication.

---

## 🚀 Key Features

* **ASP.NET Core MVC Backend**: Built with C#, implementing repository patterns, controllers, models, and Entity Framework Core migrations.
* **Identity & Authentication**: Integrated with ASP.NET Core Identity and Firebase Auth (`firebase-auth.js`, `google-services.json`).
* **Mobile Support**: Flutter/Dart integration (located in the `finders/` module) for cross-platform client functionality.
* **Database & Data Layer**: Features structured repositories, interfaces, pagination support (`PaginatedList.cs`), and database migrations.

---

## 🛠️ Tech Stack

* **Backend**: .NET / C#, ASP.NET Core MVC, Entity Framework Core
* **Frontend (Web)**: HTML5, CSS3, Razor Views
* **Mobile**: Flutter / Dart
* **Authentication**: ASP.NET Core Identity, Firebase Authentication
* **DevOps**: Docker

---

## 📂 Project Structure

```text
FindersApp/
├── App_Data/                # Data storage / local DB files
├── Areas/Identity/          # ASP.NET Core Identity pages & flows
├── Controllers/             # MVC Controllers
├── Data/                    # DbContext & database configurations
├── Interfaces/              # Repository & service interfaces
├── Migrations/              # Entity Framework migrations
├── Models/                  # Application data models
├── Repositories/            # Data access repository implementations
├── Views/                   # Razor UI views
├── finders/                 # Mobile client project (Flutter/Dart)
├── wwwroot/                 # Static assets (JS, CSS, images)
├── Dockerfile               # Container build script
├── Finders.csproj           # C# project file
├── FindersApp.sln           # Visual Studio solution file
└── Program.cs               # Application entry point & service startup

⚙️ Getting Started

Prerequisites
.NET SDK 6.0+

Flutter SDK (if running the mobile client)

Firebase Firestore
