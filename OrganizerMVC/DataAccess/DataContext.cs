using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using OrganizerMVC.Models;

namespace OrganizerMVC.DataAccess
{
    //public class DataContext : IdentityDbContext<User>
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public DbSet<Activity> Activities { get; set; }

        public DataContext() : base("SchedulerMvc")
        {
            Database.SetInitializer(new DataInitializer());
        }

        public static DataContext Create()
        {
            return new DataContext();
        }
    }
}