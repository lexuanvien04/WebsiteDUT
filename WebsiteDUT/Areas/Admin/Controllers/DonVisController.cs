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
using WebsiteDUT.Common;
using WebsiteDUT.Model;

namespace WebsiteDUT.Areas.Admin.Controllers
{
    public class DonVisController : BaseController
    {
        private WebsiteDUTDbContext db = new WebsiteDUTDbContext();

        // GET: Admin/DonVis
        public ActionResult Index(string searchString, int page = 1, int pagesize = 10)
        {
           
            var user = new DonViDao();
            var model = user.ListWhereAll(searchString, page, pagesize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        // GET: Admin/DonVis/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonVi donVi = db.DonVis.Find(id);
            if (donVi == null)
            {
                return HttpNotFound();
            }
            return View(donVi);
        }

        // GET: Admin/DonVis/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiDonVi = new SelectList(db.LoaiDonVis, "MaloaiDonVi", "TenLoaiDonVi");
            return View();
        }

        // POST: Admin/DonVis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DonVi donVi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(donVi.MaDonVi))
                    {
                        SetAlert("Không được để trống!", "warning");
                        return View();
                    }
                    var dao = new DonViDao();
                    string result;
                    result = dao.Insert(donVi);

                    if (result != null)
                    {
                        SetAlert("Tạo mới thành công!", "success");
                        return RedirectToAction("Index", "DonVis");
                    }
                    else
                    {
                        SetAlert("Tạo mới thất bại!", "error");
                    }
                    db.DonVis.Add(donVi);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Common.Common.WriteLog("DonVis", "Create-Post", ex.ToString());
            }

            ViewBag.MaLoaiDonVi = new SelectList(db.LoaiDonVis, "MaloaiDonVi", "TenLoaiDonVi", donVi.MaLoaiDonVi);
            return View(donVi);
        }

        // GET: Admin/DonVis/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonVi donVi = db.DonVis.Find(id);
            if (donVi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiDonVi = new SelectList(db.LoaiDonVis, "MaloaiDonVi", "TenLoaiDonVi", donVi.MaLoaiDonVi);
            return View(donVi);
        }

        // POST: Admin/DonVis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonVi,MaLoaiDonVi,TenDonVi,TrangThai")] DonVi donVi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donVi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiDonVi = new SelectList(db.LoaiDonVis, "MaloaiDonVi", "TenLoaiDonVi", donVi.MaLoaiDonVi);
            return View(donVi);
        }

        // GET: Admin/DonVis/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonVi donVi = db.DonVis.Find(id);
            if (donVi == null)
            {
                return HttpNotFound();
            }
            return View(donVi);
        }

        // POST: Admin/DonVis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DonVi donVi = db.DonVis.Find(id);
            db.DonVis.Remove(donVi);
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
        [HttpPost]
        public JsonResult DonViTrangThai(string id)
        {
            var result = new DonViDao().DonViTrangThai(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
