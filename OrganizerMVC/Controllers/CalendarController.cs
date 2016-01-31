using System.Web.Mvc;
using OrganizerMVC.DataAccess;

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