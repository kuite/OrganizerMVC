﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OrganizerMVC.Models.Database
{
    public class DataContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public DbSet<Activity> Activities { get; set; }

        public DataContext() : base("SchedulerMvc")
        {
            System.Data.Entity.Database.SetInitializer(new DataInitializer());
        }

        public static DataContext Create()
        {
            return new DataContext();
        }
    }
}