#if NET6_0_OR_GREATER || NETSTANDARD2_1
using System.Text.Json.Serialization;
#endif
#if NETSTANDARD2_0 || NET472_OR_GREATER
using Newtonsoft.Json;
#endif

namespace Easy.Tools.GoogleCalendarEvents.Models
{
    /// <summary>
    /// Represents the root response from the Google Calendar API.
    /// </summary>
    public class GoogleCalendarResponse
    {
        /// <summary>
        /// Gets or sets the list of event items.
        /// </summary>
#if NET6_0_OR_GREATER || NETSTANDARD2_1
        [JsonPropertyName("items")]
#endif
#if NETSTANDARD2_0 || NET472_OR_GREATER
        [JsonProperty("items")]
#endif
        public List<Item> Items { get; set; } = new List<Item>();
    }

    /// <summary>
    /// Represents a single event item in the calendar.
    /// </summary>
    public class Item
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
        public DateValue Start { get; set; } = new DateValue();

        /// <summary>
        /// The end date of the event.
        /// </summary>
#if NET6_0_OR_GREATER || NETSTANDARD2_1
        [JsonPropertyName("end")]
#endif
#if NETSTANDARD2_0 || NET472_OR_GREATER
        [JsonProperty("end")]
#endif
        public DateValue End { get; set; } = new DateValue();

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Summary: {Summary}, Description: {Description}, Status: {Status}, Start: {Start?.Date}, End: {End?.Date}";
        }
    }

    /// <summary>
    /// Wrapper for the date string.
    /// </summary>
    public class DateValue
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
    }
}