using Ecommerce.Domain.Model;
using Ecommerce.Web.Controllers;
using Ecommerce.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EC_TH2012_J.Areas.BackEnd.Controllers
{
    public class ProductController : Controller
    {
        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult Product()
        {
            SanPhamModel spm = new SanPhamModel();
            ViewBag.LoaiSP = new SelectList(spm.GetAllLoaiSP(), "MaLoai", "TenLoai");
            return View();
        }

        //[AuthLog(Roles = "Quản trị viên,Nhân viên")]
        //public ActionResult GetProduct(string key, string maloai, int? page)
        //{
        //    SanPhamModel spm = new SanPhamModel();
        //    ViewBag.key = key;
        //    ViewBag.maloai = maloai;
        //    return ProductsPagination(spm.AdvancedSearch(key, maloai, null, null, null), page, null);
        //}

        //[AuthLog(Roles = "Quản trị viên,Nhân viên")]
        //public ActionResult ProductsPagination(IQueryable<SanPham> lst, int? page, int? pagesize)
        //{
        //    int pageSize = (pagesize ?? 10);
        //    int pageNumber = (page ?? 1);
        //    return PartialView("SanPhamPartial", lst.OrderBy(m => m.MaSP).ToPagedList(pageNumber, pageSize));
        //}

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult CrudProduct_View(string id)
        {
            SanPhamModel spm = new SanPhamModel();
            var model = new SanPham();
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Thêm mới";
            }
            else
            {
                ViewBag.Title = "Cập nhật sản phẩm";
                model = spm.FindById(id);
                if (model == null)
                    model = new SanPham();

            }
            ViewBag.HangSX = new SelectList(spm.GetAllHangSX(), "HangSX", "TenHang", model.HangSX);
            ViewBag.LoaiSP = new SelectList(spm.GetAllLoaiSP(), "MaLoai", "TenLoai", model.LoaiSP);

            return View("~/Areas/BackEnd/Views/Product/CrudProduct.cshtml", model);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProduct(SanPham model, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            SanPhamModel spm = new SanPhamModel();
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.MaSP))
                    return AddProduct(model, file1, file2, file3);
                else
                    return EditProduct(model, file1, file2, file3);
            }
            return RedirectToAction("SanPham", "Admin", new { Area = "" });
        }

        private ActionResult AddProduct(SanPham sanpham, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            SanPhamModel spm = new SanPhamModel();
            if (ModelState.IsValid)
            {
                string _productCode = spm.AddProduct(sanpham);
                UploaProductImg(file1, _productCode + "1");
                UploaProductImg(file2, _productCode + "2");
                UploaProductImg(file3, _productCode + "3");
                ThongSoKyThuat _techniqueInfo = new ThongSoKyThuat();
                _techniqueInfo.MaSP = _productCode;
                List<ThongSoKyThuat> productTechniques = new List<ThongSoKyThuat>();
                productTechniques.Add(_techniqueInfo);
                return AddProductTechnique(productTechniques);
            }
            return RedirectToAction("SanPham", "Admin", new { Area = "" });
        }

        private ActionResult EditProduct(SanPham sanpham, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            SanPhamModel spm = new SanPhamModel();
            if (ModelState.IsValid)
            {
                spm.EditSP(sanpham);
                UploaProductImg(file1, sanpham.MaSP + "1");
                UploaProductImg(file2, sanpham.MaSP + "2");
                UploaProductImg(file3, sanpham.MaSP + "3");
                return RedirectToAction("SanPham", "Admin", new { Area = "" });
            }
            return CrudProduct_View(sanpham.MaSP);
        }

        [HttpPost]
        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult DeleteProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPhamModel model = new SanPhamModel();
            DeleteProductImg(model.FindById(id).AnhDaiDien);
            DeleteProductImg(model.FindById(id).AnhNen);
            DeleteProductImg(model.FindById(id).AnhKhac);
            model.DeleteProduct(id);
            return RedirectToAction("SanPham", "Admin", new { Area = "" });
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public bool UploaProductImg(HttpPostedFileBase file, string fileName)
        {
            if (file != null && file.ContentLength > 0)
            {
                var name = Path.GetExtension(file.FileName);
                if (!Path.GetExtension(file.FileName).Equals(".jpg"))
                {
                    return false;
                }
                var path = Path.Combine(Server.MapPath("~/images/products"), fileName + ".jpg");
                file.SaveAs(path);
                return true;
            }
            return false;
        }

        [AuthLog(Roles = "Quản trị viên")]
        public bool DeleteProductImg(string filename)
        {
            string fullPath = Request.MapPath("~/images/products/" + filename);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            return false;
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        [HttpPost]
        public ActionResult AddProductTechnique(List<ThongSoKyThuat> productTechniques)
        {
            if (productTechniques.Count == 0)
            {
                return RedirectToAction("SanPham", "Admin", new { Area = "" });
            }
            SanPhamModel spm = new SanPhamModel();
            foreach (var item in productTechniques)
            {
                if (!string.IsNullOrEmpty(item.ThuocTinh))
                    spm.ThemTSKT(item);
            }
            return RedirectToAction("SanPham", "Admin", new { Area = "" });
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult EditProductTechnique(string masp)
        {
            SanPhamModel spm = new SanPhamModel();
            if (spm.GetTSKT(masp).ToList().Count == 0)
            {
                ThongSoKyThuat ts = new ThongSoKyThuat();
                ts.MaSP = masp;
                List<ThongSoKyThuat> lst = new List<ThongSoKyThuat>();
                lst.Add(ts);
                return View("ThemThongSoKT", lst);
            }
            return View("SuaThongSoKT", spm.GetTSKT(masp).ToList());
        }


    }
}