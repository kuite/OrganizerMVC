using System.Web.Mvc;
using OrganizerMVC.DataAccess;

namespace OrganizerMVC.Controllers
{
    public class CalendarController : Controller
    {
        public ActionResult Index()
        {
            //todo: get activites only which belongs for current user

            return View();
        }
    }
}