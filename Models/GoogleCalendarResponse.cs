
namespace Easy.Tools.GoogleCalendarEvents.Models
{
    public class GoogleCalendarResponse
    {
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string Status { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public DateValue Start { get; set; }
        public DateValue End { get; set; }

        public override string ToString()
        {
            return $"Summary: {Summary}, Description: {Description}, Status: {Status}, Start: {Start?.Date}, End: {End?.Date}";
        }
    }

    public class DateValue
    {
        public string Date { get; set; }
    }

}
