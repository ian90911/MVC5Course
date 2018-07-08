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
    public class ProductsController : BaseController
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: Products
        public ActionResult Index()
        {
            var list = db.Product.Take(10).OrderByDescending(x => x.ProductId).ToList();
            return View(list);
        }

        // GET: Products
        public ActionResult Index2()
        {
            var list = db.Product
                .Where(x => x.Active == true)
                .Take(10)
                .OrderByDescending(x => x.ProductId)
                .Select(x => new ProductViewModel()
                {
                    Active = x.Active,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Stock = x.Stock
                });
            return View(list);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AddNewProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var newData = new Product() {
                Active=model.Active,
                Price=model.Price,
                ProductId=model.ProductId,
                ProductName=model.ProductName,
                Stock=model.Stock
            };
            db.Product.Add(newData);
            db.SaveChanges();

            return RedirectToAction("Index2");
        }

        public ActionResult EditOldProduct(int id)
        {
            var existData = db.Product.FirstOrDefault(x => x.ProductId == id);
            if (existData == null)
            {
                throw new NullReferenceException("找不到資料");
            }
            var existDataViewModel = new ProductViewModel() {
                ProductId=existData.ProductId,
                Active=existData.Active,
                Price=existData.Price,
                ProductName=existData.ProductName,
                Stock=existData.Stock
            };
            return View(existDataViewModel);
        }

        [HttpPost]
        public ActionResult EditOldProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existData = db.Product.FirstOrDefault(x => x.ProductId == model.ProductId);
            if (existData == null)
            {
                throw new NullReferenceException("找不到資料");
            }
            existData.Price = model.Price;
            existData.ProductName = model.ProductName;
            existData.Stock = model.Stock;
            existData.Active = model.Active;
            int result=db.SaveChanges();
            if (result <= 0)
            {
                throw new ArgumentException("更新失敗");
            }
            return RedirectToAction("Index2");
        }

        public ActionResult DeleteOldProduct(int id)
        {
            var existData = db.Product.FirstOrDefault(x => x.ProductId == id);
            if (existData == null)
            {
                throw new NullReferenceException("找不到資料");
            }
            var existDataViewModel = new ProductViewModel()
            {
                ProductId = existData.ProductId,
                Active = existData.Active,
                Price = existData.Price,
                ProductName = existData.ProductName,
                Stock = existData.Stock
            };
            return View(existDataViewModel);
        }

        [HttpPost, ActionName("DeleteOldProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOldProductConfirmed(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existData = db.Product.FirstOrDefault(x => x.ProductId == id);
            if (existData == null)
            {
                throw new NullReferenceException("找不到資料");
            }
            db.Product.Remove(existData);
            int result = db.SaveChanges();
            if (result <= 0)
            {
                throw new ArgumentException("刪除失敗");
            }
            return RedirectToAction("Index2");
        }
    }
}
