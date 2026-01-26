[![Build & Test](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/actions/workflows/build.yml/badge.svg)](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/actions/workflows/build.yml)
[![Build & Release](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/actions/workflows/release.yml/badge.svg)](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/actions/workflows/release.yml)
[![Build & Nuget Publish](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/actions/workflows/nuget.yml/badge.svg)](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/actions/workflows/nuget.yml)
[![Release](https://img.shields.io/github/v/release/elminalirzayev/Easy.Tools.GoogleCalendarEvents)](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/releases)
[![License](https://img.shields.io/github/license/elminalirzayev/Easy.Tools.GoogleCalendarEvents)](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/blob/master/LICENSE.txt)
[![NuGet](https://img.shields.io/nuget/v/Easy.Tools.GoogleCalendarEvents.svg)](https://www.nuget.org/packages/Easy.Tools.GoogleCalendarEvents)


# Easy.Tools.GoogleCalendarEvents

A lightweight, high-performance .NET library for accessing public events and holidays from Google Calendar. 
Designed to be dependency-free on modern .NET and compatible with legacy Frameworks.

## Features

- **Zero-Dependency on Modern .NET:** Uses `System.Text.Json` natively on .NET 6+.
- **Broad Compatibility:** Supports .NET 6, .NET 7, .NET 8, .NET 9,.NET 10 .NET Standard 2.0/2.1, and .NET Framework 4.7.2+.
- **Simple API:** Fetch events with a single asynchronous method call.
- **Strongly Typed:** Full object model support for calendar events.
- **Resilient:** Includes built-in token support for cancellation.

## Installation

Install via NuGet Package Manager Console:
```powershell
Install-Package Easy.Tools.GoogleCalendarEvents
```
Or via .NET CLI:
```powershell
dotnet add package Easy.Tools.GoogleCalendarEvents
```

## Prerequisites

To use this library, you need a **Google API Key** (no OAuth required for public calendars).

1.  Go to [Google Cloud Console](https://console.cloud.google.com/).    
2.  Create a new project or select an existing one.    
3.  Navigate to **APIs & Services > Credentials**.    
4.  Click **Create Credentials** -> **API Key**.    
5.  Enable the **Google Calendar API** in the "Library" section.
    

## Usage

### 1. Basic Console Application

```csharp
using Easy.Tools.GoogleCalendarEvents.Services;

// 1. Setup your credentials
var apiKey = "YOUR_GOOGLE_API_KEY";
var calendarId = "tr.turkish#holiday@group.v.calendar.google.com"; // Example: Turkey Holidays

// 2. Initialize the service
var service = new GoogleEventService(apiKey, calendarId);

try 
{
    // 3. Fetch events
    var events = await service.GetEventsAsync();

    Console.WriteLine($"Found {events.Count} events:");
    foreach (var item in events)
    {
        Console.WriteLine($"- {item.Summary} ({item.Start.Date})");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error fetching events: {ex.Message}");
}
```

### 2. ASP.NET Core (Dependency Injection)

This library is designed to work seamlessly with `HttpClientFactory`.

In `Program.cs`:
```csharp
using Easy.Tools.GoogleCalendarEvents.Services;

var builder = WebApplication.CreateBuilder(args);

// Register HttpClient and the Service
builder.Services.AddHttpClient<GoogleEventService>(client => 
{
    // Optional: Configure global timeouts or headers here if needed
});

// Register as Singleton or Scoped depending on your needs
builder.Services.AddSingleton(sp => 
    new GoogleEventService(
        "YOUR_API_KEY", 
        "tr.turkish#holiday@group.v.calendar.google.com", 
        sp.GetRequiredService<HttpClient>()
    ));

var app = builder.Build();
```

## Popular Holiday Calendar IDs

You can use any public calendar ID. Here are some common ones for holidays:

| Region | Language | Calendar ID |
| --- | --- | --- |
| 🇹🇷 Turkey | Turkish | `tr.turkish#holiday@group.v.calendar.google.com` |
| 🇹🇷 Turkey | English | `en.turkish.official#holiday@group.v.calendar.google.com` |
| 🇦🇿 Azerbaijan | English | `en-gb.az#holiday@group.v.calendar.google.com` |
| 🇺🇸 USA | English | `en.usa#holiday@group.v.calendar.google.com` |
| 🇩🇪 Germany | German | `de.german#holiday@group.v.calendar.google.com` |


 **Tip:** To find a Calendar ID, go to Google Calendar Settings > Add Calendar > Browse resources of interest, or look at the "Integrate calendar" section of any public calendar settings.


## License

This project is licensed under the MIT License.

---

© 2025 Elmin Alirzayev / Easy Code Tools
