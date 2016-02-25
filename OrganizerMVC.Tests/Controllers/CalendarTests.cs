using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Moq;
using NUnit.Framework;
using OrganizerMVC.Controllers;
using OrganizerMVC.Models;
using OrganizerMVC.Models.Database;
using OrganizerMVC.ViewModels;

namespace OrganizerMVC.Tests.Controllers
{
    public class CalendarTests
    {
        private CalendarController _calendarController;

        [SetUp]
        public void Setup()
        {
            SetupAsync().Wait();
        }

        public async Task SetupAsync()
        {
            var userViewModel = new LoginViewModel
            {
                Email = "user@wp.pl",
                Password = "useruser",
                RememberMe = false
            };
            var context = new DataContext();
            var manager = new UserManager(new UserStore(context));
            _calendarController = new CalendarController(context);

            var mockAuthenticationManager = new Mock<IAuthenticationManager>();
            mockAuthenticationManager.Setup(am => am.SignOut());
            mockAuthenticationManager.Setup(am => am.SignIn());
            _calendarController.AuthenticationManager = mockAuthenticationManager.Object;

            if (manager.FindByEmail("test@wp.pl") == null)
            {
                await manager.CreateAsync(new User { Email = userViewModel.Email, UserName = userViewModel.Email }, userViewModel.Password);
            }
        }

        [Test]
        public void IndexTest()
        {
            var rslt = _calendarController.Index() as ViewResult;
            Assert.IsNotNull(rslt);
        }

        [Test]
        public void AddEventTest()
        {
            var evnt = new EventViewModel
            {
                Title = "Title event",
                Description = "Test event",
                Start = "11:00",
                End = "14:27",
                Date = "2016-02-01"
            };
            var events = _calendarController.GetUserEvents();
            _calendarController.AddEvent(evnt);
            var newEvents = _calendarController.GetUserEvents();
            Assert.AreNotEqual(events, newEvents);
        }

        [Test]
        public void DeleteEventTest()
        {

        }

        [Test]
        public void UpdateEventTest()
        {

        }

        [Test]
        public void GetUserEventsTest()
        {
            var events = _calendarController.GetUserEvents();
        }
    }
}
