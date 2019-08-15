using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Admin.CommEntity.ResEntities
{
    public class MenuShowModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public bool isOpen { get; set; }
        public string url { get; set; }
        public string targetType { get; set; }
        public List<MenuShowModel> children { get; set; }

        public MenuShowModel()
        {
            children = new List<MenuShowModel>();

        }
    }

    public class MenuModel
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuLink { get; set; }
        public int MenuParentID { get; set; }
        public string MenuIcon { get; set; }
        public int MenuStatus { get; set; }
        public List<MenuModel> ChildMenus { get; set; }
    }

    public class RoleInfo
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleRemark { get; set; }
        public int RoleStatus { get; set; }
        public List<RoleMenuItem> RoleMenus { get; set; }
    }

    public class RoleMenuItem
    {
        public int MenuID { get; set; }
        public string MenuTitle { get; set; }
        public int MenuParentID { get; set; }
        public int Checked { get; set; }
        public List<RoleMenuItem> ChildMenus { get; set; }
        public List<RoleMenuPageItem> ItemPages { get; set; }
    }

    public class RoleMenuPageItem
    {
        public int PageID { get; set; }
        public int MenuID { get; set; }
        public int MainStatus { get; set; }
        public string PageName { get; set; }
        public int Checked { get; set; }
    }



    public class RoleItemInfo
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int Checked { get; set; }
    }

    public class SysAccountInfo
    {
        public int ManagerId { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerRealname { get; set; }
        public int? ManagerStatus { get; set; }
        public string ManagerTel { get; set; }
        public string ManagerName { get; set; }
        public int IsSupper { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public List<RoleItemInfo> Roles { get; set; }
    }


    public class MyAccountSaveModel
    {
        public long ManagerId { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerRealname { get; set; }
        public string ManagerPwd { get; set; }
        public string RePassword { get; set; }
        public string ManagerTel { get; set; }

    }


    public class JSPageInfo
    {
        public int PageId { get; set; }
        public int MenuId { get; set; }
        public string PageName { get; set; }
        public string PageBtnname { get; set; }
        public string PageViewname { get; set; }
        public string PageUrl { get; set; }
    }
}
