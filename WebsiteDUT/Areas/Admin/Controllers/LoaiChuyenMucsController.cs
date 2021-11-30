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
    public class LoaiChuyenMucsController : BaseController
    {
        private WebsiteDUTDbContext db = new WebsiteDUTDbContext();

        // GET: Admin/LoaiChuyenMucs
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
            var session = (LoginModels)Session[Constants.USER_SESSION];
            if (session == null) return RedirectToAction("Index", "Login");
            var user = new LoaiChuyenMucDao();
            var model = user.ListWhereAll(searchString, page, pagesize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
       

        // GET: Admin/LoaiChuyenMucs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiChuyenMuc loaiChuyenMuc = db.LoaiChuyenMucs.Find(id);
            if (loaiChuyenMuc == null)
            {
                return HttpNotFound();
            }
            return View(loaiChuyenMuc);
        }

        // GET: Admin/LoaiChuyenMucs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiChuyenMucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiChuyenMuc,TenLoaiChuyenMuc,DuongDan,TrangThai")] LoaiChuyenMuc loaiChuyenMuc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(loaiChuyenMuc.MaLoaiChuyenMuc))
                    {
                        SetAlert("Không được để trống!", "warning");
                        return View();
                    }
                    var dao = new LoaiChuyenMucDao();
                    string result;
                    
                    result = dao.Insert(loaiChuyenMuc);

                    if (result != null)
                    {
                        SetAlert("Tạo mới thành công!", "success");
                        return RedirectToAction("Index", "LoaiChuyenMucs");
                    }
                    else
                    {
                        SetAlert("Tạo mới thất bại!", "error");
                    }

                    db.LoaiChuyenMucs.Add(loaiChuyenMuc);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Common.Common.WriteLog("LoaiChuyenMucs", "Create-Post", ex.ToString());
            }

            return View(loaiChuyenMuc);
        }

        // GET: Admin/LoaiChuyenMucs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiChuyenMuc loaiChuyenMuc = db.LoaiChuyenMucs.Find(id);
            if (loaiChuyenMuc == null)
            {
                return HttpNotFound();
            }
            return View(loaiChuyenMuc);
        }

        // POST: Admin/LoaiChuyenMucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiChuyenMuc,TenLoaiChuyenMuc,DuongDan,TrangThai")] LoaiChuyenMuc loaiChuyenMuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiChuyenMuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiChuyenMuc);
        }

        // GET: Admin/LoaiChuyenMucs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiChuyenMuc loaiChuyenMuc = db.LoaiChuyenMucs.Find(id);
            if (loaiChuyenMuc == null)
            {
                return HttpNotFound();
            }
            return View(loaiChuyenMuc);
        }

        // POST: Admin/LoaiChuyenMucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LoaiChuyenMuc loaiChuyenMuc = db.LoaiChuyenMucs.Find(id);
            db.LoaiChuyenMucs.Remove(loaiChuyenMuc);
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
        public JsonResult LoaiChuyenMucTrangThai(string id)
        {
            var result = new LoaiChuyenMucDao().LoaiChuyenMucTrangThai(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
