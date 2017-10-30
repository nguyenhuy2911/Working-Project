using Ecommerce.Domain.Model;
using Ecommerce.Web.Models;
using Ecommerce.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EC_TH2012_J.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index(string alias, string brandAlias)
        {
            var model = new CategoryViewModel();
            CategoryModel _categoryModel = new CategoryModel();
            LoaiSP category = _categoryModel.FindByAlias(alias);
            if (category != null)
            {
                model.Items = category.SanPhams.ToList();
                model.CategoryName = category.TenLoai;
            }
            
            if (!string.IsNullOrEmpty(brandAlias))
            {
                HangSanXuatModel brandModel = new HangSanXuatModel();
                var itemsByBrand = category.SanPhams.Where(p => p.HangSanXuat.Alias == brandAlias).ToList(); //brandModel.FindByAlias(brandAlias);
                if (itemsByBrand != null)
                {
                    model.Items = itemsByBrand;
                    model.CategoryName = string.Format("{0},{1}", category.TenLoai, itemsByBrand[0].HangSanXuat.TenHang);
                }
            }
            return View("~/Views/Front/Category/Index.cshtml", model);
        }
    }
}