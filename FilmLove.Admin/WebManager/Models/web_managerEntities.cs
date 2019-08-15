using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FilmLove.Admin.WebManager.Models
{

    public partial class web_managerEntities : DbContext
    {
        public web_managerEntities(string nameOrConnectionString = "name=OfficialWebEntities")
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var enWebSysLog = modelBuilder.Entity<WebSysLog>();

            enWebSysLog.HasKey(e => e.Id);

            enWebSysLog.ToTable("web_sys_log");

            enWebSysLog.Property(e => e.Id).HasColumnName("id");

            enWebSysLog.Property(e => e.LogContent)
                    .HasColumnName("log_content")
                    .HasMaxLength(4000);

            enWebSysLog.Property(e => e.LogIp)
                    .HasColumnName("log_ip")
                    .HasMaxLength(100);

            enWebSysLog.Property(e => e.LogName)
                    .HasColumnName("log_name")
                    .HasMaxLength(100);

            enWebSysLog.Property(e => e.LogTime)
                    .HasColumnName("log_time")
                    .HasColumnType("datetime");

            enWebSysLog.Property(e => e.LogType).HasColumnName("log_type");

            enWebSysLog.Property(e => e.ManagerAccount)
                    .HasColumnName("manager_account")
                    .HasMaxLength(50);

            enWebSysLog.Property(e => e.ManagerGuid)
                    .IsRequired()
                    .HasColumnName("manager_guid")
                    .HasMaxLength(50);

            enWebSysLog.Property(e => e.MapMethod)
                    .HasColumnName("map_method")
                    .HasMaxLength(50);

            var enWebSysManager = modelBuilder.Entity<WebSysManager>();

            enWebSysManager.HasKey(e => e.ManagerId);

            enWebSysManager.ToTable("web_sys_manager");

            enWebSysManager.Property(e => e.ManagerId).HasColumnName("manager_id");

            enWebSysManager.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

            enWebSysManager.Property(e => e.CurToken)
                    .HasColumnName("cur_token")
                    .HasMaxLength(50);

            enWebSysManager.Property(e => e.IsSupper).HasColumnName("is_supper");

            enWebSysManager.Property(e => e.LastLoginTime)
                    .HasColumnName("last_login_time")
                    .HasColumnType("datetime");

            enWebSysManager.Property(e => e.ManagerEmail)
                    .HasColumnName("manager_email")
                    .HasMaxLength(50);

            enWebSysManager.Property(e => e.ManagerIsdel).HasColumnName("manager_isdel");

            enWebSysManager.Property(e => e.ManagerName)
                    .HasColumnName("manager_name")
                    .HasMaxLength(30);

            enWebSysManager.Property(e => e.ManagerPwd)
                    .HasColumnName("manager_pwd")
                    .HasMaxLength(50);

            enWebSysManager.Property(e => e.ManagerRealname)
                    .HasColumnName("manager_realname")
                    .HasMaxLength(20);

            enWebSysManager.Property(e => e.ManagerScal)
                    .HasColumnName("manager_scal")
                    .HasMaxLength(10);

            enWebSysManager.Property(e => e.ManagerStatus).HasColumnName("manager_status");

            enWebSysManager.Property(e => e.ManagerTel)
                    .HasColumnName("manager_tel")
                    .HasMaxLength(30);

            enWebSysManager.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");

            var enWebSysManagerRole = modelBuilder.Entity<WebSysManagerRole>();

            enWebSysManagerRole.HasKey(e => e.AutoId);

            enWebSysManagerRole.ToTable("web_sys_manager_role");

            enWebSysManagerRole.Property(e => e.AutoId).HasColumnName("auto_id");

            enWebSysManagerRole.Property(e => e.ManagerId).HasColumnName("manager_id");

            enWebSysManagerRole.Property(e => e.RoleId).HasColumnName("role_id");

            var enWebSysMenu = modelBuilder.Entity<WebSysMenu>();

            enWebSysMenu.HasKey(e => e.MenuId);

            enWebSysMenu.ToTable("web_sys_menu");

            enWebSysMenu.Property(e => e.MenuId).HasColumnName("menu_id");

            enWebSysMenu.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

            enWebSysMenu.Property(e => e.IndexCode)
                    .HasColumnName("index_code")
                    .HasMaxLength(50);

            enWebSysMenu.Property(e => e.MenuIcon)
                    .HasColumnName("menu_icon")
                    .HasMaxLength(20);

            enWebSysMenu.Property(e => e.MenuItempages)
                    .HasColumnName("menu_itempages")
                    .HasMaxLength(3000);

            enWebSysMenu.Property(e => e.MenuName)
                    .HasColumnName("menu_name")
                    .HasMaxLength(50);

            enWebSysMenu.Property(e => e.MenuPid).HasColumnName("menu_pid");

            enWebSysMenu.Property(e => e.MenuSort).HasColumnName("menu_sort");

            enWebSysMenu.Property(e => e.MenuStatus).HasColumnName("menu_status");

            enWebSysMenu.Property(e => e.MenuUrl)
                    .HasColumnName("menu_url")
                    .HasMaxLength(200);

            enWebSysMenu.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");

            var enWebSysMenuPage = modelBuilder.Entity<WebSysMenuPage>();

            enWebSysMenuPage.HasKey(e => e.PageId);

            enWebSysMenuPage.ToTable("web_sys_menu_page");

            enWebSysMenuPage.Property(e => e.PageId).HasColumnName("page_id");

            enWebSysMenuPage.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

            enWebSysMenuPage.Property(e => e.MainStatus).HasColumnName("main_status");

            enWebSysMenuPage.Property(e => e.MenuId).HasColumnName("menu_id");

            enWebSysMenuPage.Property(e => e.PageBtnname)
                    .HasColumnName("page_btnname")
                    .HasMaxLength(50);

            enWebSysMenuPage.Property(e => e.PageName)
                    .HasColumnName("page_name")
                    .HasMaxLength(50);

            enWebSysMenuPage.Property(e => e.PageParamters)
                    .HasColumnName("page_paramters")
                    .HasMaxLength(300);

            enWebSysMenuPage.Property(e => e.PageStatus).HasColumnName("page_status");

            enWebSysMenuPage.Property(e => e.PageType).HasColumnName("page_type");

            enWebSysMenuPage.Property(e => e.PageUrl)
                    .IsRequired()
                    .HasColumnName("page_url")
                    .HasMaxLength(300);

            enWebSysMenuPage.Property(e => e.PageViewname)
                    .HasColumnName("page_viewname")
                    .HasMaxLength(50);

            enWebSysMenuPage.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");

            var enWebSysRole = modelBuilder.Entity<WebSysRole>();

            enWebSysRole.HasKey(e => e.RoleId);

            enWebSysRole.ToTable("web_sys_role");

            enWebSysRole.Property(e => e.RoleId).HasColumnName("role_id");

            enWebSysRole.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

            enWebSysRole.Property(e => e.RoleName)
                    .HasColumnName("role_name")
                    .HasMaxLength(50);

            enWebSysRole.Property(e => e.RoleRemark)
                    .HasColumnName("role_remark")
                    .HasMaxLength(100);

            enWebSysRole.Property(e => e.RoleStatus).HasColumnName("role_status");

            enWebSysRole.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");

            var enWebSysRoleMenu = modelBuilder.Entity<WebSysRoleMenu>();

            enWebSysRoleMenu.HasKey(e => e.AutoId);

            enWebSysRoleMenu.ToTable("web_sys_role_menu");

            enWebSysRoleMenu.Property(e => e.AutoId).HasColumnName("auto_id");

            enWebSysRoleMenu.Property(e => e.MenuId).HasColumnName("menu_id");

            enWebSysRoleMenu.Property(e => e.PageIds)
                    .HasColumnName("page_ids")
                    .HasMaxLength(300);

            enWebSysRoleMenu.Property(e => e.RoleId).HasColumnName("role_id");


        }

        /// <summary>
        /// web_sys_log
        /// </summary>
        public virtual DbSet<WebSysLog> WebSysLog { get; set; }
        /// <summary>
        /// web_sys_manager
        /// </summary>
        public virtual DbSet<WebSysManager> WebSysManager { get; set; }
        /// <summary>
        /// web_sys_manager_role
        /// </summary>
        public virtual DbSet<WebSysManagerRole> WebSysManagerRole { get; set; }
        /// <summary>
        /// web_sys_menu
        /// </summary>
        public virtual DbSet<WebSysMenu> WebSysMenu { get; set; }
        /// <summary>
        /// web_sys_menu_page
        /// </summary>
        public virtual DbSet<WebSysMenuPage> WebSysMenuPage { get; set; }
        /// <summary>
        /// web_sys_role
        /// </summary>
        public virtual DbSet<WebSysRole> WebSysRole { get; set; }
        /// <summary>
        /// web_sys_role_menu
        /// </summary>
        public virtual DbSet<WebSysRoleMenu> WebSysRoleMenu { get; set; }

    }
    /// <summary>
    ///web_sys_log
    /// </summary>
    public partial class WebSysLog
    {
        /// <summary>
        /// id
        /// </summary>
        [Display(Name = "id")]
        public int Id { get; set; }
        /// <summary>
        /// manager_guid
        /// </summary>
        [Display(Name = "manager_guid")]
        public string ManagerGuid { get; set; }
        /// <summary>
        /// log_type
        /// </summary>
        [Display(Name = "log_type")]
        public int? LogType { get; set; }
        /// <summary>
        /// log_content
        /// </summary>
        [Display(Name = "log_content")]
        public string LogContent { get; set; }
        /// <summary>
        /// log_time
        /// </summary>
        [Display(Name = "log_time")]
        public DateTime? LogTime { get; set; }
        /// <summary>
        /// log_name
        /// </summary>
        [Display(Name = "log_name")]
        public string LogName { get; set; }
        /// <summary>
        /// manager_account
        /// </summary>
        [Display(Name = "manager_account")]
        public string ManagerAccount { get; set; }
        /// <summary>
        /// map_method
        /// </summary>
        [Display(Name = "map_method")]
        public string MapMethod { get; set; }
        /// <summary>
        /// log_ip
        /// </summary>
        [Display(Name = "log_ip")]
        public string LogIp { get; set; }
    }
    /// <summary>
    ///web_sys_manager
    /// </summary>
    public partial class WebSysManager
    {
        /// <summary>
        /// manager_id
        /// </summary>
        [Display(Name = "manager_id")]
        public int ManagerId { get; set; }
        /// <summary>
        /// manager_name
        /// </summary>
        [Display(Name = "manager_name")]
        public string ManagerName { get; set; }
        /// <summary>
        /// manager_pwd
        /// </summary>
        [Display(Name = "manager_pwd")]
        public string ManagerPwd { get; set; }
        /// <summary>
        /// manager_scal
        /// </summary>
        [Display(Name = "manager_scal")]
        public string ManagerScal { get; set; }
        /// <summary>
        /// manager_realname
        /// </summary>
        [Display(Name = "manager_realname")]
        public string ManagerRealname { get; set; }
        /// <summary>
        /// manager_tel
        /// </summary>
        [Display(Name = "manager_tel")]
        public string ManagerTel { get; set; }
        /// <summary>
        /// manager_email
        /// </summary>
        [Display(Name = "manager_email")]
        public string ManagerEmail { get; set; }
        /// <summary>
        /// manager_isdel
        /// </summary>
        [Display(Name = "manager_isdel")]
        public int? ManagerIsdel { get; set; }
        /// <summary>
        /// manager_status
        /// </summary>
        [Display(Name = "manager_status")]
        public int? ManagerStatus { get; set; }
        /// <summary>
        /// create_time
        /// </summary>
        [Display(Name = "create_time")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// update_time
        /// </summary>
        [Display(Name = "update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// is_supper
        /// </summary>
        [Display(Name = "is_supper")]
        public int? IsSupper { get; set; }
        /// <summary>
        /// last_login_time
        /// </summary>
        [Display(Name = "last_login_time")]
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// cur_token
        /// </summary>
        [Display(Name = "cur_token")]
        public string CurToken { get; set; }
    }
    /// <summary>
    ///web_sys_manager_role
    /// </summary>
    public partial class WebSysManagerRole
    {
        /// <summary>
        /// auto_id
        /// </summary>
        [Display(Name = "auto_id")]
        public int AutoId { get; set; }
        /// <summary>
        /// manager_id
        /// </summary>
        [Display(Name = "manager_id")]
        public int? ManagerId { get; set; }
        /// <summary>
        /// role_id
        /// </summary>
        [Display(Name = "role_id")]
        public int? RoleId { get; set; }
    }
    /// <summary>
    ///web_sys_menu
    /// </summary>
    public partial class WebSysMenu
    {
        /// <summary>
        /// menu_id
        /// </summary>
        [Display(Name = "menu_id")]
        public int MenuId { get; set; }
        /// <summary>
        /// menu_name
        /// </summary>
        [Display(Name = "menu_name")]
        public string MenuName { get; set; }
        /// <summary>
        /// menu_pid
        /// </summary>
        [Display(Name = "menu_pid")]
        public int? MenuPid { get; set; }
        /// <summary>
        /// menu_icon
        /// </summary>
        [Display(Name = "menu_icon")]
        public string MenuIcon { get; set; }
        /// <summary>
        /// index_code
        /// </summary>
        [Display(Name = "index_code")]
        public string IndexCode { get; set; }
        /// <summary>
        /// menu_url
        /// </summary>
        [Display(Name = "menu_url")]
        public string MenuUrl { get; set; }
        /// <summary>
        /// menu_status
        /// </summary>
        [Display(Name = "menu_status")]
        public int? MenuStatus { get; set; }
        /// <summary>
        /// menu_itempages
        /// </summary>
        [Display(Name = "menu_itempages")]
        public string MenuItempages { get; set; }
        /// <summary>
        /// create_time
        /// </summary>
        [Display(Name = "create_time")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// update_time
        /// </summary>
        [Display(Name = "update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// menu_sort
        /// </summary>
        [Display(Name = "menu_sort")]
        public int? MenuSort { get; set; }
    }
    /// <summary>
    ///web_sys_menu_page
    /// </summary>
    public partial class WebSysMenuPage
    {
        /// <summary>
        /// page_id
        /// </summary>
        [Display(Name = "page_id")]
        public int PageId { get; set; }
        /// <summary>
        /// menu_id
        /// </summary>
        [Display(Name = "menu_id")]
        public int? MenuId { get; set; }
        /// <summary>
        /// main_status
        /// </summary>
        [Display(Name = "main_status")]
        public int? MainStatus { get; set; }
        /// <summary>
        /// page_name
        /// </summary>
        [Display(Name = "page_name")]
        public string PageName { get; set; }
        /// <summary>
        /// page_status
        /// </summary>
        [Display(Name = "page_status")]
        public int? PageStatus { get; set; }
        /// <summary>
        /// page_viewname
        /// </summary>
        [Display(Name = "page_viewname")]
        public string PageViewname { get; set; }
        /// <summary>
        /// page_btnname
        /// </summary>
        [Display(Name = "page_btnname")]
        public string PageBtnname { get; set; }
        /// <summary>
        /// page_type
        /// </summary>
        [Display(Name = "page_type")]
        public int? PageType { get; set; }
        /// <summary>
        /// page_url
        /// </summary>
        [Display(Name = "page_url")]
        public string PageUrl { get; set; }
        /// <summary>
        /// page_paramters
        /// </summary>
        [Display(Name = "page_paramters")]
        public string PageParamters { get; set; }
        /// <summary>
        /// create_time
        /// </summary>
        [Display(Name = "create_time")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// update_time
        /// </summary>
        [Display(Name = "update_time")]
        public DateTime? UpdateTime { get; set; }
    }
    /// <summary>
    ///web_sys_role
    /// </summary>
    public partial class WebSysRole
    {
        /// <summary>
        /// role_id
        /// </summary>
        [Display(Name = "role_id")]
        public int RoleId { get; set; }
        /// <summary>
        /// role_name
        /// </summary>
        [Display(Name = "role_name")]
        public string RoleName { get; set; }
        /// <summary>
        /// role_status
        /// </summary>
        [Display(Name = "role_status")]
        public int? RoleStatus { get; set; }
        /// <summary>
        /// create_time
        /// </summary>
        [Display(Name = "create_time")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// update_time
        /// </summary>
        [Display(Name = "update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// role_remark
        /// </summary>
        [Display(Name = "role_remark")]
        public string RoleRemark { get; set; }
    }
    /// <summary>
    ///web_sys_role_menu
    /// </summary>
    public partial class WebSysRoleMenu
    {
        /// <summary>
        /// auto_id
        /// </summary>
        [Display(Name = "auto_id")]
        public int AutoId { get; set; }
        /// <summary>
        /// role_id
        /// </summary>
        [Display(Name = "role_id")]
        public int? RoleId { get; set; }
        /// <summary>
        /// menu_id
        /// </summary>
        [Display(Name = "menu_id")]
        public int? MenuId { get; set; }
        /// <summary>
        /// page_ids
        /// </summary>
        [Display(Name = "page_ids")]
        public string PageIds { get; set; }
    }
}