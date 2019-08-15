using EditorUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmLove.Admin.Controllers
{
    public class ToolController : BaseController
    {
        // GET: Tool
        public ActionResult UploadProductImage()
        {
            return UploadFile("Product");
        }

        // GET: Tool
        public ActionResult UploadFile(string fileType)
        {
            var r = FileUploadHelper.UploadFileOne(fileType);
            return Json(r);
        }

        #region 编辑器帮助

        public ContentResult EditorHelp()
        {
            Handler handler = null;
            HttpContext context = System.Web.HttpContext.Current;
            string actionName = Request.QueryString["action"];
            switch (actionName)
            {
                case "config":
                    handler = new ConfigHandler(context);
                    break;
                case "uploadimage":
                    handler = new UploadHandler(context, new UploadConfig()
                    {
                        ActionName = actionName,
                        AllowExtensions = Config.GetStringList("imageAllowFiles"),
                        PathFormat = Config.GetString("imagePathFormat"),
                        SizeLimit = Config.GetInt("imageMaxSize"),
                        UploadFieldName = Config.GetString("imageFieldName")
                    });
                    break;
                case "buiuploadimage":
                    handler = new UploadHandler(context, new UploadConfig()
                    {
                        ActionName = actionName,
                        AllowExtensions = Config.GetStringList("imageAllowFiles"),
                        PathFormat = Config.GetString("imagePathFormat"),
                        SizeLimit = Config.GetInt("imageMaxSize"),
                        UploadFieldName = "Filedata"
                    });
                    break;
                case "uploadscrawl":
                    handler = new UploadHandler(context, new UploadConfig()
                    {
                        ActionName = actionName,
                        AllowExtensions = new string[] { ".png" },
                        PathFormat = Config.GetString("scrawlPathFormat"),
                        SizeLimit = Config.GetInt("scrawlMaxSize"),
                        UploadFieldName = Config.GetString("scrawlFieldName"),
                        Base64 = true,
                        Base64Filename = "scrawl.png"
                    });
                    break;
                case "uploadvideo":
                    handler = new UploadHandler(context, new UploadConfig()
                    {
                        ActionName = actionName,
                        AllowExtensions = Config.GetStringList("videoAllowFiles"),
                        PathFormat = Config.GetString("videoPathFormat"),
                        SizeLimit = Config.GetInt("videoMaxSize"),
                        UploadFieldName = Config.GetString("videoFieldName")
                    });
                    break;
                case "uploadfile":
                    handler = new UploadHandler(context, new UploadConfig()
                    {
                        ActionName = actionName,
                        AllowExtensions = Config.GetStringList("fileAllowFiles"),
                        PathFormat = Config.GetString("filePathFormat"),
                        SizeLimit = Config.GetInt("fileMaxSize"),
                        UploadFieldName = Config.GetString("fileFieldName")
                    });
                    break;
                case "listimage":
                    handler = new ListFileManager(context, Config.GetString("imageManagerListPath"), Config.GetStringList("imageManagerAllowFiles"));
                    break;
                case "listfile":
                    handler = new ListFileManager(context, Config.GetString("fileManagerListPath"), Config.GetStringList("fileManagerAllowFiles"));
                    break;
                case "catchimage":
                    handler = new CrawlerHandler(context);
                    break;
                default:
                    handler = new NotSupportedHandler(context);
                    break;
            }
            handler.Process();
            return Content("");
        }
        #endregion
    }
}