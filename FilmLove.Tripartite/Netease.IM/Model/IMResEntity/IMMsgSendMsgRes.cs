using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Tripartite.Netease.IM.Model.IMResEntity
{
    public class IMMsgSendMsgRes
    {
        public int code { get; set; }
        public IMMsgSendMsgData data { get; set; }
    }

    public class IMMsgSendMsgData
    {
        public long msgid { get; set; }
        public long timetag { get; set; }
        public bool antispam { get; set; }
    }
}
