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
    public class CarouselPhotoController : BaseController
    {
        // GET: CarouselPhoto
        #region 度载动态
        CarouselPhotoManager _CarouselPhotoManager = new CarouselPhotoManager();
        [MenuItemAttribute("基本管理", "度载动态")]
        public ActionResult CarouselPhotoList()
        {
            return View();
        }
        [MenuItemAttribute("基本管理", "度载动态", "动态编辑页面")]
        public ActionResult CarouselPhotoEdit(int? id)
        {

            var ent = _CarouselPhotoManager.GetCarouselPhotoById(id);
            if (ent == null)
                ent = new CarouselPhoto();
            return View(ent);
        }
        [MenuItemAttribute("基本管理", "度载动态", "动态列表加载")]
        public ActionResult CarouselPhotoListLoad(CarouselPhotoListReq req)
        {
            var r = _CarouselPhotoManager.CarouselPhotoListLoad(req);
            return Json(r);
        }
        [MenuItemAttribute("基本管理", "度载动态", "动态编辑保存")]
        [ValidateInput(false)]
        public JsonResult CarouselPhotoListSave(CarouselPhoto model)
        {
            var r = _CarouselPhotoManager.CarouselPhotoListSave(model);
            return Json(r);
        }
        public JsonResult Delete(int? id)
        {
            var ent = _CarouselPhotoManager.GetCarouselPhotoById(id);
            ent.IsActive = 0;
            var r = _CarouselPhotoManager.CarouselPhotoListSave(ent);
            return Json(r.msg, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UploadIMG()
        {
            
            HttpPostedFileBase file = Request.Files["file"];
            if (file != null)
            {
                try
                {
                    var filename = Path.Combine(Request.MapPath("~/filmlove/Upload"), file.FileName);
                    file.SaveAs(filename);
                    return Content("上传成功");
                }
                catch (Exception ex)
                {
                    return Content(string.Format("上传文件出现异常：{0}", ex.Message));
                }

            }
            else
            {
                return Content("没有文件需要上传！");
            }
        }
        #endregion
    }
}