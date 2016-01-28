using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OrganizerMVC.Models;

namespace OrganizerMVC.DataAccess
{
    public class DataInitializer : DropCreateDatabaseAlways<DataContext>
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
            var acctivities = new List<Activity> {actv3, actv4, actv5};

            if (!context.Users.Any(u => u.UserName == "tester@wp.pl"))
            {
                var userStore = new UserStore<AppUser>(context);
                var userManager = new UserManager<AppUser>(userStore);

                var userToInsert = new AppUser
                {
                    UserName = "tester@wp.pl",
                    PhoneNumber = "0797697898",
                    Activities = new List<Activity> { actv1, actv2 }
                };
                userManager.Create(userToInsert, "tester");

                actv1.AppUser = userToInsert;
                actv2.AppUser = userToInsert;
            }


            //todo 0: create user with activities tester@wp.pl/tester

            acctivities.ForEach(a => context.Activities.Add(a));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}