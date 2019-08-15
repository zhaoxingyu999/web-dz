using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Tripartite.Netease.IM.Model.IMResEntity
{
    public class IMTeamQueryDetailRes
    {
        public int code { get; set; }

        public IMTeamQueryDetailTInfo tinfo { get; set; }
    }

    public class IMTeamQueryDetailTInfo
    {
        public List<IMTeamQueryDetailTInfoMember> members { get; set; }
    }

    public class IMTeamQueryDetailTInfoMember
    {
        public string accid { get; set; }
    }
}
