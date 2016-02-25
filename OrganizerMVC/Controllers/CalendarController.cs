using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using OrganizerMVC.Models;
using OrganizerMVC.Models.Database;
using OrganizerMVC.Models.Logic;
using OrganizerMVC.Services;
using OrganizerMVC.ViewModels;
using WebGrease.Css.Extensions;

namespace OrganizerMVC.Controllers
{
    [Authorize]
    public class CalendarController : BaseController
    {
        private readonly IEventsService _eventsService;

        private readonly UserManager _manager;

        private IAuthenticationManager _authenticationManager;

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return _authenticationManager ?? Request.GetOwinContext().Authentication;
            }
            set
            {
                _authenticationManager = value;
            }
        }

        public CalendarController(DataContext context)
        {
            _eventsService = new EventsService(context);
            _manager = new UserManager(new UserStore(context));
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string AddEvent(EventViewModel evm)
        {
            var user = _manager.FindById(CurrentUser.UserId);

            var newEvent = new Event
            {
                User = user,
                Title = evm.Title,
                Description = evm.Description,
                Date = evm.Date,
                Start = evm.Start,
                End = evm.End
            };

            _eventsService.Add(newEvent);

            var evnt = new CalendarEvent
            {
                title = newEvent.Title,
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
            var events = _eventsService.GetUserEvents(id);

            var calEvents = new List<CalendarEvent>();
            events.ForEach(e => calEvents.Add(new CalendarEvent
            {
                title = e.Title,
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

        [HttpPost]
        public string DeleteEvent(int id)
        {
            int deletedId;
            var evt = _eventsService.Get(id);

            if (evt != null)
            {
                _eventsService.Delete(evt.EventId);
                deletedId = id;
            }
            else
            {
                deletedId = 0;
            }

            var json = JsonConvert.SerializeObject(deletedId, Formatting.None);

            return json;
        }

        [HttpPost]
        public string UpdateEvent(EventViewModel evm)
        {
            var user = _manager.FindById(CurrentUser.UserId);
            var evId = evm.Id;

            var evt = new Event
            {
                EventId = evm.Id,
                User = user,
                Title = evm.Title,
                Description = evm.Description,
                Date = evm.Date,
                Start = evm.Start,
                End = evm.End
            };
            _eventsService.Update(evt);

            var json = JsonConvert.SerializeObject(evId);

            return json;
        }
    }
}