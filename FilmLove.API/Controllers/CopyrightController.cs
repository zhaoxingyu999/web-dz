using FilmLove.Business.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJYSoft.YL.Common;

namespace FilmLove.API.Controllers
{
    public class CopyrightController : BaseController
    {
        CopyrightModel model = new CopyrightModel();
        public JsonResult GetInfo(int? id)
        {

            AjaxResult r = new AjaxResult();
            r.code = model.GetInfo(out object list, id);
            r.data = list;
            return Json(r.data, JsonRequestBehavior.AllowGet);
        }
    }
}