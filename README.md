# Sample WebSocket Windows Service

This project implements a WebSocket server as a Windows background service using .NET, allowing communication between the service and a web application.

## Table of Contents

1. [Features](#features)
2. [Prerequisites](#prerequisites)
3. [Installation](#installation)
4. [Usage](#usage)
5. [Project Structure](#project-structure)
6. [Configuration](#configuration)
7. [Troubleshooting](#troubleshooting)

## Features

- Windows background service implementation using .NET
- WebSocket server for real-time communication
- Simple web client for testing and demonstration

## Prerequisites

- .NET 8.0 SDK
- Windows operating system
- Administrator privileges (for service installation)

## Installation

1. Clone the repository:
`git clone https://github.com/Coder-Krish/sample-websocket-service.git`

2. Build the project: `dotnet build`
3. Publish the service: `dotnet publish -c Release -r win-x64 --self-contained`
4. Install the Windows service (run as administrator): `sc create MyWebSocketService binPath= "DriveLetter:\path\to\your\published\MyWebSocketService.exe"`

## Usage

1. Start the Windows service: `sc start MyWebSocketService`
2. Open the `index.html` file in a web browser to use the web client.

3. Enter messages in the web client to communicate with the service.

## Project Structure

- `Worker.cs`: Contains the main logic for the WebSocket server.
- `Program.cs`: Configures and runs the Windows service.
- `ClientApp/index.html`: Web client for testing the WebSocket connection.

## Configuration

- The WebSocket server listens on `ws://localhost:8085` by default.
- Modify the `Worker.cs` file to change the server's behavior.
- Update the `index.js` file if you change the WebSocket server address.

## Troubleshooting

- Ensure the service is running: `sc query MyWebSocketService`
- Check Windows Event Viewer for any error logs
- Verify firewall settings if connecting from other machines
