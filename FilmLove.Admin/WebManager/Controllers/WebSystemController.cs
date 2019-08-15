using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using FilmLove.Admin.CommEntity;
using FilmLove.Admin.CommEntity.Entity;
using FilmLove.Admin.CommEntity.ResEntities;
using FilmLove.Admin.ManagerBusiness.SYSAdmin;
using FilmLove.Admin.WebManager.Models;
using YJYSoft.YL.Common;
using WebManagers.Core;

namespace FilmLove.Admin.Controllers
{
    public class WebSystemController : BaseController
    {
        WebSYSMenuManager sysMenuManager = new WebSYSMenuManager();
        WebSYSRoleManager sysRoleManager = new WebSYSRoleManager();
        WebSYSUserManager sysUserManager = new WebSYSUserManager();

        #region 菜单相关
        /// <summary>
        /// 菜单列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuList()
        {
            return View("~/WebManager/Views/WebSystem/MenuList.cshtml");
        }

        /// <summary>
        /// 菜单编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuEdit()
        {
            return View("~/WebManager/Views/WebSystem/MenuEdit.cshtml");
        }

        /// <summary>
        /// 子菜单页面
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        public ActionResult MenuPageList(string menuid)
        {
            int MenuId = ConvertN.ToInt32(menuid);
            WebSysMenu CurMenu = sysMenuManager.GetMenuById(MenuId);
            WebSysMenu ParentMenu = sysMenuManager.GetMenuById(CurMenu.MenuPid ?? 0);
            ViewBag.CurMenu = CurMenu;
            ViewBag.ParentMenu = ParentMenu;
            return View("~/WebManager/Views/WebSystem/MenuPageList.cshtml");
        }

        public JsonResult MenuPageDataList(string menuid)
        {
            int MenuId = ConvertN.ToInt32(menuid);
            var r = sysMenuManager.GetMenuPageList(MenuId);
            return Json(r);
        }


        public ActionResult MenuPageEdit(int menuid, int pageid)
        {
            WebSysMenuPage MenuPage = sysMenuManager.GetMenuPageById(menuid, pageid);
            ViewBag.MenuPage = MenuPage;
            return View("~/WebManager/Views/WebSystem/MenuPageEdit.cshtml");
        }

        [AdminLogAttribute("添加修改子菜单页面", 0)]
        public JsonResult SaveMenuPage(WebSysMenuPage model)
        {
            var r = sysMenuManager.SaveMenuPage(model);
            return Json(r);
        }

        /// <summary>
        /// 菜单列表加载
        /// </summary>
        /// <returns></returns>
        public JsonResult MenuLoadList()
        {
            var result = sysMenuManager.GetAllMenu();
            return Json(result);
        }


        /// <summary>
        /// 菜单信息加载
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        public JsonResult MenuLoadInfo(string menuid)
        {
            int iMenuID = ConvertN.ToInt32(menuid);
            var result = sysMenuManager.GetMenuEntity(iMenuID);
            return Json(result);
        }

        /// <summary>
        /// 菜单保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AdminLogAttribute("添加修改菜单", 0)]
        public JsonResult MenuSave(WebSysMenu entity)
        {
            var result = sysMenuManager.SaveMenu(entity);
            return Json(result);
        }
        [AllowAnonymous]
        public JsonResult MakeMenu()
        {
            AjaxResult result = new AjaxResult();
            var s = SysModule.InitPermission();
            WebSYSMenuManager menuManager = new WebSYSMenuManager();
            int i = menuManager.MakeMenu(s);
            result.data = i;
            return Json(result);
        }

        [AllowAnonymous]
        public ActionResult MakeMenu2()
        {
            string str = "";
            var ss = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Module.Name;
            int index = ss.LastIndexOf(".dll", StringComparison.OrdinalIgnoreCase);
            var allTypes = Assembly.Load(ss.Substring(0, index)).GetTypes();
            var types = allTypes.Where(e => e.BaseType.Name == nameof(BaseController));
            List<MenuItemAttribute> allMenus = new List<MenuItemAttribute>();
            foreach (var type in types)
            {
                var members = type.GetMethods().Where(e => (e.ReturnType.BaseType != null && e.ReturnType.BaseType.Name == nameof(ActionResult))
                || e.ReturnType.Name == nameof(ActionResult)).ToList();
                {
                    //二级菜单
                    foreach (var member in members)
                    {
                        var attrs = member.GetCustomAttributes(typeof(MenuItemAttribute), true);
                        if (attrs.Length == 0)
                            continue;
                        MenuItemAttribute menuItem = attrs[0] as MenuItemAttribute;
                        var aname = member.Name;
                        var cname = member.DeclaringType.Name;
                        cname = cname.Substring(0, cname.LastIndexOf("Controller"));
                        menuItem.Url = string.Format("/{0}/{1}", cname, aname);
                        menuItem.ReturnType = member.ReturnType.Name;
                        menuItem.ItemKey = Encrypt.MD5Encrypt(menuItem.Url);
                        menuItem.ItemPKey = "";
                        allMenus.Add(menuItem);
                    }
                }
            }

            allMenus.ForEach(t =>
            {
                if (t.IsMain != 1)
                {
                    var pMenu = allMenus.FirstOrDefault(m => m.IsMain == 1 && m.MainName == t.MainName);
                    if (pMenu != null)
                    {
                        t.ItemPKey = pMenu.ItemKey;
                    }
                }
            });

            var allNewMenus = allMenus.Where(m => m.IsMain == 1).ToList();
            allNewMenus.ForEach(t =>
            {
                t.SubPages.Add(t);
                t.SubPages.AddRange(allMenus.Where(m => m.ItemPKey == t.ItemKey).ToList());
            });


            WebSYSMenuManager _WebSYSMenuManager = new WebSYSMenuManager();
            int r = _WebSYSMenuManager.MakeMenu2(allNewMenus);
            str += r.ToString() + "<br />";
            //str += JsonHelper.Serializer(allNewMenus);
            return Content(str);
        }
        #endregion

        #region 角色相关
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleList()
        {
            return View("~/WebManager/Views/WebSystem/RoleList.cshtml");
        }

        /// <summary>
        /// 角色列表数据加载
        /// </summary>
        /// <returns></returns>
        public JsonResult RoleLoadList()
        {
            var result = sysRoleManager.GetAllRole();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 角色编辑
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public ActionResult RoleEdit(string roleid)
        {
            return View("~/WebManager/Views/WebSystem/RoleEdit.cshtml");
        }

        /// <summary>
        /// 加载角色信息
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>

        public JsonResult RoleLoadInfo(int roleid)
        {
            var result = sysRoleManager.GetRoleInfo(roleid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 角色信息保存
        /// </summary>
        /// <returns></returns>
        [AdminLogAttribute("添加修改角色信息", 0)]
        public JsonResult RoleSave(RoleInfo info)
        {
            var result = sysRoleManager.SaveRole(info);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 账号相关

        public ActionResult AccountList()
        {
            return View("~/WebManager/Views/WebSystem/AccountList.cshtml");
        }


        public JsonResult AccountLoadList(SysUserListModel model)
        {
            var page = sysUserManager.AccountLoadList(model);
            return Json(page);
        }


        public ActionResult AccountEdit(string accountid)
        {
            return View("~/WebManager/Views/WebSystem/AccountEdit.cshtml");
        }


        public JsonResult AccountLoadInfo(int accountid)
        {
            var result = sysUserManager.GetSysAccountInfo(accountid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [AdminLogAttribute("添加修改账号", 0)]
        public JsonResult AccountSaveInfo(SysAccountInfo model)
        {
            var result = sysUserManager.SaveAccountInfo(model);
            return Json(result);
        }

        #region 个人信息编辑
        [AllowAnonymous]
        public ActionResult MyAccount()
        {
            WebSysManager ent = sysUserManager.GetManagerById(CurAccount.ManagerId);
            return View("~/WebManager/Views/WebSystem/MyAccount.cshtml", ent);
        }

        [AllowAnonymous]
        [AdminLogAttribute("添加修改账号", 0)]
        public JsonResult MyAccountSave(MyAccountSaveModel model)
        {
            var r = sysUserManager.MyAccountSave(model);
            return Json(r);
        }

        [AllowAnonymous]
        public ActionResult UpdatePassword()
        {
            return View("~/WebManager/Views/WebSystem/UpdatePassword.cshtml");
        }

        [AllowAnonymous]
        [AdminLogAttribute("添加修改账号密码", 0)]
        public JsonResult UpdatePasswordDo(string PassWord, string NewPassWord, string RePassWord)
        {
            var r = sysUserManager.UpdatePasswordDo(CurAccount.ManagerId, PassWord, NewPassWord, RePassWord);
            return Json(r);
        }
        #endregion
        #endregion
    }
}