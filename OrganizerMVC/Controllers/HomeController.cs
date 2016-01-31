using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrganizerMVC.DataAccess;
using OrganizerMVC.Models;

namespace OrganizerMVC.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var context = new DataContext();
            if (!context.Database.Exists())
            {
                context.Database.Initialize(true);
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}