using System.Linq;
using System.Web.Mvc;
using OrganizerMVC.Models.Database;

namespace OrganizerMVC.Controllers
{
    [Authorize]
    public class CalendarController : BaseController
    {
        private readonly DataContext db = new DataContext();

        public ActionResult Index()
        {
            //todo: get activites only which belongs for current user

            var id = CurrentUser.UserId;
            var userActivities = db.Activities.Where(a => a.User.Id == id);

            return View();
        }
    }
}