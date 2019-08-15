using FilmLove.Common;
using FilmLove.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Business.API
{
   public  class AboutUSModel:DBObjects
    {
        public int GetInfo(out object list)
        {
            list = db.AboutUs
                 .OrderByDescending(o => o.Id)
                .Select(s => new
                {
                    content = s.Content,
                    type = s.Type,
                    name=s.Name,
                    id = s.Id
                }).ToList();
            return 0;
        }
    }
}
