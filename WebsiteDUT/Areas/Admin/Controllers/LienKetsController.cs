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
        private WebsiteDUTDbContext db = new WebsiteDUTDbContext();

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
        public ActionResult Create(LienKet lienKet, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(lienKet.MaLienKet))
                    {
                        SetAlert("Không được để trống!", "warning");
                        return View();
                    }
                    var dao = new LienKetDao();
                    string result;
                    image = Request.Files["ImageData"];
                    if (image != null && image.ContentLength > 0)
                    {
                        lienKet.AnhDaiDien = new byte[image.ContentLength]; // image stored-in binary formate
                        image.InputStream.Read(lienKet.AnhDaiDien, 0, image.ContentLength);
                        string fileName = System.IO.Path.GetFileName(image.FileName);
                        string urlImage = Server.MapPath("~/Assets/Image/" + fileName);
                        image.SaveAs(urlImage);

                    }
                    result = dao.Insert(lienKet);

                    if (result != null)
                    {
                        SetAlert("Tạo mới thành công!", "success");
                        return RedirectToAction("Index", "LienKets");
                    }
                    else
                    {
                        SetAlert("Tạo mới thất bại!", "error");
                    }

                    db.LienKets.Add(lienKet);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Common.Common.WriteLog("LienKets", "Create-Post", ex.ToString());
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
        public ActionResult Edit(LienKet lienKet, HttpPostedFileBase editImage)
        {
           
            if (ModelState.IsValid)
            {
                    if (editImage != null && editImage.ContentLength > 0)
                    {
                        lienKet.AnhDaiDien = new byte[editImage.ContentLength]; // image stored in binary fomate 
                        editImage.InputStream.Read(lienKet.AnhDaiDien, 0, editImage.ContentLength);
                        string fileName = System.IO.Path.GetFileName(editImage.FileName);
                        string urlImage = Server.MapPath("~/Assets/Image/" + fileName);
                        editImage.SaveAs(urlImage);
                    }

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
