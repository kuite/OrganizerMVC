using System.Linq;
using System.Web.Mvc;
using OrganizerMVC.DataAccess;
using OrganizerMVC.Models;

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
        public ActionResult Add(Activity activity)
        {
            _repository.Add(activity);

            return RedirectToAction("Index", "Calendar");
        }
    }
}