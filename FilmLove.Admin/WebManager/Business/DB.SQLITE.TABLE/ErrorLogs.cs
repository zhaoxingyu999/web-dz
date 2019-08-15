using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB.SQLITE.TABLE
{
    public class ErrorLogs
    {
        public int ID { get; set; }
        public string ErrorType { get; set; }
        public string URL { get; set; }
        public string MoreTxt { get; set; }
        public string ErrorTxt { get; set; }
        public DateTime CreateTime { get; set; }
        public string IP { get; set; }
    }
}