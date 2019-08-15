using FilmLove.Common;
using FilmLove.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Business.API
{
    public class CarouselPhotoModel : DBObjects
    {
        public int GetInfo(out object list,  int? carid)
        {
            list = db.CarouselPhoto.WhereIf(carid!=null,a =>a.Id==carid&&a.IsActive==1)
                .OrderByDescending(o=>o.CreateTime)
                .Select(s=>new {
                imgUrl=s.ImgUrl,
                title=s.Title,
                content=s.Content,
                time=s.Time,
                desc=s.Description,
                remake=s.Remark,
                author=s.Author,
                headline=s.Headline,
                id =s.Id
            }).Take(3).ToList();
            return 0;
        }
    }
}
