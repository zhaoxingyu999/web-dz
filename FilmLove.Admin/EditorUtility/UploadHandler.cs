using FilmLove.Admin;
using System;
using System.IO;
using System.Linq;
using System.Web;


namespace EditorUtility
{
	public class CSPUploadResult
    {
        public string fpath { get; set; }
        public bool status { get; set; }
        public string path { get; set; }
        public string message { get; set; }
        public string code { get; set; }
    }

    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : Handler
    {

        public UploadConfig UploadConfig { get; private set; }
        public UploadResult Result { get; private set; }

        public UploadHandler(HttpContext context, UploadConfig config)
            : base(context)
        {
            this.UploadConfig = config;
            this.Result = new UploadResult() { State = UploadState.Unknown };
        }

        public override void Process()
        {
            var file = Request.Files[UploadConfig.UploadFieldName];

            string uploadFileName = file.FileName;
            Result.SourceFileName = uploadFileName;
            Result.FileSize = string.Format("{0:F2}M", file.ContentLength / 1024M / 1024M);
            Result.LimitSize = string.Format("{0:F2}M", UploadConfig.SizeLimit / 1024M / 1024M);
            if (!CheckFileType(uploadFileName))
            {
                Result.State = UploadState.TypeNotAllow;
                WriteResult();
                return;
            }
            if (!CheckFileSize(file.ContentLength))
            {
                Result.State = UploadState.SizeLimitExceed;
                WriteResult();
                return;
            }

            try
            {
                string strFileType = "EditorFile";
                if(UploadConfig.ActionName == "uploadimage")
                {
                    strFileType = "EditorImg";
                }
                var result = FileUploadHelper.UploadFileOne(strFileType);
                if (result.code == 0)
                {
                    var dEnt = (FileUploadR)result.data;
                    Result.Url = dEnt.urlPath;
                    Result.State = UploadState.Success;
                }
                else
                {
                    Result.State = UploadState.FileAccessError;
                    Result.ErrorMessage = "上传失败";
                }
            }
            catch (Exception ex)
            {
                Result.State = UploadState.FileAccessError;
                Result.ErrorMessage = ex.Message;
            }
            finally
            {
                WriteResult();
            }

            /*
            byte[] uploadFileBytes = null;
            string uploadFileName = null;
            if (UploadConfig.Base64)
            {
                uploadFileName = UploadConfig.Base64Filename;
                uploadFileBytes = Convert.FromBase64String(Request[UploadConfig.UploadFieldName]);
            }
            else
            {
                var file = Request.Files[UploadConfig.UploadFieldName];
                uploadFileName = file.FileName;
                Result.SourceFileName = uploadFileName;
                Result.FileSize = string.Format("{0:F2}M", file.ContentLength / 1024M / 1024M);
                Result.LimitSize = string.Format("{0:F2}M", UploadConfig.SizeLimit / 1024M / 1024M);

                if (!CheckFileType(uploadFileName))
                {
                    Result.State = UploadState.TypeNotAllow;
                    WriteResult();
                    return;
                }
                if (!CheckFileSize(file.ContentLength))
                {
                    Result.State = UploadState.SizeLimitExceed;
                    WriteResult();
                    return;
                }

                uploadFileBytes = new byte[file.ContentLength];
                try
                {
                    file.InputStream.Read(uploadFileBytes, 0, file.ContentLength);
                }
                catch (Exception)
                {
                    Result.State = UploadState.NetworkError;
                    WriteResult();
                }
            }

            Result.OriginFileName = uploadFileName;

            var savePath = PathFormatter.Format(uploadFileName, UploadConfig.PathFormat);
            var localPath = Server.MapPath(savePath);

            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(localPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                }

                //二进制流写入文件
                File.WriteAllBytes(localPath, uploadFileBytes);
                if (UploadConfig.ActionName == "uploadimage")
                {
                    HttpHelper helper = new HttpHelper();
                    string str = helper.TakeUploadFileString(string.Format("http://{0}/upload/UploadImageByFileOne", GlobalConfig.ImageServer), localPath);
                    CSPUploadResult res = JsonHelper.Deserializer<CSPUploadResult>(str);
                    if(res.status == true)
                    {
                        if(File.Exists(localPath))
                        { 
                            File.Delete(localPath);
                        }
                        Result.Url = res.path;
                        Result.State = UploadState.Success;
                    }
                    else
                    {
                        Result.State = UploadState.FileAccessError;
                        Result.ErrorMessage = "上传失败";
                    }
                }
                else
                {
                    Result.Url = string.Format("http://{0}{1}", Request.Url.Host, savePath);
                    Result.State = UploadState.Success;
                }
            }
            catch (Exception e)
            {
                Result.State = UploadState.FileAccessError;
                Result.ErrorMessage = e.Message;
            }
            finally
            {
                WriteResult();
            }
            */
        }

        private void WriteResult()
        {
            this.WriteJson(new
            {
                state = GetStateMessage(Result.State),
                statecode = Result.State.ToString(),
                url = Result.Url,
                title = Result.OriginFileName,
                original = Result.OriginFileName,
                error = Result.ErrorMessage,
                limit = Result.LimitSize,
                filesize = Result.FileSize,
                sourcefilename = Result.SourceFileName
            });
        }

        private string GetStateMessage(UploadState state)
        {
            switch (state)
            {
                case UploadState.Success:
                    return "SUCCESS";
                case UploadState.FileAccessError:
                    return "文件访问出错，请检查写入权限";
                case UploadState.SizeLimitExceed:
                    return "文件大小超出服务器限制";
                case UploadState.TypeNotAllow:
                    return "不允许的文件格式";
                case UploadState.NetworkError:
                    return "网络错误";
            }
            return "未知错误";
        }

        private bool CheckFileType(string filename)
        {
            var fileExtension = Path.GetExtension(filename).ToLower();
            return UploadConfig.AllowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
        }

        private bool CheckFileSize(int size)
        {
            return size < UploadConfig.SizeLimit;
        }
    }

    public class UploadConfig
    {
        /// <summary>
        /// Action 名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 文件命名规则
        /// </summary>
        public string PathFormat { get; set; }

        /// <summary>
        /// 上传表单域名称
        /// </summary>
        public string UploadFieldName { get; set; }

        /// <summary>
        /// 上传大小限制
        /// </summary>
        public int SizeLimit { get; set; }

        /// <summary>
        /// 上传允许的文件格式
        /// </summary>
        public string[] AllowExtensions { get; set; }

        /// <summary>
        /// 文件是否以 Base64 的形式上传
        /// </summary>
        public bool Base64 { get; set; }

        /// <summary>
        /// Base64 字符串所表示的文件名
        /// </summary>
        public string Base64Filename { get; set; }
    }

    public class UploadResult
    {
        public UploadState State { get; set; }
        public string Url { get; set; }
        public string OriginFileName { get; set; }
        public string FileSize { get; set; }
        public string LimitSize { get; set; }
        public string SourceFileName { get; set; }
        public string ErrorMessage { get; set; }
    }

    public enum UploadState
    {
        Success = 0,
        SizeLimitExceed = -1,
        TypeNotAllow = -2,
        FileAccessError = -3,
        NetworkError = -4,
        Unknown = 1,
    }

}