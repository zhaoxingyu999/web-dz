using FilmLove.Business;
using FilmLove.Business.Entity.Request;
using FilmLove.Database.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebManagers.Core;
using YJYSoft.YL.Common;

namespace FilmLove.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        ProductManager _productManager = new ProductManager();
        [MenuItemAttribute("基本管理", "产品管理")]
        public ActionResult ProductList()
        {
            return View();
        }
        [MenuItemAttribute("基本管理", "产品管理", "编辑")]
        public ActionResult ProductEdit(int? id)
        {

            var ent = _productManager.GetProductById(id);
            if (ent == null)
                ent = new Product();
            return View(ent);
        }
        [MenuItemAttribute("基本管理", "产品管理", "保存")]
        [ValidateInput(false)]
        public JsonResult ProductListSave(Product model)
        {
            var r = _productManager.ProductListSave(model);
            return Json(r);
        }
        [MenuItemAttribute("基本管理", "产品管理", "加载")]
        public JsonResult ProductListLoad(CarouselPhotoListReq req)
        {
            var r = _productManager.ProductListLoad(req);
            return Json(r, JsonRequestBehavior.AllowGet);
        }
    }
}