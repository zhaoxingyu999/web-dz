using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using FilmLove.Admin.CommEntity.ResEntities;
using WebManagers.Core.Entity;
using YJYSoft.YL.Common.Helper;

namespace FilmLove.Admin.Controllers
{
    public class WebJSExtController : BaseController
    {
        [AllowAnonymous]
        public ContentResult GetAuthPages(string p)
        {
            List<SysMenuPage> autoPages = new List<SysMenuPage>();
            var page = CurAuthPages.FirstOrDefault(m => m.PageUrl == p);
            if (page != null)
            {
                autoPages = CurAuthPages.Where(m => m.MenuId == page.MenuId).ToList();
            }
            Dictionary<string, JSPageInfo> dicR = new Dictionary<string, JSPageInfo>();
            if (autoPages.Count > 0)
            {
                foreach (var item in autoPages)
                {
                    if (!dicR.ContainsKey(item.PageUrl))
                    {
                        dicR[item.PageUrl] = new JSPageInfo()
                        {
                            MenuId = item.MenuId ?? 0,
                            PageBtnname = item.PageBtnname,
                            PageId = item.PageId,
                            PageName = item.PageName,
                            PageUrl = item.PageUrl,
                            PageViewname = item.PageViewname
                        };
                    }
                }
            }
            string str = "var authPages = " + Newtonsoft.Json.JsonConvert.SerializeObject(dicR) + ";";
            return Content(str);
        }

        #region 枚举JS变量
        [OutputCache(Duration = 600, Location = OutputCacheLocation.ServerAndClient, VaryByParam = "*")]
        [AllowAnonymous]
        public ContentResult EnumJs()
        {
            string str = "";
            var ss = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Module.Name;
            int index = ss.LastIndexOf(".dll", StringComparison.OrdinalIgnoreCase);
            var classes = Assembly.Load(ss.Substring(0, index)).GetTypes().Where(m => m.Namespace == nameof(FilmLove.Admin.WebManager.Models)).ToList();
            foreach (var item in classes)
            {
                if (item.BaseType != typeof(Enum))
                    continue;
                str += GetEnumJSItem(item.FullName.Replace(nameof(FilmLove.Admin.WebManager.Models), "").Replace("+", "_").ToLower().Replace("enum", ""), item);
            }
            return Content(str);
        }

        private string GetEnumJSItem(string name, Type T)
        {
            return string.Format("var {0}={1};\r\n", "e_" + name, GetEContent(T));
        }

        private string GetEContent(Type T)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(EnumAutoHelper.GetJsItem(T));
            }
            catch
            {
                return "error";
            }
        }
        #endregion

    }
}