using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YJYSoft.YL.Common;
using System.Web;
using System.IO;
using System.Drawing.Imaging;
using SingleGo.Database;
using System.Drawing;
using FilmLove.Database;
using SingleGo.OutAPI;

namespace FilmLove.Admin
{
    public class FileUploadR
    {
        public string fpath { get; set; }
        public string urlPath { get; set; }
        public string absPath { get; set; }
    }
    public class FileUploadHelper
    {
        public static AjaxResult UploadFileOne(string FileTypeName)
        {
            if (HttpContext.Current == null)
                return new AjaxResult("无效请求");
            if (HttpContext.Current.Request.Files == null)
                return new AjaxResult("无效请求");
            if (HttpContext.Current.Request.Files.Count == 0)
                return new AjaxResult("无效请求");
            HttpPostedFile file1 = HttpContext.Current.Request.Files[0];
            if (file1 == null)
                return new AjaxResult("上传文件无效");
            if (file1.ContentLength == 0)
                return new AjaxResult("上传文件长度无效");
            var stream = file1.InputStream;
            string ext = Path.GetExtension(file1.FileName);
            List<string> typeList = new List<string>() { "image/gif", "image/jpeg", "image/png" };
            bool isImg = false;
            ImageFormat ifmat = ImageFormat.Png;
            if (file1.ContentType.Equals("image/gif"))
            {
                ext = ".gif";
                isImg = true;
                ifmat = ImageFormat.Gif;
            }
            else if (file1.ContentType.Equals("image/jpeg"))
            {
                ext = ".jpg";
                isImg = true;
                ifmat = ImageFormat.Jpeg;
            }
            else if (file1.ContentType.Equals("image/png"))
            {
                ext = ".png";
                isImg = true;
                ifmat = ImageFormat.Png;
            }
            try
            {
                FileTypeName = FileTypeName.Trim('/', '\\');
                string dirPath = string.Format("/dualpsy/Upload/{0}/{1:yyyyMM}/", FileTypeName, DateTime.Now);
                dirPath = dirPath.Replace("//","/");
                string fileCode = string.Format("{0:yyyyMMddHHmmssfff}{1}", DateTime.Now, YJYSoft.YL.Common.Common.GetRandomCode(3));
                string fileName = fileCode + ext;
                string URLPath = Configure.FileDomain;
                string ABSPath = dirPath + fileName;
                //if (HttpContext.Current.Request.Url.Port != 80)
                //    URLPath += ":" + HttpContext.Current.Request.Url.Port.ToString();
                if (URLPath[URLPath.Length - 1] == '/')
                    URLPath = URLPath.Substring(0, URLPath.Length - 1);
                URLPath += ABSPath;
                string savePath = Configure.FileSavePath + dirPath;// HttpContext.Current.Server.MapPath(dirPath);
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);
                string fullPath = savePath + fileName;
                if (isImg)
                {
                    Image img = Image.FromStream(stream);
                    //img = ImageHelper.MakeThumbnail(img, 600, 600, "W");
                    img.Save(fullPath, ifmat);
                    img.Dispose();
                }
                else
                {
                    file1.SaveAs(fullPath);
                }
#if FB
                var v = new QCloudCOS("1258572948", "ap-chengdu", "AKIDKi1vlbNKDvSqOtPONBsPkfdOyNdhGWxS", "Vq14wcIe1wt3HNK1ksCMj4LVBlEcgls3", "singlego-1258572948");
                if(v.Upload(fullPath, ABSPath)!=0)
                {
                    URLPath= "http://ahalfi.dualpsy.com/dualpsy" + ABSPath;
                 
                }

#endif
                

                var data = new FileUploadR
                {
                    urlPath = URLPath,
                    absPath = ABSPath,
                };
                return new AjaxResult() { data = data };
            }
            catch (Exception ex)
            {
                return new AjaxResult(ex.ToString());
            }
        }
    }
}