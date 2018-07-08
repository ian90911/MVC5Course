﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewTest()
        {
            return View((object)("test"));
        }

        public PartialViewResult PartialViewTest()
        {
            return PartialView("ViewTest", "hihihi");
        }
    }
}