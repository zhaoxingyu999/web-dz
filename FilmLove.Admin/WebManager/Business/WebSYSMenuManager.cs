using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YJYSoft.YL.Common;
using System.Data;
using System.Data.SqlClient;
using FilmLove.Admin.WebManager.Models;
using FilmLove.Admin.CommEntity.Entity;
using FilmLove.Admin.CommEntity.ResEntities;
using System.Transactions;
using FilmLove.Admin.CommEntity;
using WebManagers.Core.Entity;

namespace FilmLove.Admin.ManagerBusiness.SYSAdmin
{
    public class WebSYSMenuManager : DBObjects
    {
        public WebSysMenu GetMenuById(int MenuId)
        {
            return db.WebSysMenu.FirstOrDefault(m => m.MenuId == MenuId);
        }

        public AjaxResult GetMenuPageList(int MenuId)
        {
            var r = db.WebSysMenuPage.Where(m => m.MenuId == MenuId).ToList();
            return new AjaxResult(r);
        }

        public WebSysMenuPage GetMenuPageById(int menuid, int pageid)
        {
            if (pageid == 0)
                return new WebSysMenuPage() { MenuId = menuid, PageStatus = 1 };
            var menupage = db.WebSysMenuPage.FirstOrDefault(m => m.PageId == pageid);
            if (menupage == null)
                return new WebSysMenuPage() { MenuId = menuid, PageStatus = 1 };
            return menupage;
        }

        public AjaxResult SaveMenuPage(WebSysMenuPage model)
        {
            if (model == null)
                return new AjaxResult("数据无效");
            if (string.IsNullOrEmpty(model.PageName))
                return new AjaxResult("请输入页面名称");
            if (string.IsNullOrEmpty(model.PageViewname))
                return new AjaxResult("请输入显示名称");
            if (model.PageUrl == null) model.PageUrl = "";
            DateTime dtNow = DateTime.Now;
            var ent = db.WebSysMenuPage.FirstOrDefault(m => m.PageId == model.PageId);
            int r = 0;
            if (ent == null)
            {
                ent = Newtonsoft.Json.JsonConvert.DeserializeObject<WebSysMenuPage>(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                ent.MainStatus = 0;
                ent.CreateTime = dtNow;
                ent.UpdateTime = dtNow;
                db.WebSysMenuPage.Add(ent);
                r += db.SaveChanges();
            }
            else
            {
                ent.PageName = model.PageName;
                ent.PageBtnname = model.PageBtnname;
                ent.PageParamters = model.PageParamters;
                ent.PageStatus = model.PageStatus;
                ent.PageType = model.PageType;
                ent.PageUrl = model.PageUrl;
                ent.PageViewname = model.PageViewname;
                ent.UpdateTime = dtNow;
                r += db.SaveChanges();
            }
            if (r <= 0)
                return new AjaxResult("更新失败");
            return new AjaxResult("更新成功！", 0);
        }

        public AjaxResult GetAllEnableMenu()
        {
            var Menus = db.WebSysMenu.Where(m => m.MenuStatus == 1).ToList();
            return new AjaxResult(UNMemus(Menus));
        }

        /// <summary>
        /// 获取所有栏目
        /// </summary>
        /// <returns></returns>
        public AjaxResult GetAllMenu()
        {
            var Menus = db.WebSysMenu.ToList();
            return new AjaxResult(UNMemus(Menus));
        }



        public AjaxResult GetMenuByManagerID(int ManagerID)
        {
            string strSQL = @"SELECT DISTINCT a.* FROM 
                        fxc_sys_menu AS a,
                        fxc_sys_role_menu AS b,
                        fxc_sys_manager_role AS c
                        WHERE a.MenuId = b.MenuId
                        AND a.MenuStatus = 1
                        AND b.RoleId = c.RoleId
                        AND c.ManagerId = @managerid";
            SqlParameter[] paramters = new SqlParameter[] {
                new SqlParameter("@managerid",ManagerID)
            };
            var list = db.Database.SqlQuery<WebSysMenu>(strSQL, paramters).ToList();
            return new AjaxResult(UNMemus(list));
        }



        public AjaxResult GetMenuEntity(int MenuID)
        {
            var menu = db.WebSysMenu.FirstOrDefault(m => m.MenuId == MenuID);
            if (menu == null)
                menu = new WebSysMenu()
                {
                    MenuPid = 0,
                    MenuStatus = 1
                };
            var topMenus = db.WebSysMenu.Where(m => m.MenuId != MenuID && m.MenuPid == 0).ToList();
            var result = new
            {
                menu = menu,
                topMenus = topMenus
            };
            return new AjaxResult(result);
        }

        public AjaxResult SaveMenu(WebSysMenu entity)
        {
            if (entity == null)
                return new AjaxResult("请求数据无效!");
            if (string.IsNullOrEmpty(entity.MenuName))
                return new AjaxResult("请输入栏目名称!");
            using (var scope = new TransactionScope())
            {
                int r;
                WebSysMenu old = SaveMenu(entity, out r);
                if (r <= 0)
                    return new AjaxResult("更新失败");
                scope.Complete();
                return new AjaxResult(old);
            }
        }
        private WebSysMenu SaveMenu(WebSysMenu entity, out int r)
        {
            r = 0;
            DateTime dtNow = DateTime.Now;
            var old = db.WebSysMenu.FirstOrDefault(m => m.MenuId == entity.MenuId);
            if (old == null)
            {
                old = Newtonsoft.Json.JsonConvert.DeserializeObject<WebSysMenu>(Newtonsoft.Json.JsonConvert.SerializeObject(entity));
                old.CreateTime = dtNow;
                old.UpdateTime = dtNow;
                db.WebSysMenu.Add(old);
                r += db.SaveChanges();
            }
            else
            {
                old.MenuName = entity.MenuName;
                old.MenuPid = entity.MenuPid;
                old.MenuIcon = entity.MenuIcon;
                old.MenuUrl = entity.MenuUrl;
                old.MenuStatus = entity.MenuStatus;
                old.MenuItempages = "";
                old.UpdateTime = dtNow;
                old.MenuSort = entity.MenuSort;
                r += db.SaveChanges();
            }
            string pIndexCode = "";
            if (entity.MenuPid != 0)
            {
                var pMenu = db.WebSysMenu.FirstOrDefault(m => m.MenuId == old.MenuPid);
                if (pMenu != null)
                    pIndexCode = pMenu.IndexCode;
            }
            old.IndexCode = pIndexCode + old.MenuId.ToString("00000");
            if (!string.IsNullOrWhiteSpace(entity.IndexCode))
                old.IndexCode = entity.IndexCode;
            r += db.SaveChanges();
            if (old.MenuPid != 0)
            {
                var menuPage = db.WebSysMenuPage.FirstOrDefault(m => m.MainStatus == 1 && m.MenuId == old.MenuId);
                if (menuPage == null)
                {
                    menuPage = new WebSysMenuPage()
                    {
                        CreateTime = dtNow,
                        MainStatus = 1,
                        MenuId = old.MenuId,
                        PageBtnname = old.MenuName,
                        PageName = old.MenuName,
                        PageParamters = "",
                        PageStatus = 1,
                        PageType = 1,
                        PageUrl = old.MenuUrl,
                        PageViewname = old.MenuName,
                        UpdateTime = dtNow,
                    };
                    db.WebSysMenuPage.Add(menuPage);
                    r += db.SaveChanges();
                }
                else
                {
                    menuPage.PageName = old.MenuName;
                    menuPage.PageViewname = old.MenuName;
                    menuPage.PageUrl = old.MenuUrl;
                    menuPage.UpdateTime = dtNow;
                    r += db.SaveChanges();
                }
            }
            return old;
        }

        private List<MenuModel> UNMemus(List<WebSysMenu> Menus)
        {
            Menus = Menus.OrderByDescending(m => m.MenuSort).ToList();
            var topMenus = Menus.Where(m => m.MenuPid == 0).ToList();
            List<MenuModel> list = new List<MenuModel>();
            foreach (var item in topMenus)
            {
                MenuModel addItem = new MenuModel()
                {
                    MenuID = item.MenuId,
                    MenuIcon = item.MenuIcon,
                    MenuLink = item.MenuUrl,
                    MenuParentID = item.MenuPid ?? 0,
                    MenuStatus = item.MenuStatus ?? 0,
                    MenuName = item.MenuName,
                };
                addItem.ChildMenus = new List<MenuModel>();
                var chls = Menus.Where(m => m.MenuPid == item.MenuId).ToList();
                if (chls.Count > 0)
                {
                    foreach (var itemc in chls)
                    {
                        MenuModel addItemc = new MenuModel()
                        {
                            MenuID = itemc.MenuId,
                            MenuIcon = "",
                            MenuLink = itemc.MenuUrl,
                            MenuStatus = itemc.MenuStatus ?? 0,
                            MenuParentID = itemc.MenuPid ?? 0,
                            MenuName = itemc.MenuName,
                        };
                        addItem.ChildMenus.Add(addItemc);
                    }
                }
                list.Add(addItem);
            }
            return list;
        }
        public AjaxResult LoadMenu(SysManager manager)
        {
            List<MenuShowModel> menus = new List<MenuShowModel>();
            List<WebSysMenu> menuList = GetMenus(manager);
            var topMenulist = menuList.Where(m => m.MenuPid == 0).ToList();
            foreach (var item in topMenulist)
            {
                MenuShowModel addItem = new MenuShowModel()
                {
                    id = Encrypt.MD5Encrypt16(item.MenuId.ToString("0000000000")),
                    icon = "fa fa-" + item.MenuIcon,
                    isOpen = false,
                    text = item.MenuName,
                };
                addItem.children = new List<MenuShowModel>();
                var cList = menuList.Where(m => m.MenuPid == item.MenuId).ToList();
                foreach (var itemc in cList)
                {
                    MenuShowModel addC = new MenuShowModel()
                    {
                        id = Encrypt.MD5Encrypt16(itemc.MenuId.ToString("0000000000")),
                        icon = "fa fa-circle",
                        targetType = "iframe-tab",
                        text = itemc.MenuName,
                        url = itemc.MenuUrl
                    };
                    addItem.children.Add(addC);
                }
                menus.Add(addItem);
            }
            return new AjaxResult(menus);
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public List<WebSysMenu> GetMenus(SysManager manager)
        {
            if (manager.IsSupper == 1)
                return GetAllMenus();
            else
                return GetManagerMenus(manager);
        }


        public List<WebSysMenu> GetAllMenus()
        {
            return db.WebSysMenu.Where(m => m.MenuStatus == 1).OrderByDescending(m => m.MenuSort).ToList();
        }

        public List<WebSysMenu> GetManagerMenus(SysManager manager)
        {
            //获取角色ID集合
            var roleIds = db.WebSysManagerRole.Where(m => m.ManagerId == manager.ManagerId).Select(m => m.RoleId).Distinct().ToList();
            //获取菜单ID集合
            List<int> menuIds = db.WebSysRoleMenu.Where(m => roleIds.Contains(m.RoleId)).Select(m => m.MenuId ?? 0).Distinct().ToList();
            //获取菜单
            return db.WebSysMenu.Where(m => menuIds.Contains(m.MenuId) && m.MenuStatus == 1).OrderByDescending(m => m.MenuSort).ToList();
        }

        public int MakeMenu(List<FirstMenu> firstMenus)
        {
            int i = 0;
            using (var scope = new TransactionScope())
            {
                foreach (var first in firstMenus)
                {
                    var f = db.WebSysMenu.Where(w => w.MenuPid == 0 && w.IndexCode == first.IndexCode).FirstOrDefault();
                    if (f == null)
                    {
                        int r;
                        f = SaveMenu(first, out r);
                        i++;
                    }
                    foreach (var secend in first.child)
                    {
                        var s = db.WebSysMenu.Where(w => w.MenuPid != 0 && (w.IndexCode == secend.IndexCode || w.MenuUrl == secend.MenuUrl)).FirstOrDefault();
                        if (s == null)
                        {
                            int r;
                            secend.MenuPid = f.MenuId;
                            s = SaveMenu(secend, out r);
                            i++;
                        }
                        foreach (var page in secend.child)
                        {
                            var p = db.WebSysMenuPage.Where(w => w.PageUrl == page.PageUrl).FirstOrDefault();
                            if (p == null)
                            {
                                page.MenuId = s.MenuId;
                                SaveMenuPage(page);
                            }
                        }
                    }

                }
                db.SaveChanges();
                scope.Complete();
            }
            return 0;
        }
        public int MakeMenu2(List<WebManagers.Core.MenuItemAttribute> allMenus)
        {
            int r = 0;
            //先进行排序
            allMenus = allMenus.OrderBy(m => m.IsMain).ToList();

            using (TransactionScope trans = new TransactionScope())
            {
                var DBTopMenu = db.WebSysMenu.Where(m => m.MenuPid == 0).ToList();
                List<string> ModuleNames = allMenus.Select(m => m.ModuleName).Distinct().ToList();
                foreach (var item in ModuleNames)
                {
                    var dbModule = DBTopMenu.FirstOrDefault(m => m.MenuName == item);
                    if (dbModule == null)
                    {
                        dbModule = new WebSysMenu()
                        {
                            CreateTime = DateTime.Now,
                            IndexCode = "",
                            MenuIcon = "",
                            MenuItempages = "",
                            MenuName = item,
                            MenuPid = 0,
                            MenuSort = 0,
                            MenuStatus = 1,
                            MenuUrl = "",
                            UpdateTime = DateTime.Now,
                        };
                        db.WebSysMenu.Add(dbModule);
                        r += db.SaveChanges();
                        dbModule.IndexCode = dbModule.MenuId.ToString("00000");
                        r += db.SaveChanges();
                    }
                }
                var DBAllMenus = db.WebSysMenu.ToList();
                var DBAllPages = db.WebSysMenuPage.ToList();
                foreach (var item in allMenus)
                {
                    var topMenu = DBAllMenus.Where(m => m.MenuName == item.ModuleName).FirstOrDefault();
                    var dbMenu = DBAllMenus.FirstOrDefault(m => m.MenuUrl == item.Url);
                    if (dbMenu == null)
                    {
                        dbMenu = new WebSysMenu()
                        {
                            CreateTime = DateTime.Now,
                            IndexCode = "",
                            MenuIcon = "",
                            MenuItempages = "",
                            MenuName = item.IsMain == 1 ? item.MainName : item.SubName,
                            MenuPid = topMenu.MenuId,
                            MenuSort = 0,
                            MenuStatus = 1,
                            MenuUrl = item.Url,
                            UpdateTime = DateTime.Now,
                        };
                        db.WebSysMenu.Add(dbMenu);
                    }
                    else
                    {
                        dbMenu.MenuName = item.MainName;
                        dbMenu.MenuPid = topMenu.MenuId;
                        dbMenu.UpdateTime = DateTime.Now;
                    }
                    r += db.SaveChanges();
                    dbMenu.IndexCode = topMenu.IndexCode + dbMenu.MenuId.ToString("00000");
                    r += db.SaveChanges();

                    foreach (var itemSub in item.SubPages)
                    {
                        var itemPage = DBAllPages.Where(m => m.PageUrl == itemSub.Url).FirstOrDefault();
                        string itemSubName = itemSub.IsMain == 1 ? itemSub.MainName : itemSub.SubName;
                        if (itemPage == null)
                        {
                            itemPage = new WebSysMenuPage()
                            {
                                CreateTime = DateTime.Now,
                                MainStatus = itemSub.IsMain == 1 ? 1 : 0,
                                MenuId = dbMenu.MenuId,
                                PageBtnname = itemSubName,
                                PageName = itemSubName,
                                PageParamters = "",
                                PageStatus = 1,
                                PageType = itemSub.ReturnType == "JsonResult" ? 2 : 1,
                                PageUrl = itemSub.Url,
                                PageViewname = itemSubName,
                                UpdateTime = DateTime.Now,
                            };
                            db.WebSysMenuPage.Add(itemPage);
                        }
                        else
                        {
                            itemPage.CreateTime = DateTime.Now;
                            itemPage.MainStatus = itemSub.IsMain == 1 ? 1 : 0;
                            itemPage.MenuId = dbMenu.MenuId;
                            itemPage.PageBtnname = itemSubName;
                            itemPage.PageName = itemSubName;
                            itemPage.PageType = itemSub.ReturnType == "JsonResult" ? 2 : 1;
                            itemPage.PageViewname = itemSubName;
                            itemPage.UpdateTime = DateTime.Now;
                        }
                        r += db.SaveChanges();
                    }
                }
                trans.Complete();
                return r;
            }
        }
    }
}
