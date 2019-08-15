using FilmLove.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.API.Tests.utils
{
    internal class SmsClear
    {
        filmloveEntities db = new filmloveEntities();
        public void ClearBing(string phone)
        {
            db.MainSms.RemoveRange(db.MainSms.Where(w => w.Phone == phone));
            db.UserThirdAuthInfo.RemoveRange(
                db.UserThirdAuthInfo.
                    Where(w => w.UserId == db.BaseUser.Where(ww => ww.Phone == phone).Select(s => s.UserId).FirstOrDefault())
               );
            db.SaveChanges();
        }

        public void ClearSms(string phone)
        {
            db.MainSms.RemoveRange(db.MainSms.Where(w => w.Phone == phone));
            db.SaveChanges();
        }

        public void ChangeSendTime(string phone)
        {
            var v = db.MainSms.Where(w => w.Phone == phone).FirstOrDefault();
            v.PublishTime = v.PublishTime.AddSeconds(-60);
            db.SaveChanges();
        }

        internal void AddBingTestUser(string phone,int type)
        {
            var v = db.BaseUser.Add(new BaseUser() {
                Phone=phone,
                NickName="",
                Number="",
                Scal="",
                CreatTime =DateTime.Now,
            });
            db.SaveChanges();
            db.UserThirdAuthInfo.Add(new UserThirdAuthInfo() {
                UserId = v.UserId,
                AuthType = type,
                AutoCode="1"
            });
            db.SaveChanges();
        }

        internal void ClearBingTestUser(string phone)
        {
            db.UserThirdAuthInfo.RemoveRange(
                db.UserThirdAuthInfo.Where(w => w.UserId == db.BaseUser.Where(ww => ww.Phone == phone).Select(s => s.UserId).FirstOrDefault())
                ); ;
            db.BaseUser.RemoveRange(db.BaseUser.Where(w=>w.Phone==phone));
            db.SaveChanges();
        }
    }
}
