using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Admin.CommEntity.ResEntities
{
    public class NavigationModel
    {
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Title3 { get; set; }

        public bool IsBack { get; set; }

        public NavigationModel()
        {

        }

        public NavigationModel(string t1, string t2, bool isback = false)
        {
            this.Title1 = t1;
            this.Title2 = t2;
            this.Title3 = "";
            this.IsBack = isback;
        }

        public NavigationModel(string t1, string t2, string t3, bool isback = false)
        {
            this.Title1 = t1;
            this.Title2 = t2;
            this.Title3 = t3;
            this.IsBack = isback;
        }
    }
}
