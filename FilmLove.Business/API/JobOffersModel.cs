using FilmLove.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YJYSoft.YL.Common;

namespace FilmLove.Business.API
{
    public class JobOffersModel:DBObjects
    {
        public int GetInfo(out object list)
        {
            list = db.JobOffers
                .Where(o=>o.IsActive==1)
                .OrderByDescending(o => o.CreateTime)
                .Select(s => new {
                   id=s.Id,
                   name=s.Name,
                   count=s.Count,
                   position=s.Position,
                   task=s.Task,
                   time=s.Time,
                   createtime=s.CreateTime
                }).Take(3).ToList();
            return 0;
        }
    }
}
