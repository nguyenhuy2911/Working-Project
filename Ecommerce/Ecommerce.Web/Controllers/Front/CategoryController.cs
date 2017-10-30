using Ecommerce.Domain.Model;
using Ecommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EC_TH2012_J.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index(string alias, string branch)
        {
            CategoryModel _categoryModel = new CategoryModel();
            List<SanPham> products = _categoryModel.GetProducts(alias, branch).ToList();
            var model = products;
            return View("~/Views/Front/Category/Index.cshtml", model);
        }
    }
}