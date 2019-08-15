using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
namespace FilmLove.Database.Entity
{
  
    public partial class OfficialWebEntities : DbContext
    {
        public bool IsDisposed { get; set; }
        public OfficialWebEntities(string nameOrConnectionString="name = OfficialWebEntities")
            : base(nameOrConnectionString)
        {
            IsDisposed = false;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            IsDisposed = true;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
                        var enAboutUs = modelBuilder.Entity<AboutUs>();

                        enAboutUs.HasKey(e =>e.Id);

                        enAboutUs.ToTable("AboutUs");

                        enAboutUs.Property(e => e.Id).HasColumnName("ID");

                        enAboutUs.Property(e => e.Content).HasMaxLength(4000);

                        enAboutUs.Property(e => e.Name).HasMaxLength(100);

                        enAboutUs.Property(e => e.UpdateTime).HasColumnType("datetime");

                        var enCarouselPhoto = modelBuilder.Entity<CarouselPhoto>();

                        enCarouselPhoto.HasKey(e =>e.Id);

                        enCarouselPhoto.ToTable("CarouselPhoto");

                        enCarouselPhoto.Property(e => e.Id).HasColumnName("ID");

                        enCarouselPhoto.Property(e => e.Author).HasMaxLength(500);

                        enCarouselPhoto.Property(e => e.Content).HasColumnType("text");

                        enCarouselPhoto.Property(e => e.CreateTime).HasColumnType("datetime");

                        enCarouselPhoto.Property(e => e.Description).HasMaxLength(500);

                        enCarouselPhoto.Property(e => e.Headline).HasMaxLength(500);

                        enCarouselPhoto.Property(e => e.ImgUrl).HasMaxLength(500);

                        enCarouselPhoto.Property(e => e.Remark).HasMaxLength(500);

                        enCarouselPhoto.Property(e => e.Time).HasMaxLength(500);

                        enCarouselPhoto.Property(e => e.Title).HasMaxLength(500);

                        var enCopyright = modelBuilder.Entity<Copyright>();

                        enCopyright.HasKey(e =>e.Id);

                        enCopyright.ToTable("Copyright");

                        enCopyright.Property(e => e.Id).HasColumnName("ID");

                        enCopyright.Property(e => e.Address)
                                .HasColumnName("address")
                                .HasMaxLength(300);

                        enCopyright.Property(e => e.Email)
                                .HasColumnName("email")
                                .HasMaxLength(300);

                        enCopyright.Property(e => e.Tel)
                                .HasColumnName("tel")
                                .HasMaxLength(300);

                        enCopyright.Property(e => e.UpdateTime).HasColumnType("datetime");

                        var enJobOffers = modelBuilder.Entity<JobOffers>();

                        enJobOffers.HasKey(e =>e.Id);

                        enJobOffers.ToTable("JobOffers");

                        enJobOffers.Property(e => e.Id).HasColumnName("ID");

                        enJobOffers.Property(e => e.Count).HasMaxLength(500);

                        enJobOffers.Property(e => e.CreateTime).HasColumnType("datetime");

                        enJobOffers.Property(e => e.Name).HasMaxLength(500);

                        enJobOffers.Property(e => e.Position).HasMaxLength(500);

                        enJobOffers.Property(e => e.Task).HasColumnType("text");

                        enJobOffers.Property(e => e.Time).HasMaxLength(500);

                        var enProduct = modelBuilder.Entity<Product>();

                        enProduct.HasKey(e =>e.Id);

                        enProduct.ToTable("Product");

                        enProduct.Property(e => e.Id).HasColumnName("ID");

                        enProduct.Property(e => e.Content).HasColumnType("text");

                        enProduct.Property(e => e.Name).HasMaxLength(50);

                        enProduct.Property(e => e.Title).HasMaxLength(100);

                        enProduct.Property(e => e.UpdateTime).HasColumnType("datetime");

                        var enWebSysLog = modelBuilder.Entity<WebSysLog>();

                        enWebSysLog.HasKey(e =>e.Id);

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

                        enWebSysManager.HasKey(e =>e.ManagerId);

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

                        enWebSysManagerRole.HasKey(e =>e.AutoId);

                        enWebSysManagerRole.ToTable("web_sys_manager_role");

                        enWebSysManagerRole.Property(e => e.AutoId).HasColumnName("auto_id");

                        enWebSysManagerRole.Property(e => e.ManagerId).HasColumnName("manager_id");

                        enWebSysManagerRole.Property(e => e.RoleId).HasColumnName("role_id");

                        var enWebSysMenu = modelBuilder.Entity<WebSysMenu>();

                        enWebSysMenu.HasKey(e =>e.MenuId);

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

                        enWebSysMenuPage.HasKey(e =>e.PageId);

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

                        enWebSysRole.HasKey(e =>e.RoleId);

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

                        enWebSysRoleMenu.HasKey(e =>e.AutoId);

                        enWebSysRoleMenu.ToTable("web_sys_role_menu");

                        enWebSysRoleMenu.Property(e => e.AutoId).HasColumnName("auto_id");

                        enWebSysRoleMenu.Property(e => e.MenuId).HasColumnName("menu_id");

                        enWebSysRoleMenu.Property(e => e.PageIds)
                                .HasColumnName("page_ids")
                                .HasMaxLength(300);

                        enWebSysRoleMenu.Property(e => e.RoleId).HasColumnName("role_id");


        }

                /// <summary>
                /// AboutUs
                /// </summary>
                public virtual DbSet<AboutUs> AboutUs { get; set; }
                /// <summary>
                /// CarouselPhoto
                /// </summary>
                public virtual DbSet<CarouselPhoto> CarouselPhoto { get; set; }
                /// <summary>
                /// Copyright
                /// </summary>
                public virtual DbSet<Copyright> Copyright { get; set; }
                /// <summary>
                /// JobOffers
                /// </summary>
                public virtual DbSet<JobOffers> JobOffers { get; set; }
                /// <summary>
                /// Product
                /// </summary>
                public virtual DbSet<Product> Product { get; set; }
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
}