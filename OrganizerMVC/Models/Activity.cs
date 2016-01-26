
namespace OrganizerMVC.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Time { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}