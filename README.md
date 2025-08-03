# CircuitPath

A simple, fast Revit add-in to bulk-set the **Circuit Path Mode** for all circuits in your project.  
**CircuitPath** is the quickest way to ensure every circuit’s path is set to "All Devices" with a single click.

---

## Features

- **One-click batch update**: Set Circuit Path Mode to "All Devices" for every circuit in the active Revit project.
- Integrates with the **RK Tools** ribbon tab for quick access.
- Lightweight – no unnecessary UI or configuration.
- Built using the [ricaun.Revit.UI](https://github.com/ricaun-io) library for modern Revit integration.

---

## Getting Started

### Prerequisites

- **Revit 2022+**
- [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)
- [ricaun.Revit.UI](https://github.com/ricaun-io)
  
### Installation

1. **Build the solution** in Visual Studio.
2. Copy the resulting `.dll` and `.addin` file to your Revit Addins folder:  
   `C:\ProgramData\Autodesk\Revit\Addins\2024\`
3. Launch Revit.  
   You’ll find **CircuitPath** in the **"Small Tools"** panel under the **"RK Tools"** ribbon tab.

---

## Usage

1. Click **"Set circuit path"** in the RK Tools > Small Tools panel.
2. All electrical circuits in your project will be updated to use **Path Mode: All Devices**.
3. A message will appear confirming the update.

---

## Developer Notes

- **No window or configuration**: Designed to be as fast and unobtrusive as possible.
- Uses the [Revit API](https://www.revitapidocs.com/2024/027da68a-fb94-2cb9-b1d9-78e0a9db565c.htm) to set `ElectricalCircuit.PathMode` for all circuits in the project.
- Integrated with the [ricaun.Revit.UI AppLoader](https://github.com/ricaun-io) for ribbon setup.

---

## License

MIT License.  
See [LICENSE](LICENSE) for details.

---

## Credits

- Ribbon/button management via [ricaun.Revit.UI](https://github.com/ricaun-io/Revit-UI)
