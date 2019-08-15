using FilmLove.Business;
using FilmLove.Business.Entity.Request;
using FilmLove.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebManagers.Core;

namespace FilmLove.Admin.Controllers
{
    public class JobOffersController : BaseController
    {
        // GET: JobOffers
        #region 招聘工作
        JobOffersManager _JobOffersManager = new JobOffersManager();
        [MenuItemAttribute("基本管理", "工作招聘")]
        public ActionResult JobOffersList()
        {
            return View();
        }
        [MenuItemAttribute("基本管理", "工作招聘", "招聘编辑页面")]
        public ActionResult JobOffersEdit(int? id)
        {
            var ent = _JobOffersManager.GetJobOffersById(id);
            if (ent == null)
                ent = new JobOffers();
            return View(ent);
        }
        [MenuItemAttribute("基本管理", "工作招聘", "招聘列表加载")]
        public ActionResult JobOffersListLoad(JobOffersListReq req)
        {
            var r = _JobOffersManager.JobOffersListLoad(req);
            return Json(r);
        }
        [MenuItemAttribute("基本管理", "工作招聘", "招聘编辑保存")]
        [ValidateInput(false)]
        public JsonResult JobOffersListSave(JobOffers model)
        {
            var r = _JobOffersManager.JobOffersListSave(model);
            return Json(r);
        }
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Delete(int? id)
        {
            var ent = _JobOffersManager.GetJobOffersById(id);
            ent.IsActive = 0;
            var r = _JobOffersManager.JobOffersListSave(ent);
            return Json(r.msg, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}