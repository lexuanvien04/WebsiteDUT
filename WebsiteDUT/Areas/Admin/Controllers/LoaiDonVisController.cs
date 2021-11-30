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
    public class LoaiDonVisController : BaseController
    {
        private WebsiteDUTDbContext db = new WebsiteDUTDbContext();

        // GET: Admin/LoaiDonVis
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
            
            var user = new LoaiDonViDao();
            var model = user.ListWhereAll(searchString, page, pagesize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        // GET: Admin/LoaiDonVis/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiDonVi loaiDonVi = db.LoaiDonVis.Find(id);
            if (loaiDonVi == null)
            {
                return HttpNotFound();
            }
            return View(loaiDonVi);
        }

        // GET: Admin/LoaiDonVis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiDonVis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaloaiDonVi,TenLoaiDonVi,TrangThai")] LoaiDonVi loaiDonVi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(loaiDonVi.MaloaiDonVi))
                    {
                        SetAlert("Không được để trống!", "warning");
                        return View();
                    }
                    var dao = new LoaiDonViDao();
                    string result;

                    result = dao.Insert(loaiDonVi);

                    if (result != null)
                    {
                        SetAlert("Tạo mới thành công!", "success");
                        return RedirectToAction("Index", "LoaiDonVis");
                    }
                    else
                    {
                        SetAlert("Tạo mới thất bại!", "error");
                    }

                    db.LoaiDonVis.Add(loaiDonVi);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Common.Common.WriteLog("LoaiDonVis", "Create-Post", ex.ToString());
            }

            return View(loaiDonVi);
        }

        // GET: Admin/LoaiDonVis/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiDonVi loaiDonVi = db.LoaiDonVis.Find(id);
            if (loaiDonVi == null)
            {
                return HttpNotFound();
            }
            return View(loaiDonVi);
        }

        // POST: Admin/LoaiDonVis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaloaiDonVi,TenLoaiDonVi,TrangThai")] LoaiDonVi loaiDonVi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiDonVi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiDonVi);
        }

        // GET: Admin/LoaiDonVis/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiDonVi loaiDonVi = db.LoaiDonVis.Find(id);
            if (loaiDonVi == null)
            {
                return HttpNotFound();
            }
            return View(loaiDonVi);
        }

        // POST: Admin/LoaiDonVis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LoaiDonVi loaiDonVi = db.LoaiDonVis.Find(id);
            db.LoaiDonVis.Remove(loaiDonVi);
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
        public JsonResult LoaiDonViTrangThai(string id)
        {
            var result = new LoaiDonViDao().LoaiDonViTrangThai(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
