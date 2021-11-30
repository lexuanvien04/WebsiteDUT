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
    public class TagCouldsController : BaseController
    {
        private WebsiteDUTDbContext db = new WebsiteDUTDbContext();

        // GET: Admin/TagCoulds
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {

            var user = new TagCouldDao();
            var model = user.ListWhereAll(searchString, page, pagesize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        // GET: Admin/TagCoulds/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TagCould tagCould = db.TagCoulds.Find(id);
            if (tagCould == null)
            {
                return HttpNotFound();
            }
            return View(tagCould);
        }

        // GET: Admin/TagCoulds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TagCoulds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTagCould,TenCould,TrangThai")] TagCould tagCould)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(tagCould.MaTagCould))
                    {
                        SetAlert("Không được để trống!", "warning");
                        return View();
                    }
                    var dao = new TagCouldDao();
                    string result;

                    result = dao.Insert(tagCould);

                    if (result != null)
                    {
                        SetAlert("Tạo mới thành công!", "success");
                        return RedirectToAction("Index", "TagCoulds");
                    }
                    else
                    {
                        SetAlert("Tạo mới thất bại!", "error");
                    }

                    db.TagCoulds.Add(tagCould);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Common.Common.WriteLog("TagCoulds", "Create-Post", ex.ToString());
            }

            return View(tagCould);
        }

        // GET: Admin/TagCoulds/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TagCould tagCould = db.TagCoulds.Find(id);
            if (tagCould == null)
            {
                return HttpNotFound();
            }
            return View(tagCould);
        }

        // POST: Admin/TagCoulds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTagCould,TenCould,TrangThai")] TagCould tagCould)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tagCould).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tagCould);
        }

        // GET: Admin/TagCoulds/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TagCould tagCould = db.TagCoulds.Find(id);
            if (tagCould == null)
            {
                return HttpNotFound();
            }
            return View(tagCould);
        }

        // POST: Admin/TagCoulds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TagCould tagCould = db.TagCoulds.Find(id);
            db.TagCoulds.Remove(tagCould);
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
        public JsonResult TagCouldTrangThai(string id)
        {
            var result = new TagCouldDao().TagCouldTrangThai(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
