using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using YJYSoft.YL.Common;
using FilmLove.Admin.ManagerBusiness.Common;
using YJYSoft.YL.Common.WebHelper;
using FilmLove.Admin.CommEntity.Entity;
using FilmLove.Admin.ManagerBusiness.SYSAdmin;
using System.Data.SQLite;
using FilmLove.Admin;
using FilmLove.Admin.WebManager.Models;
using DB.SQLITE;
using System.Diagnostics;
using WebManagers.Core;
using WebManagers.Core.Entity;

namespace FilmLove.Admin
{
    class AuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var skipAuthorization =
                filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute),
                    inherit: true);
            if (skipAuthorization)
                return;

            bool isAjax = IsAjaxPage(filterContext);
            var CurAccount = AdminUser.CurAccount;
            if (CurAccount == null)
            {
                DoMainLogout(filterContext, isAjax);
                return;
            }
            RecordLog(filterContext, CurAccount);
            bool result = AuthorizeCheck(filterContext, CurAccount, isAjax);
            if (result == false)
                return;
            base.OnActionExecuting(filterContext);
        }

        protected bool IsAjaxPage(ActionExecutingContext filterContext)
        {
            string ActionName = filterContext.ActionDescriptor.ActionName;
            bool isAjax = false;
            var methods = filterContext.Controller.GetType().GetMethods().ToList();
            foreach (var method in methods)
            {
                if (method.ReturnType != typeof(JsonResult))
                    continue;
                if (method.Name.ToLower() == ActionName.ToLower())
                {
                    isAjax = true;
                    break;
                }
            }
            return isAjax;
        }

        protected void DoMainLogout(ActionExecutingContext filterContext, bool IsAjax)
        {
            var Cookie = AuthenticationHelper.GetAuthTicket(ALLKeys.C_ADMINUSER);
            if (Cookie != null)
                AuthenticationHelper.SignOut(ALLKeys.C_ADMINUSER);
            if (IsAjax)
            {
                JsonResult jr = new JsonResult();
                jr.Data = new AjaxResult("账户权限失效", -1);
                jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = jr;
                return;
            }
            else
            {
                UrlHelper Url = new UrlHelper(filterContext.RequestContext);
                filterContext.RequestContext.HttpContext.Response.
                    Write(string.Format("<script>top.window.location.href='{0}';</script>", Url.Action("Logout", "WebEntrance")));
                filterContext.RequestContext.HttpContext.Response.End();
                return;
            }
        }

        private void RecordLog(ActionExecutingContext filterContext, SysManager user)
        {
            var Request = filterContext.RequestContext.HttpContext.Request;
            //StringBuilder sb = new StringBuilder();
            //sb.Append("============================================================ \r\n");
            //sb.Append(string.Format("请求时间：{0:yyyy-MM-dd HH:mm:ss.fff}\r\n", DateTime.Now));
            //sb.Append("操作人：" + CurAccount.ManagerName + "\r\n");
            //sb.Append("用户ID：" + CurAccount.ManagerId + "\r\n");
            //sb.Append("请求参数：\r\n" + GetRequestString() + "\r\n");
            //LogFileTool.WriteLog(string.Format("Log/Request/{0}/{1}/", ControllerName, ActionName), sb.ToString());
            try
            {
                SQLiteParameter URL = new SQLiteParameter("URL", Request.Url.ToString());
                SQLiteParameter UserInfo = new SQLiteParameter("UserInfo", Newtonsoft.Json.JsonConvert.SerializeObject(user));
                SQLiteParameter CreateTime = new SQLiteParameter("CreateTime", DateTime.Now);
                SQLiteParameter IP = new SQLiteParameter("IP", MvcApplication.GetIP());
                if (Request.QueryString.AllKeys.Length > 0 || Request.Form.AllKeys.Length > 0)
                {
                    var vv = "Request.QueryString\r\n";
                    foreach (var key in Request.QueryString.AllKeys)
                    {
                        vv += key + ":" + Request.QueryString[key] + "\r\n";
                    }
                    vv += "Request.Form\r\n";
                    foreach (var key in Request.Form.AllKeys)
                    {
                        vv += key + ":" + Request.Form[key] + "\r\n";
                    }
                    vv += "Request.Headers\r\n";
                    foreach (var key in Request.Headers.AllKeys)
                    {
                        if (key == "Cookie") continue;
                        vv += key + ":" + Request.Headers[key] + "\r\n";
                    }
                    SQLiteParameter Param = new SQLiteParameter("Param", vv);
                    var c = SqliteAdoSessionManager.Current;
                    var vvc = c.OperLogsDB.CreateSQLiteCommand(@"INSERT INTO OperLogs (
                             URL,
                             Param,
                             UserInfo,
                             CreateTime,
                             IP
                         )
                         VALUES (
                             @URL,
                             @Param,
                             @UserInfo,
                             @CreateTime,
                             @IP
                         );", new SQLiteParameter[] { URL, Param, UserInfo, CreateTime, IP });
                    c.OperLogsDB.ExecuteNonQuery(vvc);
                }
            }
            catch { }
        }

        private bool AuthorizeCheck(ActionExecutingContext filterContext, SysManager user, bool isAjax)
        {
            if (user.IsSupper == 1)
                return true;
            var CurAuthPages = AdminUser.CurAuthPages;

            string ActionName = filterContext.ActionDescriptor.ActionName;
            string ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string thisUrl = string.Format("/{0}/{1}", ControllerName, ActionName).ToLower();
            var autoPage = CurAuthPages.FirstOrDefault(m => m.PageUrl.ToLower() == thisUrl);
            if (autoPage == null)
            {
                if (isAjax)
                {
                    JsonResult jr = new JsonResult();
                    jr.Data = new AjaxResult("你无访问权限" + thisUrl);
                    jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    filterContext.Result = jr;
                }
                else
                {
                    filterContext.RequestContext.HttpContext.Response.Write("你无访问权限" + thisUrl);
                    filterContext.RequestContext.HttpContext.Response.End();
                }
                return false;
            }
            return true;
        }
    }

    public class AdminUser
    {
        public static SysManager CurAccount
        {
            get
            {
                SysManager account = null;
                account = SysLoginInfo.CurAccount();
                if (account != null)
                    return account;
                if (!AuthenticationHelper.CheckAuthorization(ALLKeys.C_ADMINUSER))
                    return null;
                var aa = AuthenticationHelper.GetAuthTicket(ALLKeys.C_ADMINUSER);
                if (aa == null)
                    return null;
                var cookieInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<AdminCookieInfo>(aa.UserData);
                if (cookieInfo == null)
                    return null;
                WebSYSAccountManager accountManager = new WebSYSAccountManager();
                var user = accountManager.GetAccountInfoByID(cookieInfo.ManagerId);
                if (user == null)
                    return null;
                if (cookieInfo.LoginToken != user.CurToken)
                    return null;
                SysManager manager = new SysManager()
                {
                    IsSupper = user.IsSupper,
                    ManagerId = user.ManagerId,
                    ManagerName = user.ManagerName,
                    ManagerRealname = user.ManagerRealname,
                };
                SysLoginInfo.SetCurAccount(manager);
                return manager;
            }
        }

        public static List<SysMenuPage> CurAuthPages
        {
            get
            {
                List<SysMenuPage> list = null;
                list = SysLoginInfo.CurMenuPages();
                if (list != null)
                    return list;
                if (CurAccount == null)
                    return null;

                #region 权限页面内容写入
                WebSYSAccountManager accountManager = new WebSYSAccountManager();
                List<WebSysMenuPage> autoPages = accountManager.GetAuthPages(CurAccount);
                List<SysMenuPage> pages = autoPages.Select(s => new SysMenuPage()
                {
                    MenuId = s.MenuId,
                    PageBtnname = s.PageBtnname,
                    PageId = s.PageId,
                    PageName = s.PageName,
                    PageUrl = s.PageUrl,
                    PageViewname = s.PageViewname

                }).ToList();
                SysLoginInfo.SetCurMenuPages(pages);
                #endregion
                return pages;
            }
        }
    }

}