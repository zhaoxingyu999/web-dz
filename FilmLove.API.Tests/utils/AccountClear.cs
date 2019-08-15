using FilmLove.Business.API;
using FilmLove.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.API.Tests.utils
{
    class AccountClear
    {
        filmloveEntities db = new filmloveEntities();
        internal void ClearUser(string phone)
        {
            db.BaseUserData.RemoveRange(db.BaseUserData.Where(w=>w.UserId== db.BaseUser.Where(ww => ww.Phone == phone).Select(s=>s.UserId).FirstOrDefault()));
            db.UserGold.RemoveRange(db.UserGold.Where(w=>w.UserId== db.BaseUser.Where(ww => ww.Phone == phone).Select(s=>s.UserId).FirstOrDefault()));
            db.UserThirdAuthInfo.RemoveRange(db.UserThirdAuthInfo.Where(w => w.UserId == db.BaseUser.Where(ww => ww.Phone == phone).Select(s => s.UserId).FirstOrDefault()));

            db.BaseUser.RemoveRange(db.BaseUser.Where(w => w.Phone == phone));
            db.SaveChanges();
        }

        internal void AddUser(string phone)
        {
            var v = db.BaseUser.Add(new BaseUser()
            {
                Phone = phone,
                NickName = "",
                Number = Guid.NewGuid().ToString("N"),
                Scal = "",
                CreatTime = DateTime.Now,
            });

            db.SaveChanges();
        }
        internal void AddUser2(string phone)
        {
            var v = db.BaseUser.Add(new BaseUser()
            {
                Phone = phone,
                NickName = "",
                Number = Guid.NewGuid().ToString("N"),
                Scal = "",
                CreatTime = DateTime.Now,
                Status = 1,
            });

            db.SaveChanges();
        }

        internal void AddUserBing(string phone, ThreeLoginInfo three)
        {
            var v = db.BaseUser.Add(new BaseUser()
            {
                Phone = phone,
                NickName = "",
                Number = Guid.NewGuid().ToString("N"),
                Scal = "",
                CreatTime = DateTime.Now,
            });
            db.SaveChanges();
            db.UserThirdAuthInfo.Add(new UserThirdAuthInfo()
            {
                UserId = v.UserId,
                AuthType = three.type,
                AutoCode = three.openid,
            });
            db.SaveChanges();
        }

        internal void AddBing(string phone, ThreeLoginInfo three)
        {
            db.UserThirdAuthInfo.Add(new UserThirdAuthInfo()
            {
                UserId = 0,
                AuthType = three.type,
                AutoCode = three.openid,
            });
            db.SaveChanges();
        }
    }
}
