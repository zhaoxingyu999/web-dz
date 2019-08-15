using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FilmLove.Common
{
    public class LogFileTool
    {
        static ReaderWriterLockSlim logWriteLock = new ReaderWriterLockSlim();
        /// <summary>
        /// 写入日志文本文件
        /// </summary>
        public static void WriteLog(string savePath, string log)
        {
            log = string.Format("==================={0:yyyy-MM-dd HH:mm:ss.fff}===================\n", DateTime.Now) + log;
            WriteLog(AppDomain.CurrentDomain.BaseDirectory.ToString(), savePath, log);
        }

        /// <summary>
        /// 写入日志文本文件
        /// </summary>
        public static void WriteLog(string filePath, string savePath, string log)
        {
            try
            {
                logWriteLock.EnterWriteLock();
                string logFilePath = filePath + savePath;
                if (!Directory.Exists(logFilePath))
                {
                    Directory.CreateDirectory(logFilePath);
                }
                string strFileName = logFilePath + string.Format("{0:yyyy-MM-dd}.txt", DateTime.Now);
                using (StreamWriter sw = new StreamWriter(strFileName, true, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(log);
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch
            {

            }
            finally
            {
                logWriteLock.ExitWriteLock();
            }
        }
    }
}
