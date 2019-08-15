using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using FilmLove.Admin.CommEntity.ResEntities;
using FilmLove.Admin.WebManager.Models;
using YJYSoft.YL.Common;

namespace FilmLove.Admin.ManagerBusiness.SYSAdmin
{
    public class WebSYSRoleManager : DBObjects
    {
        public AjaxResult GetAllRole()
        {
            var Roles = db.WebSysRole.ToList();
            return new AjaxResult(Roles);
        }


        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public AjaxResult GetRoleInfo(int RoleID)
        {
            RoleInfo info = new RoleInfo();
            info.RoleStatus = 1;
            var itemPages = db.WebSysMenuPage.Where(m => m.MainStatus == 0).ToList();

            info.RoleMenus = db.WebSysMenu.Where(m => m.MenuStatus == 1).Select(m => new RoleMenuItem()
            {
                Checked = 0,
                MenuID = m.MenuId,
                MenuParentID = m.MenuPid ?? 0,
                MenuTitle = m.MenuName,
            }).ToList();

            info.RoleMenus.ForEach(t =>
            {
                t.ItemPages = itemPages.Where(a => a.MenuId == t.MenuID).Select(a => new RoleMenuPageItem()
                {
                    MenuID = t.MenuID,
                    PageID = a.PageId,
                    MainStatus = a.MainStatus ?? 0,
                    PageName = a.PageName,
                }).ToList();
            });

            var role = db.WebSysRole.FirstOrDefault(m => m.RoleId == RoleID);
            if (role != null)
            {
                info.RoleId = role.RoleId;
                info.RoleName = role.RoleName;
                info.RoleRemark = role.RoleRemark;
                info.RoleStatus = role.RoleStatus ?? 0;
                var roleMenus = db.WebSysRoleMenu.Where(m => m.RoleId == role.RoleId).ToList();
                info.RoleMenus.ForEach(t =>
                {
                    var roleMenu = roleMenus.FirstOrDefault(m => m.MenuId == t.MenuID);
                    List<string> itemPageIds = new List<string>();
                    if (roleMenu != null)
                    {
                        t.Checked = 1;
                        if (!string.IsNullOrEmpty(roleMenu.PageIds))
                        {
                            itemPageIds = roleMenu.PageIds.Split(',').ToList();
                        }
                    }
                    t.ItemPages.ForEach(l =>
                    {
                        l.Checked = itemPageIds.Contains(l.PageID.ToString()) ? 1 : 0;
                    });
                });
            }
            List<RoleMenuItem> roleItems = new List<RoleMenuItem>();
            var topMenus = info.RoleMenus.Where(m => m.MenuParentID == 0);
            foreach (var menu in topMenus)
            {
                menu.ChildMenus = info.RoleMenus.Where(m => m.MenuParentID == menu.MenuID).ToList();
                roleItems.Add(menu);
            }
            info.RoleMenus = roleItems;
            return new AjaxResult(info);
        }


        public AjaxResult SaveRole(RoleInfo info)
        {
            if (info == null)
                return new AjaxResult("提交数据无效！");
            if (string.IsNullOrEmpty(info.RoleName))
                return new AjaxResult("请输入角色名称！");
            if (info.RoleMenus.Count == 0)
                return new AjaxResult("请选择管理权限！");
            using (var scope = new TransactionScope())
            {
                var role = db.WebSysRole.FirstOrDefault(m => m.RoleId == info.RoleId);
                if (role == null)
                    role = new WebSysRole();
                role.RoleName = info.RoleName;
                role.RoleRemark = info.RoleRemark;
                role.RoleStatus = info.RoleStatus;
                if (!role.CreateTime.HasValue)
                {
                    role.CreateTime = DateTime.Now;
                }
                role.UpdateTime = DateTime.Now; ;
                if (role.RoleId == 0)
                {
                    db.WebSysRole.Add(role);
                    db.SaveChanges();
                }
                else
                {
                    db.SaveChanges();
                }

                var dels = db.WebSysRoleMenu.Where(m => m.RoleId == role.RoleId).ToList();
                if (dels.Count > 0)
                {
                    db.WebSysRoleMenu.RemoveRange(dels);
                    db.SaveChanges();
                }
                var mainPages = db.WebSysMenuPage.Where(m => m.MainStatus == 1).ToList();
                if (info.RoleMenus != null)
                {
                    List<WebSysRoleMenu> insertRoleMenus = new List<WebSysRoleMenu>();
                    foreach (var item in info.RoleMenus)
                    {
                        WebSysRoleMenu addItem = new WebSysRoleMenu()
                        {
                            RoleId = role.RoleId,
                            MenuId = item.MenuID
                        };
                        var mainPage = mainPages.FirstOrDefault(m => m.MenuId == item.MenuID);
                        if (mainPage != null)
                        {
                            addItem.PageIds = mainPage.PageId.ToString();
                        }
                        if (item.ItemPages != null)
                        {
                            addItem.PageIds = (string.IsNullOrEmpty(addItem.PageIds) ? "" : addItem.PageIds + ",") + string.Join(",", item.ItemPages.Select(m => m.PageID).ToArray());
                        }
                        insertRoleMenus.Add(addItem);
                    }
                    if (insertRoleMenus.Count > 0)
                    {
                        db.WebSysRoleMenu.AddRange(insertRoleMenus);
                        db.SaveChanges();
                    }
                }
                scope.Complete();
            }
            return new AjaxResult("角色保存成功！", 0);
        }
    }
}
