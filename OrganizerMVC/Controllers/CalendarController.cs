using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using OrganizerMVC.DataAccess;
using OrganizerMVC.Models;
using OrganizerMVC.Models.Logic;
using OrganizerMVC.ViewModels;

namespace OrganizerMVC.Controllers
{
    [Authorize]
    public class CalendarController : BaseController
    {
        private readonly IRepository<Event, int> _repository;

        public CalendarController(IRepository<Event, int> repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string AddEvent(EventViewModel evm)
        {
            var manager = new UserManager(new UserStore(_repository.CurrentContext));
            var user = manager.FindById(CurrentUser.UserId);

            var newEvent = new Event
            {
                User = user,
                Name = evm.Name,
                Description = evm.Description,
                Date = evm.Date,
                Start = evm.Start,
                End = evm.End
            };

            _repository.Add(newEvent);

            var evnt = new CalendarEvent
            {
                title = newEvent.Name,
                description = newEvent.Description,
                start = Helpers.CombineDateWithTime(newEvent.Start, newEvent.Date),
                end = Helpers.CombineDateWithTime(newEvent.End, newEvent.Date),
                id = newEvent.EventId
            };

            var json = JsonConvert.SerializeObject(evnt, Formatting.None,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

            return json;
        }

        public string GetUserEvents()
        {
            var id = CurrentUser.UserId;
            var events = _repository.Get().Where(a => a.User.Id == id);

            var calEvents = new List<CalendarEvent>();
            calEvents.AddRange(events.Select(e => new CalendarEvent
            {
                title = e.Name,
                description = e.Description,
                start = Helpers.CombineDateWithTime(e.Start, e.Date),
                end = Helpers.CombineDateWithTime(e.End, e.Date),
                id = e.EventId
            }));

            var json = JsonConvert.SerializeObject(calEvents, Formatting.None,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

            return json;
        }
    }
}