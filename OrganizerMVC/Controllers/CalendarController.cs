using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using OrganizerMVC.DataAccess;
using OrganizerMVC.Models;
using OrganizerMVC.Models.Database;
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
            var id = CurrentUser.UserId;
            var userActivities = _repository.Get().Where(a => a.User.Id == id);

            return View(userActivities);
        }

        [HttpPost]
        public void AddEvent(EventViewModel actvm)
        {
            var manager = new UserManager(new UserStore(_repository.CurrentContext));
            var user = manager.FindById(CurrentUser.UserId);

            var actv = new Event
            {
                User = user,
                Name = actvm.Name,
                Description = actvm.Description,
                Year = Int32.Parse(actvm.Date.Substring(0, 4)),
                Month = Int32.Parse(actvm.Date.Substring(5, 2)),
                Day = Int32.Parse(actvm.Date.Substring(8, 2)),
                Start = actvm.Start,
                End = actvm.End
            };

            _repository.Add(actv);
        }

        public string GetEvents()
        {
            var events = @"
            				{
            				    title: 'Meeting',
            				    start: '2016-02-12T10:30:00',
            				    end: '2016-02-12T12:30:00'
            				},
            				{
            				    title: 'Lunch',
            				    start: '2016-02-12T12:00:00'
            				},
            				{
            				    title: 'Meeting',
            				    start: '2016-02-12T14:30:00'
            				},
            				{
            				    title: 'Happy Hour',
            				    start: '2016-02-12T17:30:00'
            				},
            				{
            				    title: 'Dinner',
            				    start: '2016-02-12T20:00:00'
            				},
            				{
            				    title: 'Birthday Party',
            				    start: '2016-02-13T07:00:00'
            				},
            				{
            				    title: 'Click for Wykop',
            				    url: 'http://wykop.pl/',
            				    start: '2016-02-28'
            				}";


            var jsSerializer = new JavaScriptSerializer();
            var json = jsSerializer.Serialize(events);
            return json;
        }
    }
}