using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using OrganizerMVC.Models;

namespace OrganizerMVC.DataAccess
{
    public class DataContext : IdentityDbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Activity> Activities { get; set; } 

        public DataContext() : base("SchedulerMVC")
        {

        }
    }
}