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
    public class QuyenNguoiDungsController : BaseController
    {
        private WebsiteDUTDbContext db = new WebsiteDUTDbContext();

        // GET: Admin/QuyenNguoiDungs
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
            var user = new QuyenNguoiDungDao();
            var model = user.ListWhereAll(searchString, page, pagesize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        // GET: Admin/QuyenNguoiDungs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyenNguoiDung quyenNguoiDung = db.QuyenNguoiDungs.Find(id);
            if (quyenNguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(quyenNguoiDung);
        }

        // GET: Admin/QuyenNguoiDungs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QuyenNguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaQuyen,TenQuyen")] QuyenNguoiDung quyenNguoiDung)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(quyenNguoiDung.MaQuyen))
                    {
                        SetAlert("Không được để trống!", "warning");
                        return View();
                    }
                    var dao = new QuyenNguoiDungDao();
                    string result;

                    result = dao.Insert(quyenNguoiDung);

                    if (result != null)
                    {
                        SetAlert("Tạo mới thành công!", "success");
                        return RedirectToAction("Index", "QuyenNguoiDungs");
                    }
                    else
                    {
                        SetAlert("Tạo mới thất bại!", "error");
                    }

                    db.QuyenNguoiDungs.Add(quyenNguoiDung);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Common.Common.WriteLog("QuyenNguoiDungs", "Create-Post", ex.ToString());
            }

            return View(quyenNguoiDung);
        }

        // GET: Admin/QuyenNguoiDungs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyenNguoiDung quyenNguoiDung = db.QuyenNguoiDungs.Find(id);
            if (quyenNguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(quyenNguoiDung);
        }

        // POST: Admin/QuyenNguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaQuyen,TenQuyen")] QuyenNguoiDung quyenNguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quyenNguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quyenNguoiDung);
        }

        // GET: Admin/QuyenNguoiDungs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyenNguoiDung quyenNguoiDung = db.QuyenNguoiDungs.Find(id);
            if (quyenNguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(quyenNguoiDung);
        }

        // POST: Admin/QuyenNguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            QuyenNguoiDung quyenNguoiDung = db.QuyenNguoiDungs.Find(id);
            db.QuyenNguoiDungs.Remove(quyenNguoiDung);
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
