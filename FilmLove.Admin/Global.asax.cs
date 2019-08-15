using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YJYSoft.YL.Common;
using DB.SQLITE;
using DB.SQLITE.TABLE;
using System.Data.SQLite;
using System.Reflection;

namespace FilmLove.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~") + @"\log4net.config"));
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            WebManagers.Core.Extention.AppSetting.Start();

            var c = SqliteAdoSessionManager.Current;
            var i2 = c.ErrorLogsDB.Query2<sqlite_master>("select * from sqlite_master where type = 'table' and name = 'ErrorLogs'");
            if (i2.Count == 0)
                c.ErrorLogsDB.ExecuteSql(SQLTableSentence.SQL_ErrorLogs);
            i2 = c.OperLogsDB.Query2<sqlite_master>("select * from sqlite_master where type = 'table' and name = 'OperLogs'");
            if (i2.Count == 0)
                c.OperLogsDB.ExecuteSql(SQLTableSentence.SQL_OperLogs);
        }
        protected void Application_End(object sender, EventArgs e)
        {

        }

        void Application_Error(object sender, EventArgs e)
        {
            string ip = GetIP();
            Exception error = Server.GetLastError().GetBaseException();
            if (error.Message.IndexOf("未找到公共操作方法") != -1 || error.Message.IndexOf("的控制器或该控制器未实现") != -1)
            {
                Response.Write("404");
                Response.End();
                Server.ClearError();

            }
#if! FB
            AjaxResult v = new AjaxResult() { code = -500, msg = "网络繁忙" + error.Message + error.StackTrace.ToString(), data = "" };
#else
            AjaxResult v = new AjaxResult() { code = -500, msg = "网络繁忙", data = "" };
#endif
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(v));
            v.msg = error.Message + error.StackTrace.ToString();
            //记录日志  
            if (error.GetType().ToString() == "System.Data.Entity.Validation.DbEntityValidationException")
            {
                v.msg = "";
                DbEntityValidationException dberror = (DbEntityValidationException)error;
                foreach (var err in dberror.EntityValidationErrors)
                {
                    v.msg += err.Entry.Entity.ToString() + "--";
                    foreach (var de in err.ValidationErrors)
                    {
                        v.msg += de.PropertyName + ":" + de.ErrorMessage;
                    }
                }
                try
                {
                    logger.Error(ip + ":" + v.msg);
                }
                catch { }
            }
            if (error.GetType().ToString() == "System.Data.Entity.Infrastructure.DbUpdateException")
            {
                v.msg = "";
                DbUpdateException dberror = (DbUpdateException)error;
                foreach (var err in dberror.Entries)
                {
                    v.msg += err.Entity.ToString() + "--" + "\r\n";
                    foreach (var de in err.CurrentValues.PropertyNames)
                    {
                        v.msg += de + ":" + err.CurrentValues[de] + "\r\n";
                    }
                }
                try
                {
                    logger.Error(ip + ":" + v.msg);
                }
                catch { }
            }
            logger.Error(ip + ":" + "", error);
            Response.End();
            Server.ClearError();
            var vv = "\r\n" + Request.Url.ToString() + "\r\n";
            vv += "Request.QueryString\r\n";
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
            logger.Error(ip + ":" + vv);

            SQLiteParameter ErrorType = new SQLiteParameter("ErrorType", error.GetType().ToString());
            SQLiteParameter URL = new SQLiteParameter("URL", Request.Url.ToString());
            SQLiteParameter MoreTxt = new SQLiteParameter("MoreTxt", vv);
            SQLiteParameter CreateTime = new SQLiteParameter("CreateTime", DateTime.Now);
            SQLiteParameter IP = new SQLiteParameter("IP", GetIP());
            SQLiteParameter ErrorTxt = new SQLiteParameter("ErrorTxt", v.msg);
            var c = SqliteAdoSessionManager.Current;
            var vvc = c.ErrorLogsDB.CreateSQLiteCommand(@"INSERT INTO ErrorLogs (
                          ErrorType,
                          URL,
                          MoreTxt,
                          CreateTime,
                          IP,
                          ErrorTxt
                      )
                      VALUES (
                          @ErrorType,
                          @URL,
                          @MoreTxt,
                          @CreateTime,
                          @IP,
                          @ErrorTxt
                      );", new SQLiteParameter[] { ErrorType, URL, MoreTxt, CreateTime, IP, ErrorTxt });
            c.ErrorLogsDB.ExecuteNonQuery(vvc);
        }
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetIP()
        {
            //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
            string userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];//.ToString().Split(',')[0].Trim();
            //否则直接读取REMOTE_ADDR获取客户端IP地址
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            //前两者均失败，则利用Request.UserHostAddress属性获取IP地址，但此时无法确定该IP是客户端IP还是代理IP
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }
            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}
