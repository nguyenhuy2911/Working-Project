using Ecommerce.Domain.Model;
using Ecommerce.Web.common.Const;
using Ecommerce.Web.Models;
using Ecommerce.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
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
            var breadCrumbModel = new Breadcrumb_ViewModel()
            {
                Title1 = category.TenLoai,
                Title1_Url = "#"

            };
            if (!string.IsNullOrEmpty(brandAlias))
            {
                HangSanXuatModel brandModel = new HangSanXuatModel();
                var brand = brandModel.FindByAlias(brandAlias);
                var itemsByBrand = category.SanPhams.Where(p => p.HangSanXuat.Alias == brandAlias).ToList(); //brandModel.FindByAlias(brandAlias);
                if (itemsByBrand != null)
                {
                    model.Items = itemsByBrand;
                    model.CategoryName = itemsByBrand[0].HangSanXuat.TenHang;
                }
                breadCrumbModel.Title1_Url = Url.RouteUrl("Category", new { alias = category.Alias });
                breadCrumbModel.Title2 = brand.TenHang;
                breadCrumbModel.Title2_Url = "#";
            }
            TempData[Const.TempData_BreadCrumb] = breadCrumbModel;
            return View("~/Views/Front/Category/Index.cshtml", model);
        }

        public ActionResult BrandCategory(string productTypeAlias, string brandAlias)
        {
            var model = new CategoryViewModel();
            CategoryModel _categoryModel = new CategoryModel();
            LoaiSP category = _categoryModel.FindByAlias(productTypeAlias);
            if (category != null)
            {
                model.Items = category.SanPhams.ToList();
                model.CategoryName = category.TenLoai;
            }
            var breadCrumbModel = new Breadcrumb_ViewModel()
            {
                Title1 = category.TenLoai,
                Title1_Url = "#"

            };
            if (!string.IsNullOrEmpty(brandAlias))
            {
                HangSanXuatModel brandModel = new HangSanXuatModel();
                var brand = brandModel.FindByAlias(brandAlias);
                var itemsByBrand = category.SanPhams.Where(p => p.HangSanXuat.Alias == brandAlias).ToList(); //brandModel.FindByAlias(brandAlias);
                if (itemsByBrand != null)
                {
                    model.Items = itemsByBrand;
                    model.CategoryName = itemsByBrand[0].HangSanXuat.TenHang;
                }
                breadCrumbModel.Title1_Url = Url.RouteUrl("Category", new { alias = category.Alias });
                breadCrumbModel.Title2 = brand.TenHang;
                breadCrumbModel.Title2_Url = "#";
            }
            TempData[Const.TempData_BreadCrumb] = breadCrumbModel;
            return View("~/Views/Front/Category/Index.cshtml", model);
        }


    }
}