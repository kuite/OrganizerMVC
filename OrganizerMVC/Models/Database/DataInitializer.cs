using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace OrganizerMVC.Models.Database
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var actv1 = new Event
            {
                Title = "Turniej",
                Description = "Friendly game with frinds",
                Date = "2016-02-16",
                Start = "10:30",
                End = "12:30"
            };

            var actv2 = new Event
            {
                Title = "Wykład",
                Description = "twotwtwototw",
                Date = "2016-02-15",
                Start = "9:30",
                End = "12:30"
            };

            var actv3 = new Event
            {
                Title = "Meczyk",
                Description = "threethreether",
                Date = "2016-02-10",
                Start = "10:30",
                End = "12:30"
            };

            var actv4 = new Event
            {
                Title = "Event",
                Description = "5fourfourfourfour",
                Date = "2016-02-11",
                Start = "10:30",
                End = "21:30"
            };

            var actv5 = new Event
            {
                Title = "Tytul",
                Description = "555fivefiveifve",
                Date = "2016-02-12",
                Start = "12:30",
                End = "18:30"
            };

            if (!context.Users.Any(u => u.UserName == "user@wp.pl"))
            {
                var userStore = new UserStore(context);
                var userManager = new UserManager(userStore);

                var userToInsert = new User
                {
                    UserName = "user@wp.pl",
                    Email = "user@wp.pl",
                    PhoneNumber = "0797697898",
                    Activities = new List<Event> { actv1, actv2, actv3, actv4, actv5 }
                };
                userManager.Create(userToInsert, "useruser");

                actv1.User = userToInsert;
                actv2.User = userToInsert;
                actv3.User = userToInsert;
            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}