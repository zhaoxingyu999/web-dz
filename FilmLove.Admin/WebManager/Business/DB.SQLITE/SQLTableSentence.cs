using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB.SQLITE
{
    public class SQLTableSentence
    {
        /// <summary>
        /// 表名：ErrorLogs 错误日志表
        /// </summary>
        public const string SQL_ErrorLogs = @"
CREATE TABLE ErrorLogs (
    ID         INTEGER       PRIMARY KEY AUTOINCREMENT,
    ErrorType  VARCHAR (300),
    URL        VARCHAR (300),
    MoreTxt    TEXT,
    ErrorTxt    TEXT,
    CreateTime DATETIME,
    IP         VARCHAR (100) 
);

";
        /// <summary>
        /// 表名：LC_Record_Infrared 操作记录表
        /// </summary>
        public const string SQL_OperLogs = @"
CREATE TABLE OperLogs (
    ID         INTEGER       PRIMARY KEY AUTOINCREMENT,
    URL        VARCHAR (300),
    Param      TEXT,
    UserInfo   TEXT,
    CreateTime DATETIME,
    IP         VARCHAR (100) 
);
";

    }
}
