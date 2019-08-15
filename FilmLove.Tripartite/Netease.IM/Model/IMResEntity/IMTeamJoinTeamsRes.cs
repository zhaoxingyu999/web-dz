using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Tripartite.Netease.IM.Model.IMResEntity
{
    public class IMTeamJoinTeamsRes
    {
        public int code { get; set; }
        public int count { get; set; }
        public List<IMTeamJoinTeamsInfo> infos { get; set; }
    }


    public class IMTeamJoinTeamsInfo
    {
        public string owner { get; set; }
        public string tname { get; set; }
        public string maxusers { get; set; }
        public string tid { get; set; }
        public string size { get; set; }
        public string custom { get; set; }
    }
}
