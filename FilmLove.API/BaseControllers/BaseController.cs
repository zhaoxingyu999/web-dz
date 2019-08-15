using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJYSoft.YL.Common;
using System.IO;
using System.Data.SQLite;
using DB.SQLITE;
using FilmLove.API;
using FilmLove.Common;
using FilmLove.Database.Entity;

namespace FilmLove.API.Controllers
{
    public class BaseController : JsonController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string ControllerName = RouteData.Values["controller"].ToString().ToLower();
            string ActionName = RouteData.Values["action"].ToString().ToLower();
            if (!ModelState.IsValid)
            {
                string error = "";
                foreach (var m in ModelState)
                {
                    foreach (var e in ModelState[m.Key].Errors)
                    {
                        if (e.ErrorMessage != "")
                        {
                            error += e.ErrorMessage + "\r\n";
                        }
                        else
                        {
                            error += m.Key;
                            Exception ex = e.Exception;
                            while (ex.InnerException != null)
                                ex = ex.InnerException;
                            error += " " + ex.Message;
                        }
                    }
                }
                AjaxResult v = new AjaxResult
                {
                    code = 2001002,
                    msg = error
                };
                JsonResult jr = new JsonResult();
                jr.Data = v;
                jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = jr;
                LogFileTool.WriteLog(string.Format("Log/RequestVal/{0}/{1}/", ControllerName, ActionName), error);
                return;
            }

     
            #region 记录请求日志
            try
            {
                string vv = "";
                if (Request.QueryString.AllKeys.Length > 0 || Request.Form.AllKeys.Length > 0)
                {
                    vv = "Request.QueryString\r\n";
                    foreach (var key in Request.QueryString.AllKeys)
                    {
                        string a = Request.QueryString[key];

                        if (a.Length > 500)
                            a = a.Substring(0, 500);
                        vv += key + ":" + a + "\r\n";
                    }
                    vv += "Request.Form\r\n";
                    foreach (var key in Request.Form.AllKeys)
                    {
                        string a = Request.Form[key];
                        if (a.Length > 500)
                            a = a.Substring(0, 500);
                        vv += key + ":" + a + "\r\n";
                    }
                    vv += "Request.Headers\r\n";
                    foreach (var key in Request.Headers.AllKeys)
                    {
                        if (key == "Cookie") continue;
                        string a = Request.Headers[key];
                        if (a.Length > 500)
                            a = a.Substring(0, 500);
                        vv += key + ":" + Request.Headers[key] + "\r\n";
                    }
                    LogFileTool.WriteLog(string.Format("Log/Request/{0}/{1}/", ControllerName, ActionName), vv.ToString());

                }
                else if (string.Compare(Request.ContentType, "application/json", true) == 0)
                {
                    vv = "application/json\r\n";
                    Request.InputStream.Seek(0, SeekOrigin.Begin);
                    StreamReader reader = new StreamReader(Request.InputStream);
                    vv += reader.ReadToEnd();
                    LogFileTool.WriteLog(string.Format("Log/Request/{0}/{1}/", ControllerName, ActionName), vv.ToString());
                }
            }
            catch { }
            #endregion

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 返回AllowGet JSON
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult AllowGetJson(object data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}