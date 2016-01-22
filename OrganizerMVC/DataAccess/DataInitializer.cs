using System;
using System.Collections.Generic;
using System.Data.Entity;
using OrganizerMVC.Models;

namespace OrganizerMVC.DataAccess
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var actv1 = new Activity
            {
                Year = 2015,
                Month = 3,
                Name = "Meczyk",
                Description = "Friendly game with frinds",
                Day = 20,
                Time = DateTime.Now.ToString("HH:mm"),
            };

            var actv2 = new Activity
            {
                Year = 2015,
                Month = 5,
                Name = "Wykład",
                Description = "twotwtwototw",
                Day = 1,
                Time = DateTime.Now.ToString("HH:mm"),
            };

            var actv3 = new Activity
            {
                Year = 2015,
                Month = 7,
                Name = "Meczyk",
                Description = "threethreether",
                Day = 330,
                Time = DateTime.Now.ToString("HH:mm"),
            };

            var actv4 = new Activity
            {
                Year = 2015,
                Month = 9,
                Name = "Meczyk",
                Description = "5fourfourfourfour",
                Day = 40,
                Time = DateTime.Now.ToString("HH:mm"),
            };

            var actv5 = new Activity
            {
                Year = 2015,
                Month = 8,
                Name = "Meczyk",
                Description = "555fivefiveifve",
                Day = 55,
                Time = DateTime.Now.ToString("HH:mm"),
            };
            var acctivities = new List<Activity> {actv1, actv2, actv3, actv4, actv5};

            var ac1 = new Account
            {
                Login = "admin",
                Password = "admin",
                Email = "ziomek@o2.pl",
                Activities = new List<Activity> { actv1, actv2 }
            };

            var ac2 = new Account
            {
                Login = "asdwrjh",
                Password = "polska922",
                Email = "asdasdw@o2.pl",
                Activities = new List<Activity>()
            };
            var accounts = new List<Account> { ac1, ac2 };

            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
            {
                Name = "User"
            });

            accounts.ForEach(a => context.Accounts.Add(a));
            acctivities.ForEach(a => context.Activities.Add(a));
            context.SaveChanges();
        }
    }
}