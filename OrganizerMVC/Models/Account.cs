using System.Collections.Generic;

namespace SchedulerMVC.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}