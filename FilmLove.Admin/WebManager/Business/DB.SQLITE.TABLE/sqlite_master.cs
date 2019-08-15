using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB.SQLITE.TABLE
{
    public class sqlite_master
    {
        public string type { get; set; }
        public string name { get; set; }
        public string tbl_name { get; set; }
        public string rootpage { get; set; }
        public string sql { get; set; }
    }
}