using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OrganizerMVC.Models.Database
{
    public class DataContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public DbSet<Event> Events { get; set; }

        public DataContext() : base("SchedulerMvc")
        {
            System.Data.Entity.Database.SetInitializer(new DataInitializer());
        }
    }
}