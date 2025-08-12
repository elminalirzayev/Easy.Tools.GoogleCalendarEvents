
namespace Easy.Tools.GoogleCalendarEvents.Models
{
    public class GoogleCalendarResponse
    {
        public List<Item> Items { get; set; } = new List<Item>();
    }

    public class Item
    {
        public string Status { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateValue Start { get; set; } = new DateValue();
        public DateValue End { get; set; } = new DateValue();

        public override string ToString()
        {
            return $"Summary: {Summary}, Description: {Description}, Status: {Status}, Start: {Start?.Date}, End: {End?.Date}";
        }
    }

    public class DateValue
    {
        public string Date { get; set; } = string.Empty;
    }

}
