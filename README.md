# Chimera

Chimera is an open-source Windows application that translates input from a DualShock 4 controller into a virtual Xbox 360 controller.

The application communicates directly with the controller through the HID interface, reads its input reports in real time, converts them into a structured controller state, translates each PlayStation input to its Xbox equivalent, and updates a virtual Xbox 360 controller using ViGEmBus.

To prevent games from receiving input from both the physical and virtual controllers simultaneously, Chimera integrates with HidHide and automatically hides the physical controller while Play Mode is active.

---

## Current Features

- Partial DualShock 4 Bluetooth support
- Virtual Xbox 360 controller emulation
- Automatic HidHide integration

---

## Architecture

Chimera is divided into independent components, each responsible for a single stage of the input pipeline.

### HidScanner

Searches the system for compatible DualShock 4 controllers and retrieves the corresponding HID device.

### DualShockManager

Manages the controller connection during the application's lifetime. It initializes the controller, opens the HID stream, creates the input monitor, and exposes the active controller to every module.

### InputMonitor

Continuously reads HID reports from the controller and updates the current controller state.

### Parser

Interprets raw HID reports and converts them into a structured `DualShockState` containing buttons, triggers, D-Pad, and analog stick values.

### XboxTranslator

Maps the parsed PlayStation controller state into an equivalent Xbox controller state.

### XboxController

Communicates with ViGEmBus and updates the virtual Xbox 360 controller using the translated state.

### HidHideManager

Registers Chimera with HidHide, hides the physical controller during Play Mode, and restores it when the application exits.

---

## Input Pipeline

While Play Mode is running, Chimera performs the following sequence:

1. Detects a connected DualShock 4 controller.
2. Opens a HID communication channel.
3. Reads controller input reports continuously.
4. Parses each report into a structured controller state.
5. Translates PlayStation input into Xbox input.
6. Updates a virtual Xbox 360 controller through ViGEmBus.
7. Hides the physical controller using HidHide to prevent double input.

---

## Project Structure

```
Chimera
│
├── Models
│
├── Modules
│   ├── PlayMode
│   ├── RawAnalyzer
│   ├── InputAnalyzer
│   ├── BenchmarkAnalyzer
│   └── HidHideTest
│
├── Services
│   ├── Connection
│   ├── HidHide
│   ├── Input
│   ├── Translator
│   ├── VirtualController
│   └── Parser
│
└── Program.cs
```

---

## Requirements

Before running Chimera, install the following software:

- Windows 10 or Windows 11
- .NET 10 SDK
- ViGEmBus Driver
- HidHide Driver
- DualShock 4 connected through Bluetooth

---

## Dependencies

Chimera depends on the following open-source projects:

- ViGEmBus  
  https://github.com/nefarius/ViGEmBus

- HidHide  
  https://github.com/nefarius/HidHide

---

## Building

Clone the repository.

```bash
git clone https://github.com/<your-username>/Chimera.git

cd Chimera
```

Restore NuGet packages.

```bash
dotnet restore
```

Build the project.

```bash
dotnet build
```

Run Chimera.

```bash
dotnet run
```

---

## Usage

1. Connect the DualShock 4 through Bluetooth.
2. Install ViGEmBus.
3. Install HidHide.
4. Start Chimera.
5. Select **Play Mode**.
6. A virtual Xbox 360 controller will be created automatically.
7. Press **ESC** to stop Play Mode.

---

## Available Modules

### Play Mode

Reads the controller input, translates it into Xbox input, creates a virtual Xbox 360 controller, and automatically hides the physical controller using HidHide.

### Raw Analyzer

Displays raw HID reports and highlights modified bytes to assist with debugging and reverse engineering.

### Input Analyzer

Displays the parsed controller state, including buttons, D-Pad, triggers, and analog sticks.

### Benchmark Analyzer

Measures report processing performance and controller polling frequency.

### HidHide Test

Verifies HidHide integration independently from Play Mode.

---

## License

This project is licensed under the MIT License.