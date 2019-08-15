using DB.SQLITE;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using FilmLove.Admin.ManagerBusiness.Common;
using FilmLove.Admin.CommEntity.Entity;
using FilmLove.Admin.ManagerBusiness.SYSAdmin;
using FilmLove.Admin.WebManager.Models;
using YJYSoft.YL.Common;
using YJYSoft.YL.Common.WebHelper;
using WebManagers.Core;
using WebManagers.Core.Entity;

namespace FilmLove.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected SysManager CurAccount
        {
            get
            {
                return AdminUser.CurAccount;
            }
        }

        protected List<SysMenuPage> CurAuthPages
        {
            get
            {
                return AdminUser.CurAuthPages;
            }
        }
    }
}