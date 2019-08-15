using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilmLove.Admin.ManagerBusiness.SYSAdmin;

namespace FilmLove.Admin.Controllers
{
    public class WebHomeController : BaseController
    {
        WebSYSMenuManager menuManager = new WebSYSMenuManager();
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (CurAccount == null)
            {
                return RedirectToAction("Login", "WebEntrance");
            }
            ViewBag.CurAccount = CurAccount;

            //var menus = menuManager.GetMenus(CurAccount);
            return View("~/WebManager/Views/WebHome/Index.cshtml");
        }

        [AllowAnonymous]
        public ActionResult DashBoard()
        {
            return Content("欢迎进入管理系统");
        }

        [AllowAnonymous]
        public JsonResult LoadMenu()
        {
            var r = menuManager.LoadMenu(CurAccount);
            return Json(r);
        }
    }
}