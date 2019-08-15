using DB.SQLITE;
using DB.SQLITE.TABLE;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using YJYSoft.YL.Common;

namespace FilmLove.Admin.Areas.Logs.Models
{
    public class OperLogsModel
    {
        internal AjaxResult List(PageModel model)
        {
            Pagination<OperLogs> page = new Pagination<OperLogs>();
            var c = SqliteAdoSessionManager.Current;
            var count = c.OperLogsDB.GetSingle("select count(1) from OperLogs");
            page.TotalCount = int.Parse(count.ToString());

            int a = 0;
            int b = 20;
            if (model.PageSize > 100)
                model.PageSize = 100;
            if (model.PageSize < 1)
                model.PageSize = 20;
            a = (model.PageIndex - 1) * model.PageSize;
            b = model.PageSize;
            string sql = string.Format("select * from OperLogs order by id desc limit {0} offset {1}", b, a);
            page.dataList = c.OperLogsDB.Query2<OperLogs>(sql).ToList();

            return new AjaxResult(page);

        }
    }
}