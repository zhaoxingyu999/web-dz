using FilmLove.Common;
using FilmLove.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Business.API
{
    public class ProductModel : DBObjects
    {
        public int GetInfo(out object list)
        {
            list = db.Product
                 .OrderByDescending(o => o.Id)
                .Select(s => new
                {
                    name = s.Name,
                    title = s.Title,
                    type = s.Type,
                    content = s.Content,
                    id = s.Id
                }).ToList();
            return 0;
        }
    }
}
