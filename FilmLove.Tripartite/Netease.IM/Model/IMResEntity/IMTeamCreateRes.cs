using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Tripartite.Netease.IM.Model.IMResEntity
{
    public class IMTeamCreateRes
    {
        public int code { get; set; }
        public string tid { get; set; }
        public IMTeamCreateFaccidInfo faccid { get; set; }
    }


    public  class IMTeamCreateFaccidInfo
    {
        public List<string> accid { get; set; }
        public string msg { get; set; }
    }
}
