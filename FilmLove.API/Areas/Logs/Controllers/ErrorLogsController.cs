using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJYSoft.YL.Common;

namespace FilmLove.API.Areas.Logs.Controllers
{
	public class ErrorLogsController : Controller
	{
		// GET: Logs/ErrorLogs
		public ActionResult Index()
		{
			return View();
		}

		public JsonResult List(PageModel model)
		{
			Models.ErrorLogsModel errorLogsModel = new Models.ErrorLogsModel();
			var page = errorLogsModel.List(model);
			return Json(page);
		}

	}
}