using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmLove.Admin.WebManager.Models;
using YJYSoft.YL.Common;

namespace FilmLove.Admin.WebManager.Models
{
    public class DBObjects : IDisposable
    {
        private bool _disposed = false;

        private web_managerEntities _db;

        public DBObjects()
        {

        }
        public DBObjects(web_managerEntities gLEntities)
        {
            _db = gLEntities;
        }
        /// <summary>
        /// 优购啦数据库访问
        /// </summary>
        public web_managerEntities db
        {
            get
            {
                if (_db == null)
                    _db = new web_managerEntities(Encrypt.StringDecodeOne(DBConfigure.ConnStr));
                return _db;
            }
            set
            {
                this._db = value;
            }
        }

        ~DBObjects()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //释放如果释放
                }
                if (_db != null)
                {
                    _db.Dispose();
                }
                _disposed = true;
            }
        }

    }
}
