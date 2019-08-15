using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Tripartite.Netease.IM.Model.IMResEntity
{
    public class IMUserRefreshTokenRes
    {
        public int code { get; set; }
        public string desc { get; set; }
        public IMUserRefreshTokenInfo info { get; set; }
    }

    public class IMUserRefreshTokenInfo
    {
        public string token { get; set; }
        public string accid { get; set; }
    }
}
