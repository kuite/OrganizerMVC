namespace OrganizerMVC.Models.Logic
{
    public static class Helpers
    {
        public static string CombineDateWithTime(string time, string date)
        {
            return string.Concat(date, "T", time, ":00");
        }
    }

    public class CalendarEvent
    {
        public string title { get; set; }
        public string description { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }
}