using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class RazorController : Controller
    {
        private IClientRepository _clientRepo;

        private IClientRepository ClientRepo
        {
            get
            {
                if (this._clientRepo == null)
                {
                    this._clientRepo = RepositoryHelper.GetClientRepository();
                }
                return this._clientRepo;
            }
        }
        // GET: Razor
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult TryModelStateExtension(int id)
        {
            var clients=ClientRepo.GetClientById(id);
            return View(clients);
        }
    }
}