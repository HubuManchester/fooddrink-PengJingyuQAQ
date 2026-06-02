# FoodDrinkApp (Nutritional Guide)

A modern, cross-platform .NET MAUI application built on **.NET 8.0**. Designed to track food and beverage nutritional information, this project features real-time cloud data synchronization, complex UI state management, and deep integration with native mobile hardware APIs. Developed by PengJingyu.

## 🚀 Key Features

- **Cloud-Driven Catalog:** Real-time food list fetched asynchronously from a MockAPI RESTful backend, ensuring complete separation of concerns between front-end UI and back-end data.
- **Dynamic Theming:** Built-in dynamic background color and theme switching capabilities for enhanced user accessibility.
- **Detailed Nutritional Views:** Seamless navigation to a dedicated **Food Detail Page** displaying high-resolution imagery, detailed macro-nutrients, and allergy warnings.
- **Native Hardware Integration:** - **Text-to-Speech (TTS) & Haptics:** Audibly reads out nutritional summaries while simultaneously triggering device vibrations for a multi-sensory feedback experience.
  - **Flashlight & Camera:** Direct invocation of the device's native camera and LED flashlight modules (gracefully handled via native APIs).
  - **Geolocation:** Captures exact device GPS coordinates to track dining or purchase locations.
- **Rigorous Data Validation:** Comprehensive entry forms with strict numeric and required-field validations before cloud synchronization.

## 🏆 Architecture & Code Quality Standards

- **.NET 8.0 & MVVM Pattern:** Utilizing the latest long-term support framework with a strict Model-View-ViewModel architectural design.
- **Code Analyzers:** The codebase is heavily strictly monitored by **`CommunityToolkit.Maui.Analyzers`**, ensuring zero-warning compliance with industry-standard MVVM bindings, asynchronous programming, and dependency injection patterns.
- **Pure English Codebase:** All source code, comments, and internal documentation have been refactored to professional technical English.

## 🛠️ How to Run

Open `FoodDrinkApp.csproj` or `FoodDrinkApp.sln` using Visual Studio 2022 with the `.NET MAUI` workload installed.

**Recommended Target Emulators:**
- Android Emulator (Pixel series, API 33+ recommended)
- Windows Machine (Local)

**Build Command for Windows (.NET 8.0):**
```powershell
dotnet build .\FoodDrinkApp.csproj -f net8.0-windows10.0.19041.0
```

**Build Command for Android (.NET 8.0):**
```powershell
dotnet build .\FoodDrinkApp.csproj -f net8.0-android
```

*Note: This project utilizes `Directory.Build.props` to redirect the build output to `C:\MauiBuild\NutriTrack\` to avoid path-length and localization issues with Android packaging tools on Windows.*

## 🎬 Demonstration Highlights

1. **Theme Switching:** Demonstrate the responsive background color change upon app launch.
2. **Cloud & Detail Page:** Show the cloud-synced list and navigate into the Food Detail Page.
3. **Hardware & Haptics:** Demonstrate the synchronized Text-to-Speech and device vibration feature.
4. **API Proof (Flashlight):** Since emulators lack physical LEDs, refer to the source code to showcase the `Flashlight.TurnOnAsync()` implementation.
5. **Tablet Optimization:** Display the application running smoothly on a Tablet emulator to highlight cross-platform responsive design.
6. **Code Standards:** Showcase the integration of `CommunityToolkit.Maui.Analyzers` ensuring robust, production-ready code.
