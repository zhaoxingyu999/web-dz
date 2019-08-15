using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJYSoft.YL.Common;

namespace FilmLove.Admin.Areas.Logs.Controllers
{
    [AllowAnonymousAttribute]
    public class OperLogsController : Controller
    {
        // GET: Logs/OperLogs
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List(PageModel model)
        {
            Models.OperLogsModel operLogsModel = new Models.OperLogsModel();
            var page = operLogsModel.List(model);
            return Json(page);
        }

    }
}