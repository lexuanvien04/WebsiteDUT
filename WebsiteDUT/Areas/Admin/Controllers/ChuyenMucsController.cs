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
    public class ChuyenMucsController : BaseController
    {
        private WebsiteDUTDbContext db = new WebsiteDUTDbContext();

        // GET: Admin/ChuyenMucs
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
          
            var user = new ChuyenMucDao();
            var model = user.ListWhereAll(searchString, page, pagesize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        // GET: Admin/ChuyenMucs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenMuc chuyenMuc = db.ChuyenMucs.Find(id);
            if (chuyenMuc == null)
            {
                return HttpNotFound();
            }
            return View(chuyenMuc);
        }

        // GET: Admin/ChuyenMucs/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiChuyenMuc = new SelectList(db.LoaiChuyenMucs, "MaLoaiChuyenMuc", "TenLoaiChuyenMuc");
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "HoTen");
            return View();
        }

        // POST: Admin/ChuyenMucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChuyenMuc chuyenMuc, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(chuyenMuc.MaChuyenMuc))
                    {
                        SetAlert("Không được để trống!", "warning");
                        return View();
                    }
                    var dao = new ChuyenMucDao();
                    string result;
                    image = Request.Files["ImageData"];
                    if (image != null && image.ContentLength > 0)
                    {
                        chuyenMuc.AnhDaiDien = new byte[image.ContentLength]; // image stored-in binary formate
                        image.InputStream.Read(chuyenMuc.AnhDaiDien, 0, image.ContentLength);
                        string fileName = System.IO.Path.GetFileName(image.FileName);
                        string urlImage = Server.MapPath("~/Assets/Image/" + fileName);
                        image.SaveAs(urlImage);

                    }
                    result = dao.Insert(chuyenMuc);

                    if (result != null)
                    {
                        SetAlert("Tạo mới thành công!", "success");
                        return RedirectToAction("Index", "ChuyenMucs");
                    }
                    else
                    {
                        SetAlert("Tạo mới thất bại!", "error");
                    }
                    db.ChuyenMucs.Add(chuyenMuc);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Common.Common.WriteLog("ChuyenMucs", "Create-Post", ex.ToString());
            }

            ViewBag.MaLoaiChuyenMuc = new SelectList(db.LoaiChuyenMucs, "MaLoaiChuyenMuc", "TenLoaiChuyenMuc", chuyenMuc.MaLoaiChuyenMuc);
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "HoTen", chuyenMuc.MaNguoiDung);
            return View(chuyenMuc);
        }

        // GET: Admin/ChuyenMucs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenMuc chuyenMuc = db.ChuyenMucs.Find(id);
            if (chuyenMuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiChuyenMuc = new SelectList(db.LoaiChuyenMucs, "MaLoaiChuyenMuc", "TenLoaiChuyenMuc", chuyenMuc.MaLoaiChuyenMuc);
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "HoTen", chuyenMuc.MaNguoiDung);
            return View(chuyenMuc);
        }

        // POST: Admin/ChuyenMucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChuyenMuc chuyenMuc, HttpPostedFileBase editImage)
        {
            if (ModelState.IsValid)
            {
                if (editImage != null && editImage.ContentLength > 0)
                {
                    chuyenMuc.AnhDaiDien = new byte[editImage.ContentLength]; // image stored in binary fomate 
                    editImage.InputStream.Read(chuyenMuc.AnhDaiDien, 0, editImage.ContentLength);
                    string fileName = System.IO.Path.GetFileName(editImage.FileName);
                    string urlImage = Server.MapPath("~/Assets/Image/" + fileName);
                    editImage.SaveAs(urlImage);
                }
                db.Entry(chuyenMuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiChuyenMuc = new SelectList(db.LoaiChuyenMucs, "MaLoaiChuyenMuc", "TenLoaiChuyenMuc", chuyenMuc.MaLoaiChuyenMuc);
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "HoTen", chuyenMuc.MaNguoiDung);
            return View(chuyenMuc);
        }

        // GET: Admin/ChuyenMucs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenMuc chuyenMuc = db.ChuyenMucs.Find(id);
            if (chuyenMuc == null)
            {
                return HttpNotFound();
            }
            return View(chuyenMuc);
        }

        // POST: Admin/ChuyenMucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChuyenMuc chuyenMuc = db.ChuyenMucs.Find(id);
            db.ChuyenMucs.Remove(chuyenMuc);
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
        public JsonResult ChuyenMucTrangThai(string id)
        {
            var result = new ChuyenMucDao().ChuyenMucTrangThai(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
