using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using FilmLove.Admin.WebManager.Models;

namespace FilmLove.Admin.CommEntity
{
    public class SysModule
    {
        public static List<FirstMenu> InitPermission()
        {
            List<FirstMenu> firstMenus = new List<FirstMenu>();
            var ss = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Module.Name;
            int index = ss.LastIndexOf(".dll", StringComparison.OrdinalIgnoreCase);
            var types = Assembly.Load(ss.Substring(0, index)).GetTypes().Where(e => e.BaseType.Name == nameof(Controllers.BaseController));
            foreach (var type in types)
            {
                var members = type.GetMethods().Where(e => (e.ReturnType.BaseType != null && e.ReturnType.BaseType.Name == nameof(ActionResult)) || e.ReturnType.Name == nameof(ActionResult)).ToList();
                //二级菜单
                foreach (var member in members)
                {
                    var attrs = member.GetCustomAttributes(typeof(SecendModuleMenu), true);
                    if (attrs.Length == 0)
                        continue;
                    SecendModuleMenu SecendMenu = attrs[0] as SecendModuleMenu;
                    var Action = member.Name;
                    var Controller = member.DeclaringType.Name.Substring(0, member.DeclaringType.Name.Length - 10);
                    FirstMenu firstMenu = new FirstMenu(SecendMenu.FirestMenu);
                    var first = firstMenus.Where(w => w.IndexCode == firstMenu.IndexCode).FirstOrDefault();
                    if (first == null)
                    {
                        first = firstMenu;
                        firstMenus.Add(firstMenu);
                    }
                    var secend = first.child.Where(w => w.IndexCode == SecendMenu.SecendIndexCode).FirstOrDefault();
                    if (secend != null)
                        throw new Exception("SecendIndexCode 错误，存在" + SecendMenu.SecendIndexCode);
                    SecendMenu secendMenu = new SecendMenu()
                    {
                        MenuStatus = 1,
                        MenuName = SecendMenu.SecendMenuName,
                        IndexCode = SecendMenu.SecendIndexCode,
                        MenuUrl = "/" + Controller + "/" + Action,
                        MenuSort = 0,
                    };
                    first.child.Add(secendMenu);

                }
                //子页面
                foreach (var member in members)
                {
                    var attrs = member.GetCustomAttributes(typeof(PageModuleAttribute), true);
                    if (attrs.Length == 0)
                        continue;
                    PageModuleAttribute page = attrs[0] as PageModuleAttribute;
                    var Action = member.Name;
                    var Controller = member.DeclaringType.Name.Substring(0, member.DeclaringType.Name.Length - 10);
                    bool isFind = false;
                    foreach (var first in firstMenus)
                    {
                        var secend = first.child.Where(w => w.IndexCode == page.SecendIndexCode).FirstOrDefault();
                        if (secend == null)
                            continue;
                        isFind = true;
                        secend.child.Add(new WebSysMenuPage()
                        {
                            PageStatus = 1,
                            PageBtnname = page.PageBtName,
                            PageName = page.PageName,
                            PageType = page.PageType,
                            PageViewname = page.PageViewName,
                            PageUrl = "/" + Controller + "/" + Action,
                        });
                    }
                    if (isFind == false)
                        throw new Exception("SecendIndexCode 错误，不存在" + page.SecendIndexCode);
                }
            }
            return firstMenus;
        }
    }

    [NotMapped]
    public class FirstMenu : WebSysMenu
    {
        public FirstMenu(WebSysMenu web)
        {
            MenuName = web.MenuName;
            MenuIcon = web.MenuIcon;
            IndexCode = web.IndexCode;
            MenuStatus = web.MenuStatus;
            MenuPid = web.MenuPid;
            MenuSort = web.MenuSort;
            MenuUrl = web.MenuUrl;
        }
        public List<SecendMenu> child = new List<SecendMenu>();
    }
    [NotMapped]
    public class SecendMenu : WebSysMenu
    {
        public List<WebSysMenuPage> child = new List<WebSysMenuPage>();
    }
    public class FirstModuleMenu
    {
        public const string 系统管理 = "00001";
        public const string 例子管理 = "00002";
        public static List<WebSysMenu> Menu = new List<WebSysMenu>() {
            new WebSysMenu()
        {
            MenuName = "系统管理",
                MenuIcon = "cogs",
                IndexCode = 系统管理,//唯一值
                MenuStatus = 1,
                MenuPid = 0,
                MenuSort = 999999,
                MenuUrl = "",
            },
            new WebSysMenu()
            {
                MenuName = "例子管理",
                MenuIcon = "cogs",
                IndexCode = 例子管理,//唯一值
                MenuStatus = 1,
                MenuPid = 0,
                MenuSort = 999999,
                MenuUrl = "",
            }};
    }
    /// <summary>
    /// 二级菜单
    /// </summary>
    public class SecendModuleMenu : Attribute
    {
        public WebSysMenu FirestMenu { get; private set; }
        public string SecendMenuName { get; private set; }
        public string SecendIndexCode { get; private set; }

        /// <summary>
        /// 二级菜单
        /// </summary>
        /// <param name="firestMenu">一级菜单</param>
        /// <param name="secendMenuName">二级菜单名称</param>
        /// <param name="secendIndexCode">二级菜单唯一码</param>
        public SecendModuleMenu(string firestIndexCode, string secendMenuName, string secendIndexCode)
        {
            FirestMenu = FirstModuleMenu.Menu.Where(w => w.IndexCode == firestIndexCode).First();
            SecendMenuName = secendMenuName;
            SecendIndexCode = secendIndexCode;
        }

    }
    /// <summary>
    /// 子页面
    /// </summary>
    public class PageModuleAttribute : Attribute
    {
        public string SecendIndexCode { get; private set; }
        public string PageName { get; private set; }
        public int PageType { get; private set; }
        public string PageViewName { get; private set; }
        public string PageBtName { get; private set; }
        /// <summary>
        /// 子页面
        /// </summary>
        /// <param name="secendIndexCode">二级菜单唯一码</param>
        /// <param name="pageName">子页面名称</param>
        /// <param name="pageType">子页面类型，1：页面，2：ajax请求</param>
        /// <param name="pageViewName">显示名称-链接</param>
        /// <param name="pageBtName">显示名称-按钮</param>
        public PageModuleAttribute(string secendIndexCode, string pageName, int pageType, string pageViewName, string pageBtName)
        {
            SecendIndexCode = secendIndexCode;
            PageName = pageName;
            PageType = pageType;
            PageViewName = pageViewName;
            PageBtName = pageBtName;
        }
    }
}