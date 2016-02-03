using System.Web.Mvc;

namespace OrganizerMVC.Controllers
{
    [Authorize]
    public class CalendarController : BaseController
    {
        public ActionResult Index()
        {
            //todo: get activites only which belongs for current user

            var id = CurrentUser.UserId;

            return View();
        }
    }
}