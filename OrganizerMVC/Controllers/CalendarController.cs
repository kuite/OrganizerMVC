using System;
using System.Linq;
using System.Web.Mvc;
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
        private readonly IRepository<Activity, int> _repository;

        public CalendarController(IRepository<Activity, int> repository)
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
        public void Add(ActivityViewModel actvm)
        {
            var manager = new UserManager(new UserStore(_repository.CurrentContext));
            var user = manager.FindById(CurrentUser.UserId);

            var actv = new Activity
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
    }
}