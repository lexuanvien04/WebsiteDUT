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
    public class QuangCaosController : BaseController
    {
        private WebsiteDTUDbContext db = new WebsiteDTUDbContext();

        // GET: Admin/QuangCaos
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {

            var user = new QuangCaoDao();
            var model = user.ListWhereAll(searchString, page, pagesize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        // GET: Admin/QuangCaos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuangCao quangCao = db.QuangCaos.Find(id);
            if (quangCao == null)
            {
                return HttpNotFound();
            }
            return View(quangCao);
        }

        // GET: Admin/QuangCaos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QuangCaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaQuangCao,TenQuangCao,DuongDan,AnhDaiDien,NgayCapNhat,TrangThai")] QuangCao quangCao)
        {
            if (ModelState.IsValid)
            {
                db.QuangCaos.Add(quangCao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quangCao);
        }

        // GET: Admin/QuangCaos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuangCao quangCao = db.QuangCaos.Find(id);
            if (quangCao == null)
            {
                return HttpNotFound();
            }
            return View(quangCao);
        }

        // POST: Admin/QuangCaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaQuangCao,TenQuangCao,DuongDan,AnhDaiDien,NgayCapNhat,TrangThai")] QuangCao quangCao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quangCao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quangCao);
        }

        // GET: Admin/QuangCaos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuangCao quangCao = db.QuangCaos.Find(id);
            if (quangCao == null)
            {
                return HttpNotFound();
            }
            return View(quangCao);
        }

        // POST: Admin/QuangCaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            QuangCao quangCao = db.QuangCaos.Find(id);
            db.QuangCaos.Remove(quangCao);
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
