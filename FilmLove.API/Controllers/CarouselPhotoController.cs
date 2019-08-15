using FilmLove.Business.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJYSoft.YL.Common;

namespace FilmLove.API.Controllers
{
    public class CarouselPhotoController:BaseController
    {
        CarouselPhotoModel cpm = new CarouselPhotoModel();
        public JsonResult GetInfo(int? id)
        {

            AjaxResult r = new AjaxResult();
            r.code = cpm.GetInfo(out object list, id);
            r.data = list;
            return Json(r.data,JsonRequestBehavior.AllowGet);
        }
    }
}