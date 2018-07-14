using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : BaseController
    {
        // GET: MV
        public ActionResult Index()
        {
            ViewData.Model = new {msg="hi" };
            return View();
        }

        public ActionResult ViewDataTest()
        {
            ViewData["test"] = "test";
            return View();
        }

        public ActionResult ViewBagTest()
        {
            ViewBag.msg = "test";
            return View();
        }
        public ActionResult TempDataSave()
        {
            TempData["msg"] = "test";
            return RedirectToAction("TempDataTest");
        }
        public ActionResult TempDataTest()
        {
            return View();
        }
    }
}