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

namespace WebsiteDUT.Areas.Admin.Controllers
{
    public class NguoiDungsController : BaseController
    {
        private WebsiteDUTDbContext db = new WebsiteDUTDbContext();

        // GET: Admin/NguoiDungs
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {

            var user = new UserDao();
            var model = user.ListWhereAll(searchString, page, pagesize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        // GET: Admin/NguoiDungs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNguoiDung,HoTen,TenTruycap,MatKhau,NgayCapNhat,TrangThai")] NguoiDung nguoiDung)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(nguoiDung.MaNguoiDung))
                    {
                        SetAlert("Không được để trống!", "warning");
                        return View();
                    }
                    var dao = new UserDao();
                    if (dao.FindTenTruycap(nguoiDung.TenTruycap, nguoiDung.MaNguoiDung) != null)
                    {
                        SetAlert("tài khoản tồn tại!", "error");
                        return View();
                    }
                    var pass = Encryptor.EncryptorMD5(nguoiDung.MatKhau);
                    nguoiDung.MatKhau = pass;
                    db.NguoiDungs.Add(nguoiDung);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Common.Common.WriteLog("NguoiDungs", "Create-Post", ex.ToString());
            }
            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // POST: Admin/NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNguoiDung,HoTen,TenTruycap,MatKhau,NgayCapNhat,TrangThai")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                var pass = Encryptor.EncryptorMD5(nguoiDung.MatKhau);
                nguoiDung.MatKhau = pass;
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // POST: Admin/NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            db.NguoiDungs.Remove(nguoiDung);
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
        public JsonResult ChangeTrangThai(string id)
        {
            var result = new UserDao().ChangeTrangThai(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
