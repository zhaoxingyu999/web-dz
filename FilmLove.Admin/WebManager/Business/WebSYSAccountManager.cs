using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FilmLove.Admin.ManagerBusiness.Common;
using FilmLove.Admin.CommEntity.Entity;
using FilmLove.Admin.WebManager.Models;
using YJYSoft.YL.Common;
using YJYSoft.YL.Common.WebHelper;
using WebManagers.Core;
using WebManagers.Core.Entity;

namespace FilmLove.Admin.ManagerBusiness.SYSAdmin
{
    public class WebSYSAccountManager : DBObjects
    {
        /// <summary>
        /// 清除登陆Token
        /// </summary>
        /// <param name="UserId"></param>
        public void ClearToken(long ManagerId)
        {
            var user = db.WebSysManager.FirstOrDefault(m => m.ManagerId == ManagerId);
            if (user != null)
            {
                user.CurToken = "";
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 根据ID获取账户
        /// </summary>
        /// <param name="ManagerId"></param>
        /// <returns></returns>
        public WebSysManager GetAccountInfoByID(long ManagerId)
        {
            var user = db.WebSysManager.FirstOrDefault(m => m.ManagerId == ManagerId);
            return user;
        }

        /// <summary>
        /// 根据登陆名获取账户
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public WebSysManager GetAccountByName(string Account)
        {
            var user = db.WebSysManager.FirstOrDefault(m => m.ManagerName == Account);
            return user;
        }

        public AjaxResult DoLogin(string LoginName, string Password, string VerCode)
        {
            if (string.IsNullOrEmpty(LoginName))
                return new AjaxResult("请输入登录账号");
            if (string.IsNullOrEmpty(Password))
                return new AjaxResult("请输入登录密码");
            if (string.IsNullOrEmpty(VerCode))
                return new AjaxResult("请输入登录验证码");
            //检查验证码
            if (!VerifyCode.CheckVerifyCode(VerCode))
                return new AjaxResult("验证码错误");
            var sysUser = GetAccountByName(LoginName);
            if (sysUser == null)
                return new AjaxResult("登录账号无效");

            if (sysUser.ManagerPwd != Encrypt.MD5Encrypt(Password + sysUser.ManagerScal))
                return new AjaxResult("登陆密码错误");
            DateTime dtNow = DateTime.Now;

            sysUser.LastLoginTime = dtNow;
            sysUser.CurToken = Encrypt.MD5Encrypt(Guid.NewGuid().ToString());
            db.SaveChanges();
            sysUser.ManagerPwd = "";
            sysUser.ManagerScal = "";


            var cookieInfo = new AdminCookieInfo()
            {
                ManagerAccount = sysUser.ManagerName,
                LastLoginTime = dtNow,
                LoginToken = sysUser.CurToken,
                ManagerId = sysUser.ManagerId,
            };
            SysManager manager = new SysManager()
            {
                IsSupper = sysUser.IsSupper,
                ManagerId = sysUser.ManagerId,
                ManagerName = sysUser.ManagerName,
                ManagerRealname = sysUser.ManagerRealname
            };
            SysLoginInfo.SetCurAccount(manager);

            #region 权限页面内容写入
            List<WebSysMenuPage> autoPages = GetAuthPages(manager);
            List<SysMenuPage> pages = autoPages.Select(s => new SysMenuPage()
            {
                MenuId = s.MenuId,
                PageBtnname = s.PageBtnname,
                PageId = s.PageId,
                PageName = s.PageName,
                PageUrl = s.PageUrl,
                PageViewname = s.PageViewname

            }).ToList();
            SysLoginInfo.SetCurMenuPages(pages);
            #endregion

            AuthenticationHelper.SignIn(ALLKeys.C_ADMINUSER, sysUser.ManagerName, Newtonsoft.Json.JsonConvert.SerializeObject(cookieInfo), 60 * 8);
            return new AjaxResult(sysUser);
        }

        /// <summary>
        /// 获取账户相关权限页面
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public List<WebSysMenuPage> GetAuthPages(SysManager sysUser)
        {
            List<WebSysMenuPage> autoPages = new List<WebSysMenuPage>();
            if (sysUser.IsSupper != 1)
            {
                var roleIds = db.WebSysManagerRole.Where(m => m.ManagerId == sysUser.ManagerId).Select(m => m.RoleId).Distinct().ToList();
                List<WebSysRoleMenu> roleMenus = db.WebSysRoleMenu.Where(m => roleIds.Contains(m.RoleId)).ToList();
                List<int> pageIds = new List<int>();
                foreach (var item in roleMenus)
                {
                    if (string.IsNullOrEmpty(item.PageIds))
                        continue;
                    string[] pageidArr = item.PageIds.Split(',');
                    foreach (var pageid in pageidArr)
                    {
                        int ipageId = ConvertN.ToInt32(pageid);
                        if (!pageIds.Contains(ipageId))
                            pageIds.Add(ConvertN.ToInt32(pageid));
                    }
                }
                autoPages = db.WebSysMenuPage.Where(m => pageIds.Contains(m.PageId)).ToList();
            }
            else
            {
                autoPages = db.WebSysMenuPage.ToList();
            }
            return autoPages;
        }
    }
}
