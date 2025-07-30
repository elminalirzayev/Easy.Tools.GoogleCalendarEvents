# Easy.Tools.GoogleCalendarEvents

A lightweight and easy-to-use .NET library for accessing public events and holidays from any Google Calendar using the Google Calendar API.

## 📦 Installation

```bash
dotnet add package Easy.Tools.GoogleCalendarEvents
```

## 🛠️ Features

- Fetch all events from a specified Google Calendar
- Full model support for deserialization
- Supports official and localized holiday calendars (e.g., Turkey, Islamic holidays)
- Simple and extensible API

## 🚀 Quick Start

```csharp
using Easy.Tools.GoogleCalendarEvents.Services;

// Set your Google API key and calendar ID
var apiKey = "YOUR_API_KEY";
var calendarId = "tr.turkish#holiday@group.v.calendar.google.com"; // Turkey holidays

var service = new GoogleEventService(apiKey, calendarId);

var events = await service.GetEventsAsync();

foreach (var item in events)
{
    Console.WriteLine(item); // Override of ToString prints event summary & dates
}
```

## 📅 Example Calendars

| Region / Purpose     | Calendar ID |
|----------------------|-------------|
| Turkey (TR, in Turkish) | `tr.turkish#holiday@group.v.calendar.google.com` |
| Turkey (official, in English) | `en.turkish.official#holiday@group.v.calendar.google.com` |
| Islamic Holidays     | `en.islamic#holiday@group.v.calendar.google.com` |

## 📝 Requirements

- .NET 8.0 or higher
- A valid Google Calendar API Key

## 📄 License

MIT License

---

© 2025 Elmin Alirzayev / Easy Code Tools
