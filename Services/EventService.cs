using Easy.Tools.GoogleCalendarEvents.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

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

            using var response = await _httpClient.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<GoogleCalendarResponse>(json, options);

            return (data?.Items ?? new List<Item>()).AsReadOnly();
        }

        private string BuildRequestUrl()
        {
            var encodedCalendarId = Uri.EscapeDataString(_calendarId);
            return $"https://www.googleapis.com/calendar/v3/calendars/{encodedCalendarId}/events?key={_apiKey}";
        }
    }
}
