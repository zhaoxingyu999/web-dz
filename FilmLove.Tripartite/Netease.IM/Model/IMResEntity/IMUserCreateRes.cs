using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Tripartite.Netease.IM.Model.IMResEntity
{
    public class IMUserCreateRes
    {
        public int code { get; set; }
        public string desc { get; set; }
        public Info info { get; set; }
    }

    public class Info
    {
        public string token { get; set; }
        public string accid { get; set; }
        public string name { get; set; }
    }
}
