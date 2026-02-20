# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

HWIDIdentifier is a Windows WPF desktop application (.NET Framework 4.8) that reads and spoofs hardware identifiers (HWID, PC GUID, PC Name, Product ID, Windows Product Key, HDD serial) via Windows Registry manipulation.

## Build Commands

```bash
# Build (solution)
dotnet build HWIDIdentifier.sln

# Build release
dotnet build HWIDIdentifier.sln -c Release

# Restore NuGet packages
dotnet restore HWIDIdentifier.sln
```

## Test Commands

```bash
# Run all tests
dotnet test HWIDIdentifier.sln

# Run only unit tests
dotnet test HWIDTest/HWIDTest.csproj

# Run only UI automation tests (requires app to be built; launches actual WPF app)
dotnet test FlaUITests/FlaUITests.csproj

# Run with detailed output
dotnet test HWIDIdentifier.sln --verbosity detailed

# Run with code coverage (configured via HWIDIdentifier.runsettings)
dotnet test HWIDIdentifier.sln --settings HWIDIdentifier.runsettings --collect:"XPlat Code Coverage"
```

## Architecture

### Projects

| Project | Framework | Purpose |
|---|---|---|
| `HWIDIdentifier/` | .NET Framework 4.8 (WinExe) | Main WPF application |
| `HWIDTest/` | .NET 6.0 | MSTest unit tests |
| `FlaUITests/` | .NET 6.0 | FlaUI UI automation tests |

### Core Classes (HWIDIdentifier/)

- **`ReadHelper.cs`** — Static nested classes to read hardware values from the Windows Registry (`ReadHelper.HWID`, `ReadHelper.PCGuid`, `ReadHelper.PCName`, `ReadHelper.ProductId`, `ReadHelper.GetWindowsProductKey()`)
- **`WriteHelper.cs`** — Static nested classes to generate/spoof new hardware values (`WriteHelper.HWID.SpoofHWID()`, `WriteHelper.PCGuid.SpoofPCGuid()`, etc.)
- **`GenericHelper.cs`** — Registry access wrapper (`GenericHelper.Regedit`) and random alphanumeric string generator (`GenericHelper.RandomGenerator`)
- **`MainWindow.xaml/.cs`** — WPF UI with buttons wired to Read/Write helpers; HDD enumeration uses `ManagementObjectSearcher`

### Registry Paths Used

```
HKLM\SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001  → HwProfileGuid (HWID)
HKLM\SOFTWARE\Microsoft\Cryptography                                       → MachineGuid (PC GUID)
HKLM\SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName     → ComputerName
HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion                          → ProductID, DigitalProductId
```

### Patterns

- Static nested classes used for all read/write operations (e.g., `ReadHelper.HWID.GetValue()`)
- Registry errors return error strings rather than throwing exceptions
- log4net is configured via `log4net.config` with a rolling file (`HWIDSpoofer.log`) and console appender
- UI automation tests (FlaUITests) launch the actual compiled WPF executable and use FlaUI UIA3

### Key Dependencies

- **log4net** (2.0.14) — Logging
- **FlaUI.Core / FlaUI.UIA3** (3.2.0) — UI automation for end-to-end tests
- **MSTest** — Unit and UI test framework
