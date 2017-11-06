using Ecommerce.Domain.Model;
using Ecommerce.Web.Controllers;
using Ecommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Areas.BackEnd.Controllers
{
    [AuthLog(Roles = "Quản trị viên,Nhân viên")]
    public class ProductTypeController : Controller
    {
        // GET: BackEnd/ProductType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProductType(string key)
        {
            CategoryModel _service = new CategoryModel();
            var listProductType = _service.SearchByName(key).ToList();
            ViewBag.key = key;
            return View("~/Areas/BackEnd/Views/ProductType/_ListProductType.cshtml", listProductType);
        }

        public ActionResult CrudProductType_View(string id)
        {
            var model = new LoaiSP();

            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Thêm mới";
            }
            else
            {
                ViewBag.Title = "Cập nhật";
                CategoryModel lm = new CategoryModel();
                model = lm.FindById(id);
                if (model == null)
                    model = new LoaiSP();

            }
            return View("~/Areas/BackEnd/Views/ProductType/CrudProductType_View.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProductType(LoaiSP model)
        {
            bool isSuccess = false;
            SanPhamModel spm = new SanPhamModel();
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.MaLoai))
                    isSuccess = AddProductType(model);
                else
                    isSuccess = EditProductType(model);
            }
            if (isSuccess)
                return RedirectToAction("Index");
            else
                return View("~/Areas/BackEnd/Views/ProductType/CrudProductType_View.cshtml", model);
        }


        private bool AddProductType(LoaiSP model)
        {
            bool checkSuccess = false;
            CategoryModel spm = new CategoryModel();
            if (ModelState.IsValid && spm.KiemTraTen(model.TenLoai))
            {
                string maloai = spm.ThemLoaiSP(model);
                checkSuccess = true;

            }
            return checkSuccess;
        }


        private bool EditProductType(LoaiSP model)
        {
            bool checkSuccess = false;
            CategoryModel spm = new CategoryModel();
            if (ModelState.IsValid)
            {
                spm.EditLoaiSP(model);
                checkSuccess = true;
            }
            return checkSuccess;
        }

        [HttpPost]
        public bool DeleteProductType(string id)
        {
            CategoryModel spm = new CategoryModel();
            if (spm.FindById(id) == null)
            {
                return false;
            }
            spm.DeleteLoaiSP(id);
            return true;
        }
    }
}