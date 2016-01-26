using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using OrganizerMVC.Models;

namespace OrganizerMVC.DataAccess
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Activity> Activities { get; set; }



        public DataContext() : base("SchedulerMvc", throwIfV1Schema: false)
        {
            Database.SetInitializer(new DataInitializer());
        }

        public static DataContext Create()
        {
            return new DataContext();
        }
    }
}