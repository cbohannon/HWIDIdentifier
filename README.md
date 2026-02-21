# HWIDIdentifier

A Windows desktop application for reading and spoofing hardware identifiers via the Windows Registry.

## Features

- **Read** current hardware identifiers:
  - HWID (Hardware Profile GUID)
  - PC GUID (Machine GUID)
  - PC Name (Computer Name)
  - Product ID
  - Windows Product Key
  - HDD serial numbers and drive info
- **Spoof** new randomized values for HWID, PC GUID, PC Name, and Product ID

## Requirements

- Windows 10 or later
- .NET Framework 4.8
- **Administrator privileges** required for spoof (write) operations

## Building

```bash
dotnet restore HWIDIdentifier.sln
dotnet build HWIDIdentifier.sln
```

Or open `HWIDIdentifier.sln` in Visual Studio 2019/2022 and build from there.

## Running

Launch `HWIDIdentifier.exe` from `HWIDIdentifier/bin/Debug/` or `bin/Release/`. Run as Administrator to enable spoof functionality.

## Testing

```bash
# Run all tests
dotnet test HWIDIdentifier.sln

# Unit tests only (no app required)
dotnet test HWIDTest/HWIDTest.csproj

# UI automation tests (requires compiled Debug build)
dotnet test FlaUITests/FlaUITests.csproj
```

Unit tests do not require Administrator privileges and do not modify real registry values — write tests use a temporary `HKCU\Software\HWIDIdentifierTest` key that is created and cleaned up automatically.

## Architecture

| Project | Framework | Purpose |
|---|---|---|
| `HWIDIdentifier/` | .NET Framework 4.8 | Main WPF application |
| `HWIDTest/` | .NET 6.0 | MSTest unit tests |
| `FlaUITests/` | .NET 6.0 | FlaUI UI automation tests |

Logs are written to `HWIDSpoofer.log` (rolling, max 10 MB) in the application directory.
