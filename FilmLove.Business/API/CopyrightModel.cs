using FilmLove.Common;
using FilmLove.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FilmLove.Business.API
{
    public class CopyrightModel : DBObjects
    {
        public int GetInfo(out object list, int? id)
        {
            list = db.Copyright.Where(m=>m.Id==id)              
                .Select(s => new {
                   email=s.Email,
                   address=s.Address,
                   tel=s.Tel,
                   id=s.Id
                }).ToList();
            return 0;
        }
    }
}
