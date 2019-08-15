using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Tripartite.Login.QQ
{
    public class QQOpenIdInfo
    {
        public string client_id { get; set; }
        public string openid { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
    }
}
