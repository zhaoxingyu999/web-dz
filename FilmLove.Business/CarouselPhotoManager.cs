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
   public class CarouselPhotoManager: DBObjects
    {    
        public CarouselPhoto GetCarouselPhotoById(int? id)
        {
            return db.CarouselPhoto.FirstOrDefault(m => m.Id == id);
        }
        public AjaxResult CarouselPhotoListSave(CarouselPhoto model)
        {
            CarouselPhoto ent = db.CarouselPhoto.FirstOrDefault(m => m.Id == model.Id);
            if (ent == null)
            {
                ent = model;
                ent.CreateTime = DateTime.Now;
                ent.IsActive = 1;
                db.CarouselPhoto.Add(ent);
            }
            else
            {
                ent.CreateTime = DateTime.Now;
                ent.Author = model.Author;
                ent.Content = model.Content;
                ent.Time = model.Time;
                ent.ImgUrl = model.ImgUrl;
                ent.Remark = model.Remark;
                ent.Description = model.Description;
                ent.Title = model.Title;
                ent.Headline = model.Headline;
            }
            int r = db.SaveChanges();
            if (r <= 0)
                return new AjaxResult("保存失败");
            return new AjaxResult("保存成功", 0);
        }
        public AjaxResult CarouselPhotoListLoad(CarouselPhotoListReq req)
        {
            var q = from t in db.CarouselPhoto  where t.IsActive==1 select t;
            //过滤条件
            if (!string.IsNullOrWhiteSpace(req.Title))
                q = q.Where(w => w.Title.Contains(req.Title));
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
