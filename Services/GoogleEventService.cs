using Easy.Tools.GoogleCalendarEvents.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
#if NETSTANDARD2_1_OR_GREATER
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

namespace Easy.Tools.GoogleCalendarEvents.Services
{
    public class GoogleEventService
    {
        private readonly string _apiKey;
        private readonly string _calendarId;
        private readonly HttpClient _httpClient;

        public GoogleEventService(string apiKey, string calendarId, HttpClient? httpClient = null)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _calendarId = calendarId ?? throw new ArgumentNullException(nameof(calendarId));
            _httpClient = httpClient ?? new HttpClient();
        }

        public async Task<IReadOnlyList<Item>> GetEventsAsync(CancellationToken cancellationToken = default)
        {
            var url = BuildRequestUrl();

#if NETSTANDARD2_1_OR_GREATER
            using var response = await _httpClient.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<GoogleCalendarResponse>(json, options);
            return (data?.Items ?? new List<Item>()).AsReadOnly();
#else
            using (var response = await _httpClient.GetAsync(url))
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
