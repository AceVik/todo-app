# TodoApp

## Übersicht

**TodoApp** ist eine mobile Anwendung, die mit **.NET MAUI** entwickelt wurde und die Verwaltung von ToDo-Items ermöglicht. Die App kommuniziert mit der **TodoApi**, um Aufgaben zu erstellen, abzurufen, zu aktualisieren und zu löschen. Sie ist für Android optimiert und unterstützt grundlegende Filterfunktionen für den Aufgabenstatus.

## Voraussetzungen

- **.NET 9.0 SDK**  
  [Download](https://dotnet.microsoft.com/download/dotnet/9.0)

- **Android Emulator** (mit API-Level 35 oder höher)  
  Installiere die Android-Entwicklungstools und richte einen Emulator ein. Stelle sicher, dass `sdkmanager` und `avdmanager` verfügbar sind.

- **Git**  
  [Download](https://git-scm.com/downloads)

- **API**
  [Todo API - GitHub Repository](https://github.com/AceVik/todo-api)

## Installation

1. **Repository klonen**

   ```bash
   git clone git@github.com:AceVik/todo-app.git
   cd todo-app
   ```

2. **Abhängigkeiten installieren**
   ```bash
   dotnet restore
   ```

3. **Android Emulator starten**
   ```bash
   emulator -avd <Emulator Name>
   // oder
   emulator -avd emulator-5554
   ```

4. **Projekt bauen**
   ```bash
   dotnet build
   ```

## Starten der Anwendung
Starte die App mit dem folgenden Befehl (ggf. Parameter anpassen):

```bash
dotnet msbuild /t:Run \
               /p:TargetFramework=net9.0-android \
               /p:Configuration=Debug \
               /p:DeviceName="emulator-5554" \
               TodoApp.csproj
```

- **TargetFramework:** Gibt die Zielplattform an (Android).
- **Configuration:** Debug-Modus für die Entwicklung. (alternativ `Release`)
- **DeviceName:** Der Name des Emulators (Standard: emulator-5554).

### Hinweise
- Kommunikation mit der API: Stelle sicher, dass die TodoApi gestartet ist und über `http://10.0.2.2:5080` im Emulator erreichbar ist. Dies ist die Standard-IP für den Zugriff auf den Host-Computer vom Android Emulator aus.
- Fehlerbehebung: Sollte es Verbindungsprobleme geben, überprüfe die Netzwerkkonfiguration und stelle sicher, dass die API korrekt läuft.