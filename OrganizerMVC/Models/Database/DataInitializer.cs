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
                Year = 2015,
                Month = 3,
                Name = "Meczyk",
                Description = "Friendly game with frinds",
                Day = 20,
                Start = DateTime.Now.ToString("HH:mm"),
            };

            var actv2 = new Event
            {
                Year = 2015,
                Month = 5,
                Name = "Wykład",
                Description = "twotwtwototw",
                Day = 1,
                Start = DateTime.Now.ToString("HH:mm"),
            };

            var actv3 = new Event
            {
                Year = 2015,
                Month = 7,
                Name = "Meczyk",
                Description = "threethreether",
                Day = 330,
                Start = DateTime.Now.ToString("HH:mm"),
            };

            var actv4 = new Event
            {
                Year = 2015,
                Month = 9,
                Name = "Meczyk",
                Description = "5fourfourfourfour",
                Day = 40,
                Start = DateTime.Now.ToString("HH:mm"),
            };

            var actv5 = new Event
            {
                Year = 2015,
                Month = 8,
                Name = "Meczyk",
                Description = "555fivefiveifve",
                Day = 55,
                Start = DateTime.Now.ToString("HH:mm"),
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