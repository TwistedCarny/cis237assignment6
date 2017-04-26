using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cis237Assignment6.Models;
using System.Data.Entity.Infrastructure;

namespace cis237Assignment6.Controllers
{
    [Authorize]
    public class BeveragesController : Controller
    {
        private BeverageWCurtisEntities db = new BeverageWCurtisEntities();

        // GET: Beverages
        public ActionResult Index()
        {
            DbSet<Beverage> beveragesToFilter = db.Beverages;

            string filterName = "";
            string filterMin = "";
            string filterMax = "";

            int min = 0;
            int max = 200;

            if (Session["name"] != null && !string.IsNullOrWhiteSpace((string)Session["name"]))
            {
                filterName = (string)Session["name"];
            }

            if (Session["min"] != null && !string.IsNullOrWhiteSpace((string)Session["min"]))
            {
                filterMin = (string)Session["min"];
                min = int.Parse(filterMin);
            }

            if (Session["max"] != null && !string.IsNullOrWhiteSpace((string)Session["max"]))
            {
                filterMax = (string)Session["max"];
                max = int.Parse(filterMax);
            }

            IEnumerable<Beverage> filtered = beveragesToFilter.Where(beverage => beverage.price >= min &&
                                                                    beverage.price <= max &&
                                                                    beverage.name.Contains(filterName));
            IEnumerable<Beverage> finalFiltered = filtered.ToList();

            ViewBag.filterName = filterName;
            ViewBag.filterMin = filterMin;
            ViewBag.filterMax = filterMax;

            return View(finalFiltered);
        }

        // GET: Beverages/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // GET: Beverages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beverages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,pack,price,active")] Beverage beverage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Beverages.Add(beverage);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException ex)
            {
                return RedirectToAction("ItemExists");
            }

            return View(beverage);
        }

        // GET: Beverages/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // POST: Beverages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,pack,price,active")] Beverage beverage)
        {

            if (ModelState.IsValid)
            {
                db.Entry(beverage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            return View(beverage);
        }

        // GET: Beverages/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // POST: Beverages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Beverage beverage = db.Beverages.Find(id);
            db.Beverages.Remove(beverage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Filter()
        {
            Session["name"] = Request.Form.Get("name");
            Session["min"] = Request.Form.Get("min");
            Session["max"] = Request.Form.Get("max");

            return RedirectToAction("Index");
        }

        public ActionResult ItemExists()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
