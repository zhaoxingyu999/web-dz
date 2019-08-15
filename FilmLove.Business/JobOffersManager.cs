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
   public class JobOffersManager:DBObjects
    {
        public JobOffers GetJobOffersById(int? id)
        {
            return db.JobOffers.FirstOrDefault(m => m.Id == id);
        }
        public AjaxResult JobOffersListSave(JobOffers model)
        {
            JobOffers ent = db.JobOffers.FirstOrDefault(m => m.Id == model.Id);
            if (ent == null)
            {
                ent = model;
                ent.CreateTime = DateTime.Now;
                ent.IsActive = 1;
                db.JobOffers.Add(ent);
            }
            else
            {
                ent.Name = model.Name;
                ent.Position = model.Position;
                ent.Task = model.Task;
                ent.Count = model.Count;
                ent.Time = model.Time;
                ent.CreateTime = DateTime.Now;
            }
            int r = db.SaveChanges();
            if (r <= 0)
                return new AjaxResult("保存失败");
            return new AjaxResult("保存成功", 0);
        }

        public AjaxResult JobOffersListLoad(JobOffersListReq req)
        {
            var q = from t in db.JobOffers where t.IsActive==1 select t;
            //过滤条件
            if (!string.IsNullOrWhiteSpace(req.Name))
                q = q.Where(w => w.Name.Contains(req.Name));
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
