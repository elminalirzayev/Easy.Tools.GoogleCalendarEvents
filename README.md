[![Build & Test](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/actions/workflows/build.yml/badge.svg)](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/actions/workflows/build.yml)
[![Build & Release](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/actions/workflows/release.yml/badge.svg)](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/actions/workflows/release.yml)
[![Build & Nuget Publish](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/actions/workflows/nuget.yml/badge.svg)](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/actions/workflows/nuget.yml)
[![Release](https://img.shields.io/github/v/release/elminalirzayev/Easy.Tools.GoogleCalendarEvents)](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/releases)
[![License](https://img.shields.io/github/license/elminalirzayev/Easy.Tools.GoogleCalendarEvents)](https://github.com/elminalirzayev/Easy.Tools.GoogleCalendarEvents/blob/master/LICENSE.txt)
[![NuGet](https://img.shields.io/nuget/v/Easy.Tools.GoogleCalendarEvents.svg)](https://www.nuget.org/packages/Easy.Tools.GoogleCalendarEvents)


# Easy.Tools.GoogleCalendarEvents

A lightweight and easy-to-use .NET library for accessing public events and holidays from any Google Calendar using the Google Calendar API.

## Installation

dotnet add package Easy.Tools.GoogleCalendarEvents

## Features

- Fetch all events from a specified Google Calendar
- Full model support for deserialization
- Supports official and localized holiday calendars
- Simple and extensible API

## Quick Start

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

##  Example Calendars

| Region / Purpose     | Calendar ID |
|----------------------|-------------|
| Turkey (TR, in Turkish) | `tr.turkish#holiday@group.v.calendar.google.com` |
| Turkey (official, in English) | `en.turkish.official#holiday@group.v.calendar.google.com` |
| Azerbaijan (official, in English) | `en-gb.az#holiday@group.v.calendar.google.com` |


## Requirements

- A valid Google Calendar API Key 

## License

MIT License

---

© 2025 Elmin Alirzayev / Easy Code Tools
