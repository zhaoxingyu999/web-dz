using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Admin.CommEntity.ResEntities
{
    public class ClickButtonModel
    {
        public string BtnId { get; set; }
        public string BtnDoUrl { get; set; }
        public string BtnClassName { get; set; }

        public ClickButtonModel()
        {

        }

        public ClickButtonModel(string btnid, string dourl, string classname)
        {
            this.BtnId = btnid;
            this.BtnDoUrl = dourl;
            this.BtnClassName = classname;

        }
    }
}
