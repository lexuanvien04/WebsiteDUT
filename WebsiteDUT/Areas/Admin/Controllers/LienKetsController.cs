using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModelEF.DAO;
using ModelEF.EF;

namespace WebsiteDUT.Areas.Admin.Controllers
{
    public class LienKetsController : BaseController
    {
        private WebsiteDTUDbContext db = new WebsiteDTUDbContext();

        // GET: Admin/LienKets
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {

            var user = new LienKetDao();
            var model = user.ListWhereAll(searchString, page, pagesize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        // GET: Admin/LienKets/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienKet lienKet = db.LienKets.Find(id);
            if (lienKet == null)
            {
                return HttpNotFound();
            }
            return View(lienKet);
        }

        // GET: Admin/LienKets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LienKets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLienKet,AnhDaiDien,Duongdan,TrangThai")] LienKet lienKet)
        {
            if (ModelState.IsValid)
            {
                db.LienKets.Add(lienKet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lienKet);
        }

        // GET: Admin/LienKets/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienKet lienKet = db.LienKets.Find(id);
            if (lienKet == null)
            {
                return HttpNotFound();
            }
            return View(lienKet);
        }

        // POST: Admin/LienKets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLienKet,AnhDaiDien,Duongdan,TrangThai")] LienKet lienKet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lienKet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lienKet);
        }

        // GET: Admin/LienKets/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienKet lienKet = db.LienKets.Find(id);
            if (lienKet == null)
            {
                return HttpNotFound();
            }
            return View(lienKet);
        }

        // POST: Admin/LienKets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LienKet lienKet = db.LienKets.Find(id);
            db.LienKets.Remove(lienKet);
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
    }
}
