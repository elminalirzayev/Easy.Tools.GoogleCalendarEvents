using Easy.Tools.GoogleCalendarEvents.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

// Modern .NET için STJ
#if NET6_0_OR_GREATER || NETSTANDARD2_1
using System.Text.Json;
#endif

// Eski sürümler için Newtonsoft
#if NETSTANDARD2_0 || NET472_OR_GREATER
using Newtonsoft.Json;
#endif

namespace Easy.Tools.GoogleCalendarEvents.Services
{
    /// <summary>
    /// Provides functionality to fetch events from a public Google Calendar.
    /// </summary>
    public class GoogleEventService
    {
        private readonly string _apiKey;
        private readonly string _calendarId;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleEventService"/> class.
        /// </summary>
        /// <param name="apiKey">Your Google Cloud Console API Key.</param>
        /// <param name="calendarId">The ID of the public calendar (e.g., 'en.az#holiday@group.v.calendar.google.com').</param>
        /// <param name="httpClient">Optional HttpClient instance for dependency injection.</param>
        /// <exception cref="ArgumentNullException">Thrown when apiKey or calendarId is null.</exception>
        public GoogleEventService(string apiKey, string calendarId, HttpClient? httpClient = null)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _calendarId = calendarId ?? throw new ArgumentNullException(nameof(calendarId));
            _httpClient = httpClient ?? new HttpClient();
        }

        /// <summary>
        /// Fetches the list of events from the configured Google Calendar asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A read-only list of calendar items.</returns>
        /// <exception cref="HttpRequestException">Thrown when the API request fails.</exception>
        public async Task<IReadOnlyList<Item>> GetEventsAsync(CancellationToken cancellationToken = default)
        {
            var url = BuildRequestUrl();

#if NET6_0_OR_GREATER || NETSTANDARD2_1
            // --- MODERN (System.Text.Json) ---
            using var response = await _httpClient.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();

            string json;
#if NET6_0_OR_GREATER
                        json = await response.Content.ReadAsStringAsync(cancellationToken);
#else
            json = await response.Content.ReadAsStringAsync();
#endif
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<GoogleCalendarResponse>(json, options);
            return (data?.Items ?? new List<Item>()).AsReadOnly();

#else
            // --- LEGACY (Newtonsoft.Json) ---
            using (var response = await _httpClient.GetAsync(url, cancellationToken))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                
                var data = JsonConvert.DeserializeObject<GoogleCalendarResponse>(json);
                return (data?.Items ?? new List<Item>()).AsReadOnly();
            }
#endif
        }

        private string BuildRequestUrl()
        {
            var encodedCalendarId = Uri.EscapeDataString(_calendarId);
            return $"https://www.googleapis.com/calendar/v3/calendars/{encodedCalendarId}/events?key={_apiKey}";
        }
    }
}