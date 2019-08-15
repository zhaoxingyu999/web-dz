using FilmLove.Business;
using FilmLove.Business.Entity.Request;
using FilmLove.Database.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebManagers.Core;
using YJYSoft.YL.Common;

namespace FilmLove.Admin.Controllers
{
    public class AboutUSController : BaseController
    {
        // GET: AboutUS
        AboutUSManager _abountUSManager = new AboutUSManager();
        [MenuItemAttribute("基本管理", "关于我们_企业使命")]
        public ActionResult AboutUSList()
        {
            return View();
        }
        [MenuItemAttribute("基本管理", "关于我们_企业使命","编辑")]
        public ActionResult AboutUSEdit(int? id)
        {

            var ent = _abountUSManager.GetAboutUsById(id);
            if (ent == null)
                ent = new AboutUs();
            return View(ent);
        }
        [MenuItemAttribute("基本管理", "关于我们_企业使命", "保存")]
        [ValidateInput(false)]
        public JsonResult AboutUSListSaveData(AboutUs model)
        {
            var r = _abountUSManager.AboutUSListSave(model);
            return Json(r);
        }
        [MenuItemAttribute("基本管理", "关于我们_企业使命", "加载")]
        public JsonResult AboutUSListLoad(CarouselPhotoListReq req)
        {
            var r = _abountUSManager.AboutUSListLoad(req);
            return Json(r, JsonRequestBehavior.AllowGet);
        }
    }
}