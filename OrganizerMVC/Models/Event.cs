﻿
namespace OrganizerMVC.Models
{
    public class Event
    {
        public int EventId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public virtual User User { get; set; }
    }
}