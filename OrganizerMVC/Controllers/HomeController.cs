﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrganizerMVC.DataAccess;
using OrganizerMVC.Models;

namespace OrganizerMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var context = new ApplicationDbContext();

            if (!context.Database.Exists())
                context.Database.Initialize(true);

            //todo: get activites only which belongs for current user

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}