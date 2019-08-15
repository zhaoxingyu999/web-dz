using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB.SQLITE.TABLE
{
    public class OperLogs
    {
        public int ID { get; set; }
        public string URL { get; set; }
        public string Param { get; set; }
        public string UserInfo { get; set; }
        public DateTime CreateTime { get; set; }
        public string IP { get; set; }
    }
}