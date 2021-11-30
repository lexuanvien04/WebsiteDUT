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
        private WebsiteDUTDbContext db = new WebsiteDUTDbContext();

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
        public ActionResult Create(QuangCao quangCao, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(quangCao.MaQuangCao))
                    {
                        SetAlert("Không được để trống!", "warning");
                        return View();
                    }
                    var dao = new QuangCaoDao();
                    string result;
                    image = Request.Files["ImageData"];
                    if (image != null && image.ContentLength > 0)
                    {
                        quangCao.AnhDaiDien = new byte[image.ContentLength]; // image stored-in binary formate
                        image.InputStream.Read(quangCao.AnhDaiDien, 0, image.ContentLength);
                        string fileName = System.IO.Path.GetFileName(image.FileName);
                        string urlImage = Server.MapPath("~/Assets/Image/" + fileName);
                        image.SaveAs(urlImage);

                    }
                    result = dao.Insert(quangCao);

                    if (result != null)
                    {
                        SetAlert("Tạo mới thành công!", "success");
                        return RedirectToAction("Index", "QuangCaos");
                    }
                    else
                    {
                        SetAlert("Tạo mới thất bại!", "error");
                    }

                    db.QuangCaos.Add(quangCao);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Common.Common.WriteLog("QuangCaos", "Create-Post", ex.ToString());
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
        public ActionResult Edit(QuangCao quangCao, HttpPostedFileBase editImage)
        {
            if (ModelState.IsValid)
            {

                if (editImage != null && editImage.ContentLength > 0)
                {
                    quangCao.AnhDaiDien = new byte[editImage.ContentLength]; // image stored in binary fomate 
                    editImage.InputStream.Read(quangCao.AnhDaiDien, 0, editImage.ContentLength);
                    string fileName = System.IO.Path.GetFileName(editImage.FileName);
                    string urlImage = Server.MapPath("~/Assets/Image/" + fileName);
                    editImage.SaveAs(urlImage);
                }
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
        [HttpPost]
        public JsonResult QuangCaoTrangThai(string id)
        {
            var result = new QuangCaoDao().QuangCaoTrangThai(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
