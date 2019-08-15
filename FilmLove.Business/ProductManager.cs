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
    public class ProductManager : DBObjects
    {
        public Product GetProductById(int? id)
        {
            return db.Product.FirstOrDefault(m => m.Id == id);
        }
        public AjaxResult ProductListSave(Product model)
        {
            Product ent = db.Product.FirstOrDefault(m => m.Id == model.Id);
            if (ent == null)
            {
                ent = model;
                db.Product.Add(ent);
            }
            else
            {
                ent.Name = model.Name;
                ent.Title = model.Title;
                ent.Type = model.Type;
                ent.Content = model.Content;
                ent.UpdateTime = DateTime.Now;
            }
            int r = db.SaveChanges();
            if (r <= 0)
                return new AjaxResult("保存失败");
            return new AjaxResult("保存成功", 0);
        }

        public AjaxResult ProductListLoad(CarouselPhotoListReq req)
        {
            var q = from t in db.Product select t;
            
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
