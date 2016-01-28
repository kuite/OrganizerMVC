using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OrganizerMVC.Models;

namespace OrganizerMVC.DataAccess
{
    //public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
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

            //todo 0: create user with activities tester@wp.pl/tester

            acctivities.ForEach(a => context.Activities.Add(a));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}