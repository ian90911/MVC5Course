using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.ViewModel;

namespace MVC5Course.Controllers
{
    [RoutePrefix("VIP")]
    public class ClientsController : BaseController
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

        private IOccupationRepository _occupationRepo;

        private IOccupationRepository OccupationRepo
        {
            get
            {
                if (this._occupationRepo == null)
                {
                    this._occupationRepo = RepositoryHelper.GetOccupationRepository(ClientRepo.UnitOfWork);
                }
                return this._occupationRepo;
            }
        }
        // GET: Clients
        public ActionResult Index(string clientName,int take=10,int skip=0)
        {
            var list = ClientRepo.GetClientByName(clientName,take,skip);
            return View(list);
        }

        // GET: Clients/Details/5
        [Route("{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = ClientRepo.GetClientById(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        [Route("CreateVIP")]
        public ActionResult Create()
        {
            ViewBag.OccupationId = new SelectList(OccupationRepo.All(), "OccupationId", "OccupationName");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("CreateVIP")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,IdNumber,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes")] Client client)
        {
            if (ModelState.IsValid)
            {
                ClientRepo.Add(client);
                ClientRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.OccupationId = new SelectList(OccupationRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Edit/5
        [Route("EditVIP")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = ClientRepo.GetClientById(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.OccupationId = new SelectList(OccupationRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("EditVIP")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,FormCollection form)
        {
            var existData = ClientRepo.GetClientById(id);
            
            if (TryUpdateModel(existData))
            {

                ClientRepo.UnitOfWork.Context.Entry(existData).State = EntityState.Modified;
                ClientRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.OccupationId = new SelectList(OccupationRepo.All(), "OccupationId", "OccupationName", existData.OccupationId);
            return View(existData);
        }

        // GET: Clients/Delete/5
        [Route("Delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = ClientRepo.GetClientById(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [Route("Delete/{id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = ClientRepo.GetClientById(id);
            ClientRepo.Delete(client);
            ClientRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }


        [Route("{id}/detail")]
        public ActionResult TestClientDetail(int id)
        {
            var orders = ClientRepo.GetClientById(id).Order.ToList();
            return View(orders);
        }

        public ActionResult BatchUpdate()
        {
            ViewData.Model = ClientRepo.All().Take(10).Select(x=>new ClientBatchUpdateViewModel() {
                ClientId=x.ClientId,
                FirstName=x.FirstName,
                LastName=x.LastName,
                MiddleName=x.MiddleName
            });
            return View();
        }

        [HttpPost]
        public ActionResult BatchUpdate(IList<ClientBatchUpdateViewModel> client)
        {
            if (!ModelState.IsValid)
            {
                ViewData.Model = ClientRepo.All().Take(10).Select(x => new ClientBatchUpdateViewModel()
                {
                    ClientId = x.ClientId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    MiddleName = x.MiddleName
                });
                return View();
            }
            foreach (var item in client)
            {
                var existData = ClientRepo.GetClientById(item.ClientId);
                existData.FirstName = item.FirstName;
                existData.MiddleName = item.MiddleName;
                existData.LastName = item.LastName;
            }
            ClientRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult OrderList(int id)
        {
            var orderList = ClientRepo.GetClientById(id).Order?.ToList();
            return PartialView("_OrderList", orderList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClientRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
