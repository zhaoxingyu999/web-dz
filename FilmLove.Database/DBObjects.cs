using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmLove.Database.Entity;
using YJYSoft.YL.Common;

namespace FilmLove.Database
{
    public class DBObjects
    {
        private OfficialWebEntities _db;

        public DBObjects()
        {

        }

        public DBObjects(OfficialWebEntities gLEntities)
        {
            this._db = gLEntities;
        }

        /// <summary>
        /// 优购啦数据库访问
        /// </summary>
        public OfficialWebEntities db
        {
            get
            {
                if (this._db == null || this._db.IsDisposed)
                {
                    this._db = new OfficialWebEntities(Encrypt.StringDecodeOne(DBConfigure.ConnStr));
                }
                try
                {
                    var v = _db.Database.ExecuteSqlCommand("select 1;");
                }
                catch
                {
                    this._db = new OfficialWebEntities(Encrypt.StringDecodeOne(DBConfigure.ConnStr));
                }
                return this._db;
            }
            set
            {
                this._db = value;
            }
        }

    }
}
