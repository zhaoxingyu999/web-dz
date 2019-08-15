using FilmLove.Business.Entity.Request;
using FilmLove.Database;
using FilmLove.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YJYSoft.YL.Common;

namespace FilmLove.Business
{
   public class CopyrightManager: DBObjects
    {
        public Copyright GetCopyrightById(int? id)
        {
            return db.Copyright.FirstOrDefault(m => m.Id == id);
        }

        public AjaxResult CopyrightListSave(Copyright model)
        {
            Copyright ent = db.Copyright.FirstOrDefault(m => m.Id == model.Id);
            if(ent==null)
            {
                ent = model;
                ent.Address = "重庆渝北创意公园13栋5单元5层";
                ent.Tel = "023 - 81211678";
                ent.Email = "MARKET@DUALPSY.COM";
                ent.UpdateTime = DateTime.Now;
                db.Copyright.Add(ent);
            }else
            {
                ent.Address = model.Address;
                ent.Email = model.Email;
                ent.Tel = model.Tel;
                ent.UpdateTime = DateTime.Now;
            }
            int r = db.SaveChanges();
            if (r <= 0)
                return new AjaxResult("保存失败");
            return new AjaxResult("保存成功", 0);

        }

        public AjaxResult CopyrightSListLoad(CarouselPhotoListReq req)
        {
            var q = from t in db.Copyright select t;
            //
            var dataList = q.OrderByDescending(m => m.Id).Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize).ToList();
            var TotalCount = q.Count();
            var page = new
            {
                TotalCount = TotalCount,
                dataList = dataList,
            };
            return new AjaxResult(page);
        }
    }
}
