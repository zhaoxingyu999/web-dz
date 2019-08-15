using FilmLove.Business.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJYSoft.YL.Common;
namespace FilmLove.API.Controllers
{
    public class JobOffersController:BaseController
    {
        JobOffersModel jobmodel = new JobOffersModel();
        public JsonResult GetInfo()
        {
            AjaxResult r = new AjaxResult();
            r.code = jobmodel.GetInfo(out object list);
            r.data = list;
            return Json(r.data, JsonRequestBehavior.AllowGet);
        }
    }
}