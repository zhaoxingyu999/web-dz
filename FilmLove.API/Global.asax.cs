using DB.SQLITE;
using DB.SQLITE.TABLE;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YJYSoft.YL.Common;

namespace FilmLove.API
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string version = "1";
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~") + @"\log4net.config"));
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            string file = Server.MapPath("/") + "version.txt";
            if (System.IO.File.Exists(file))
            {
                version = System.IO.File.ReadAllText(file);
                version = version.Replace("\r", "").Replace("\n", "");
            }
#if FB
            var c = SqliteAdoSessionManager.Current;
            var i2 = c.ErrorLogsDB.Query2<sqlite_master>("select * from sqlite_master where type = 'table' and name = 'ErrorLogs'");
            if (i2.Count == 0)
                c.ErrorLogsDB.ExecuteSql(SQLTableSentence.SQL_ErrorLogs);
            i2 = c.OperLogsDB.Query2<sqlite_master>("select * from sqlite_master where type = 'table' and name = 'OperLogs'");
            if (i2.Count == 0)
                c.OperLogsDB.ExecuteSql(SQLTableSentence.SQL_OperLogs);
#endif
        }
        private void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Response.Headers.Add("version", version);
            System.Web.HttpContext.Current.Response.Headers.Add("Server", "dualpsy server");
        }

        void Application_Error(object sender, EventArgs e)
        {
            string ip = YJYSoft.YL.Common.WebHelper.IPHelper.GetIP();
            Exception error = Server.GetLastError().GetBaseException();
            Server.ClearError();

            if (error.Message.IndexOf("未找到公共操作方法") != -1 || error.Message.IndexOf("的控制器或该控制器未实现") != -1)
            {
                Response.Write("404");
                Response.End();
                return;
            }
            AjaxResult v = new AjaxResult() { code = -500, msg = "网络繁忙" + error.Message + error.StackTrace.ToString(), data = "" };

            //记录日志  
            if (error.GetType().ToString() == "System.Data.Entity.Validation.DbEntityValidationException")
            {
                v.msg = "";
                //DbEntityValidationException dberror = (DbEntityValidationException)error;
                //foreach (var err in dberror.EntityValidationErrors)
                //{
                //    v.msg += err.Entry.Entity.ToString() + "--";
                //    foreach (var de in err.ValidationErrors)
                //    {
                //        v.msg += de.PropertyName + ":" + de.ErrorMessage;
                //    }
                //}
                //try
                //{
                //    logger.Error(ip + ":" + v.msg);
                //}
                //catch { }
            }
            if (error.GetType().ToString() == "System.Data.Entity.Infrastructure.DbUpdateException")
            {
                v.msg = "";
                //DbUpdateException dberror = (DbUpdateException)error;
                //foreach (var err in dberror.Entries)
                //{
                //    v.msg += err.Entity.ToString() + "--" + "\r\n";
                //    foreach (var de in err.CurrentValues.PropertyNames)
                //    {
                //        v.msg += de + ":" + err.CurrentValues[de] + "\r\n";
                //    }
                //}
                //try
                //{
                //    logger.Error(ip + ":" + v.msg);
                //}
                //catch { }
            }
            logger.Error(ip + ":" + "", error);
            string msg = v.msg;
#if FB
             v.msg ="网络繁忙";
#endif
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(v));
            Response.End();

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
#if FB
            SQLiteParameter ErrorType = new SQLiteParameter("ErrorType", error.GetType().ToString());
            SQLiteParameter URL = new SQLiteParameter("URL", Request.Url.ToString());
            SQLiteParameter MoreTxt = new SQLiteParameter("MoreTxt", vv);
            SQLiteParameter CreateTime = new SQLiteParameter("CreateTime", DateTime.Now);
            SQLiteParameter IP = new SQLiteParameter("IP", ip);
            SQLiteParameter ErrorTxt = new SQLiteParameter("ErrorTxt", msg);
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
#endif
        }

    }
}
