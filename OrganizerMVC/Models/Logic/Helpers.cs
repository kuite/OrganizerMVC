namespace OrganizerMVC.Models.Logic
{
    public static class Helpers
    {
        public static string CombineDateWithTime(string time, string date)
        {
            if (!(time.StartsWith("1") || time.StartsWith("2")))
            {
                time = "0" + time;
            }
            return string.Concat(date, "T", time, ":00");
        }
    }

    public class CalendarEvent
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }
}