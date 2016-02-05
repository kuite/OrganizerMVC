using System.Linq;
using System.Web.Mvc;
using OrganizerMVC.DataAccess;
using OrganizerMVC.Models;
using OrganizerMVC.Models.Database;

namespace OrganizerMVC.Controllers
{
    [Authorize]
    public class CalendarController : BaseController
    {
        private IRepository<Activity, int> _repository;

        public CalendarController(IRepository<Activity, int> repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var id = CurrentUser.UserId;
            var activities = _repository.Get().Where(a => a.User.Id == id);

            return View();
        }
    }
}