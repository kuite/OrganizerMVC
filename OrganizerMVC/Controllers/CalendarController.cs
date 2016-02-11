using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OrganizerMVC.DataAccess;
using OrganizerMVC.Models;
using OrganizerMVC.Models.Database;

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
        public void Add(Activity actv)
        {
            var manager = new UserManager(new UserStore(_repository.CurrentContext));
            var user = manager.FindById(CurrentUser.UserId);

            actv.User = user;

            _repository.Add(actv);
        }
    }
}