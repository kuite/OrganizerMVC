using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Moq;
using Newtonsoft.Json;
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

        private EventViewModel _evnt;

        [SetUp]
        public void Setup()
        {
            SetupAsync().Wait();
        }

        public async Task SetupAsync()
        {
            _evnt = new EventViewModel
            {
                Title = "Title event",
                Description = "Test event",
                Start = "11:00",
                End = "14:27",
                Date = "2016-02-01"
            };

            var userViewModel = new LoginViewModel
            {
                Email = "user@wp.pl",
                Password = "useruser",
                RememberMe = false
            };
            var context = new DataContext();
            var manager = new UserManager(new UserStore(context));
            var user = await manager.FindAsync(userViewModel.Email, userViewModel.Password);
            if (user == null)
            {
                await manager.CreateAsync(new User { Email = userViewModel.Email, UserName = userViewModel.Email }, userViewModel.Password);
            }
            _calendarController = new CalendarController(context);

            var mockCp = new Mock<IClaimsPrincipal>();
            if (user != null) mockCp.SetupGet(cp => cp.UserId).Returns(user.Id);
            _calendarController.CurrentUser = mockCp.Object;

            var mockAuthenticationManager = new Mock<IAuthenticationManager>();
            mockAuthenticationManager.Setup(am => am.SignOut());
            mockAuthenticationManager.Setup(am => am.SignIn());
            _calendarController.AuthenticationManager = mockAuthenticationManager.Object;
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
            var events = _calendarController.GetUserEvents();
            _calendarController.AddEvent(_evnt);
            var newEvents = _calendarController.GetUserEvents();
            Assert.AreNotEqual(events, newEvents);
        }

        [Test]
        public void DeleteEventTest()
        {
            var rawEvents = _calendarController.GetUserEvents();
            var events = JsonConvert.DeserializeObject<List<Event>>(rawEvents);

            var evt = events.Last();
            _calendarController.DeleteEvent(evt.EventId);

            var newEvents = JsonConvert.DeserializeObject<List<Event>>(rawEvents);

            Assert.AreNotEqual(evt, newEvents.Last().EventId);
        }

        [Test]
        public void UpdateEventTest()
        {
            var rawEvents= _calendarController.GetUserEvents();
            var events = JsonConvert.DeserializeObject<List<EventViewModel>>(rawEvents);
            var evt = events.Last();

            var eventModel = _evnt;
            eventModel.Id = evt.Id;
            eventModel.Title = "Updated title";
            _calendarController.UpdateEvent(eventModel);

            Assert.AreNotEqual(evt.Title, 
                JsonConvert.DeserializeObject<List<Event>>(_calendarController.GetUserEvents()).Last().Title);
        }

        [Test]
        public void GetUserEventsTest()
        {
            var events = _calendarController.GetUserEvents();
            Assert.IsNotNull(events);
            Assert.IsNotEmpty(events);
        }
    }
}
