using System.Globalization;

#if NET6_0_OR_GREATER || NETSTANDARD2_1
using System.Text.Json.Serialization;
#endif
#if NETSTANDARD2_0 || NET472_OR_GREATER
using Newtonsoft.Json;
#endif

namespace Easy.Tools.GoogleCalendarEvents
{
    /// <summary>
    /// Represents the root response from the Google Calendar API.
    /// </summary>
    public class GoogleCalendarResponse
    {
        /// <summary>
        /// Gets or sets the list of calendar events.
        /// </summary>
#if NET6_0_OR_GREATER || NETSTANDARD2_1
        [JsonPropertyName("items")]
#endif
#if NETSTANDARD2_0 || NET472_OR_GREATER
        [JsonProperty("items")]
#endif
        public List<GoogleCalendarEvent> Events { get; set; } = new List<GoogleCalendarEvent>();
    }

    /// <summary>
    /// Represents a single event in the Google Calendar.
    /// </summary>
    public class GoogleCalendarEvent
    {
        /// <summary>
        /// Status of the event (e.g., "confirmed").
        /// </summary>
#if NET6_0_OR_GREATER || NETSTANDARD2_1
        [JsonPropertyName("status")]
#endif
#if NETSTANDARD2_0 || NET472_OR_GREATER
        [JsonProperty("status")]
#endif
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Summary or title of the event.
        /// </summary>
#if NET6_0_OR_GREATER || NETSTANDARD2_1
        [JsonPropertyName("summary")]
#endif
#if NETSTANDARD2_0 || NET472_OR_GREATER
        [JsonProperty("summary")]
#endif
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// Detailed description of the event.
        /// </summary>
#if NET6_0_OR_GREATER || NETSTANDARD2_1
        [JsonPropertyName("description")]
#endif
#if NETSTANDARD2_0 || NET472_OR_GREATER
        [JsonProperty("description")]
#endif
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The start date of the event.
        /// </summary>
#if NET6_0_OR_GREATER || NETSTANDARD2_1
        [JsonPropertyName("start")]
#endif
#if NETSTANDARD2_0 || NET472_OR_GREATER
        [JsonProperty("start")]
#endif
        public GoogleCalendarDate Start { get; set; } = new GoogleCalendarDate();

        /// <summary>
        /// The end date of the event.
        /// </summary>
#if NET6_0_OR_GREATER || NETSTANDARD2_1
        [JsonPropertyName("end")]
#endif
#if NETSTANDARD2_0 || NET472_OR_GREATER
        [JsonProperty("end")]
#endif
        public GoogleCalendarDate End { get; set; } = new GoogleCalendarDate();

        /// <inheritdoc />
        public override string ToString()
        {
            // Format: [confirmed] Title (2023-01-01 - 2023-01-02)
            var start = Start?.Date ?? "N/A";
            var end = End?.Date ?? "N/A";
            var summary = string.IsNullOrWhiteSpace(Summary) ? "(No Title)" : Summary;

            return $"[{Status}] {summary} ({start} - {end})";
        }
    }

    /// <summary>
    /// Represents the date structure in Google Calendar API response.
    /// Used typically for all-day events (holidays) where time is not provided.
    /// </summary>
    public class GoogleCalendarDate
    {
        /// <summary>
        /// The date in string format (YYYY-MM-DD).
        /// </summary>
#if NET6_0_OR_GREATER || NETSTANDARD2_1
        [JsonPropertyName("date")]
#endif
#if NETSTANDARD2_0 || NET472_OR_GREATER
        [JsonProperty("date")]
#endif
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// Helper property: Parses the string date to a C# DateTime object.
        /// Returns DateTime.MinValue if parsing fails.
        /// </summary>
        [JsonIgnore]
        public DateTime ParsedDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Date)) return DateTime.MinValue;

                // Google Calendar "date" format is usually yyyy-MM-dd
                return DateTime.TryParseExact(Date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt)
                    ? dt
                    : DateTime.MinValue;
            }
        }
    }
}