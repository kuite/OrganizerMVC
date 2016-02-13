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
                Name = "Meczyk",
                Description = "Friendly game with frinds",
                Date = "2016-02-16",
                Start = DateTime.Now.ToString("HH:mm"),
                End = DateTime.Now.AddHours(3).ToString("HH:mm")
            };

            var actv2 = new Event
            {
                Name = "Wykład",
                Description = "twotwtwototw",
                Date = "2016-02-15",
                Start = DateTime.Now.ToString("HH:mm"),
                End = DateTime.Now.AddHours(3).ToString("HH:mm")
            };

            var actv3 = new Event
            {
                Name = "Meczyk",
                Description = "threethreether",
                Date = "2016-02-10",
                Start = DateTime.Now.ToString("HH:mm"),
                End = DateTime.Now.AddHours(3).ToString("HH:mm")
            };

            var actv4 = new Event
            {
                Name = "Meczyk",
                Description = "5fourfourfourfour",
                Date = "2016-02-11",
                Start = DateTime.Now.ToString("HH:mm"),
                End = DateTime.Now.AddHours(3).ToString("HH:mm")
            };

            var actv5 = new Event
            {
                Name = "Meczyk",
                Description = "555fivefiveifve",
                Date = "2016-02-12",
                Start = DateTime.Now.ToString("HH:mm"),
                End = DateTime.Now.AddHours(3).ToString("HH:mm")
            };

            if (!context.Users.Any(u => u.UserName == "tester@wp.pl"))
            {
                var userStore = new UserStore(context);
                var userManager = new UserManager(userStore);

                var userToInsert = new User
                {
                    UserName = "tester@wp.pl",
                    Email = "tester@wp.pl",
                    PhoneNumber = "0797697898",
                    Activities = new List<Event> { actv1, actv2, actv3, actv4, actv5 }
                };
                userManager.Create(userToInsert, "tester");

                actv1.User = userToInsert;
                actv2.User = userToInsert;
                actv3.User = userToInsert;
            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}