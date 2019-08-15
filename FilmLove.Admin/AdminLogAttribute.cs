using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using YJYSoft.YL.Common;
using FilmLove.Admin.WebManager.Models;
using FilmLove.Admin.ManagerBusiness.Common;
using WebManagers.Core;

namespace FilmLove.Admin
{
    class AdminLogAttribute : ActionFilterAttribute
    {
        private string _parameterNameList = "";
        private string _LogTypeName = "";
        private int _type;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogTypeName">名称</param>
        /// <param name="tp">操作类型(0新增 1修改 2删除 3登录)</param>
        /// <param name="Param">未用</param>
        public AdminLogAttribute(string LogTypeName, int tp, string Param = "")
        {
            _LogTypeName = LogTypeName;
            _parameterNameList = Param;
            _type = tp;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            try
            {
                //操作类型(0新增 1修改 2删除 3登录)
                WebSysLog log = new WebSysLog();
                var info = SysLoginInfo.CurAccount();
                if (info != null)
                {
                    log.ManagerGuid = info.ManagerId.ToString();
                    log.ManagerAccount = info.ManagerName;
                }
                log.LogIp = GetIP();
                log.LogType = _type;
                log.LogTime = DateTime.Now;//操作时间
                log.LogName = _LogTypeName;

                string control = filterContext.Controller.ValueProvider.GetValue("controller").AttemptedValue;
                string action = filterContext.Controller.ValueProvider.GetValue("action").AttemptedValue;

                //参数说明
                StringBuilder ParamContent = new StringBuilder();
                if (_parameterNameList != "")
                {//根据条件得到参数
                    ParamContent.Append(GetWhereParam(filterContext.Controller.ValueProvider));
                }
                else
                {//获取提交的所有参数
                    ParamContent.Append(GetAllParam(filterContext.Controller.ValueProvider));
                }
                log.MapMethod = control + "/" + action;//操作方法
                log.LogContent = ParamContent.ToString();//参数说明

                //根据数据表设计，进行字符串的裁剪
                log.LogContent = log.LogContent.Length > 4000 ? log.LogContent.Substring(0, 4000) : log.LogContent;
                log.MapMethod = log.MapMethod.Length > 100 ? log.MapMethod.Substring(0, 100) : log.MapMethod;

                web_managerEntities db = new web_managerEntities(Encrypt.StringDecodeOne(DBConfigure.ConnStr));
                db.WebSysLog.Add(log);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //LogHelper.WriteLog("写操作日志错误", ex);
            }
        }
        /// <summary>
        /// 根据条件得到参数
        /// </summary>
        /// <param name="ivp"></param>
        /// <returns></returns>
        string GetWhereParam(IValueProvider ivp)
        {
            Dictionary<string, string> wpara = new Dictionary<string, string>();
            var nlist = _parameterNameList.Split(',', '|');
            var items = nlist.GroupBy(g => g).Select(s => s.Key).ToList();
            foreach (var item in items)
            {
                var valueProviderResult = ivp.GetValue(item);

                if (valueProviderResult != null)
                {
                    wpara.Add(item, valueProviderResult.AttemptedValue);
                }
            }
            var param = new
            {
                WhereParam = wpara,
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(param);
        }
        /// <summary>
        /// 获取提交的所有参数
        /// </summary>
        /// <param name="ivp"></param>
        /// <returns></returns>
        string GetAllParam(IValueProvider ivp)
        {
            Dictionary<string, string> Qsvp = new Dictionary<string, string>();
            Dictionary<string, string> Fvp = new Dictionary<string, string>();
            ValueProviderCollection vp = (ValueProviderCollection)ivp;
            foreach (var v in vp)
            {
                Type t = v.GetType();
                if (t.ToString() == "System.Web.Mvc.QueryStringValueProvider")
                {
                    QueryStringValueProvider qvp = (QueryStringValueProvider)v;
                    foreach (var keys in qvp.GetKeysFromPrefix(""))
                    {
                        Qsvp.Add(keys.Key, qvp.GetValue(keys.Key).AttemptedValue);
                    }
                }
                if (t.ToString() == "System.Web.Mvc.FormValueProvider")
                {
                    FormValueProvider qvp = (FormValueProvider)v;
                    foreach (var keys in qvp.GetKeysFromPrefix(""))
                    {
                        Fvp.Add(keys.Key, qvp.GetValue(keys.Key).AttemptedValue);
                    }
                }
            }
            var param = new
            {
                QueryString = Qsvp,
                FormValue = Fvp
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(param);
        }
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public string GetIP()
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
        public bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}
