using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebManagers.Core;
using FilmLove.Admin.ManagerBusiness;
using FilmLove.Admin.ManagerBusiness.Common;
using FilmLove.Admin.ManagerBusiness.SYSAdmin;
using FilmLove.Admin.WebManager.Models;
using YJYSoft.YL.Common;
using YJYSoft.YL.Common.WebHelper;

namespace FilmLove.Admin.Controllers
{
    [AllowAnonymous]
    public class WebEntranceController : Controller
    {
        WebSYSAccountManager accountManager = new WebSYSAccountManager();
        #region 登录
        public ActionResult Login()
        {
            if (SysLoginInfo.CurAccount() != null)
            {
                return RedirectToAction("Index", "WebHome");
            }
            else
            {
                SysLoginInfo.ClearSession();
                AuthenticationHelper.SignOut(ALLKeys.C_ADMINUSER);
            }
            return View("~/WebManager/Views/WebEntrance/Login.cshtml");
        }


        [AdminLogAttribute("登录", 3, "LoginName,VerCode")]
        public JsonResult DoLogin(string LoginName, string Password, string VerCode)
        {
            var result = accountManager.DoLogin(LoginName, Password, VerCode);
            return Json(result);
        }


        public void ValidateCode()
        {
            VerifyCode.ShowVerifyCode();
        }



#if INTRANET
        public JsonResult test()
        {
            var r = accountManager.GetAccountInfoByID(1);
            return Json(r, JsonRequestBehavior.AllowGet);
        }
#endif

        #endregion

        #region 退出
        [AdminLogAttribute("退出", 3)]
        public ActionResult Logout()
        {
            var user = SysLoginInfo.CurAccount();
            if (user != null)
            {
                accountManager.ClearToken(user.ManagerId);
            }

            SysLoginInfo.ClearSession();
            AuthenticationHelper.SignOut(ALLKeys.C_ADMINUSER);
            return RedirectToAction("Login");
        }
        #endregion
    }
}