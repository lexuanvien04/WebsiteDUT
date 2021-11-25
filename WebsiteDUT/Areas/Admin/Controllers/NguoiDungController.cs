using ModelEF.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDUT.Common;
using WebsiteDUT.Model;

namespace WebsiteDUT.Areas.Admin.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: Admin/NguoiDung
        public ActionResult Index()
        {
            var session = (LoginModels)Session[Constants.USER_SESSION];
            if(session == null) return RedirectToAction("Index","Login");
            var user = new UserDao();
            var userlist = user.ListAll();
            return View(userlist);
        }
        public JsonResult ChangeStatus(long MaNguoiDung)
        {
            var result = new UserDao().ChangeStatus(MaNguoiDung);
            return Json(new
            {
                status = result
            });
        }
    }
}