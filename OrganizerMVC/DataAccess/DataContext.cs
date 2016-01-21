using System.Data.Entity;
using OrganizerMVC.Models;

namespace OrganizerMVC.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Activity> Activities { get; set; } 

        public DataContext() : base("SchedulerMVC")
        {

        }
    }
}