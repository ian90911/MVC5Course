using MVC5Course.Models.Attribute;
using MVC5Course.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HandleError(ExceptionType =typeof(TestException),
            View= "TestExceptionError")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            throw new TestException();
            return View();
        }

        [ProductOnly]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}