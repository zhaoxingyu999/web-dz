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
    public class CopyrightController : BaseController
    {
        // GET: Copyright
        CopyrightManager _copyrightManager = new CopyrightManager();
        [MenuItemAttribute("基本管理", "版权声明")]
        public ActionResult CopyrightList()
        {
            return View();
        }
        [MenuItemAttribute("基本管理", "版权声明", "编辑")]
        public ActionResult CopyrightEdit(int? id)
        {

            var ent = _copyrightManager.GetCopyrightById(id);
            if (ent == null)
                ent = new Copyright();
            return View(ent);
        }
        [MenuItemAttribute("基本管理", "版权声明", "保存")]
        [ValidateInput(false)]
        public JsonResult CopyrightListSave(Copyright model)
        {
            var r = _copyrightManager.CopyrightListSave(model);
            return Json(r);
        }
         [MenuItemAttribute("基本管理", "版权声明", "加载")]
        public JsonResult CopyrightListLoad(CarouselPhotoListReq req)
        {
            var r = _copyrightManager.CopyrightSListLoad(req);
            return Json(r, JsonRequestBehavior.AllowGet);
        }
    }
}