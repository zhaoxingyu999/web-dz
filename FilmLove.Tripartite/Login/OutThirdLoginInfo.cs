using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Tripartite.Login
{
    public class OutThirdLoginInfo
    {
        public int isbinding { get; set; }
        public string wxtoken { get; set; }
        public int? uid { get; set; }
        public string token { get; set; }
        public int isFinish { get; set; }

        public string imtoken { get; set; }

        public LUserInfo info { get; set; }
        public class LUserInfo
        {
            public string headImg { get; set; }
            public string sex { get; set; }
            public DateTime? birthday { get; set; }

            public string nickname { get; set; }
        }
    }
}
