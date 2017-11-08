using Ecommerce.Domain.Model;
using Ecommerce.Web.Controllers;
using Ecommerce.Web.Helper;
using Ecommerce.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Areas.BackEnd.Controllers
{
    [AuthLog(Roles = "Quản trị viên, Nhân viên")]
    public class ProductController : Controller
    {
        SanPhamModel _service = new SanPhamModel();
        public ActionResult Index()
        {

            ViewBag.LoaiSP = new SelectList(_service.GetAllLoaiSP(), "MaLoai", "TenLoai");
            return View();
        }


        public ActionResult GetProduct(string key, string maloai, int? page)
        {

            ViewBag.key = key;
            ViewBag.maloai = maloai;
            var listProduct = _service.AdvancedSearch(key, maloai, null, null, null)
                                      .OrderByDescending(m => m.Id)
                                      .ThenByDescending(p => p.CreateDate)
                                      .ToPagedList(page ?? 1, 10);

            return PartialView("_ListProduct", listProduct);
        }

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
            List<SelectListItem> listBrand = spm.GetAllHangSX()?.ToList()?.Select(p =>
               new SelectListItem()
               {
                   Value = p.HangSX,
                   Text = p.TenHang,
                   Selected = p.HangSX.Equals(model.HangSX)
               }).ToList();
            List<SelectListItem> listProductType = spm.GetAllLoaiSP()?.ToList()?.Select(p =>
               new SelectListItem()
               {
                   Value = p.MaLoai,
                   Text = p.TenLoai,
                   Selected = p.MaLoai.Equals(model.LoaiSP)
               }).ToList();

            ViewBag.Brands = listBrand;
            ViewBag.ProductTypes = listProductType;

            return View("~/Areas/BackEnd/Views/Product/CrudProduct.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProduct(SanPham model, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            SanPhamModel spm = new SanPhamModel();

            if (string.IsNullOrEmpty(model.MaSP))
                return AddProduct(model, file1, file2, file3);
            else
                return EditProduct(model, file1, file2, file3);


        }

        private ActionResult AddProduct(SanPham sanpham, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            SanPhamModel spm = new SanPhamModel();
            string _productCode = spm.AddProduct(sanpham);
            UploaProductImg(file1, string.Format("{0}-{1}-{2}", sanpham.TenSP.GenerateFriendlyName(), _productCode, "1"));
            UploaProductImg(file2, string.Format("{0}-{1}-{2}", sanpham.TenSP.GenerateFriendlyName(), _productCode, "2"));
            UploaProductImg(file3, string.Format("{0}-{1}-{2}", sanpham.TenSP.GenerateFriendlyName(), _productCode, "3"));
            ThongSoKyThuat _techniqueInfo = new ThongSoKyThuat();
            _techniqueInfo.MaSP = _productCode;
            List<ThongSoKyThuat> productTechniques = new List<ThongSoKyThuat>();
            productTechniques.Add(_techniqueInfo);
            AddProductTechnique(productTechniques);

            return RedirectToAction("Index");
        }

        private ActionResult EditProduct(SanPham sanpham, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            SanPhamModel spm = new SanPhamModel();
            if (ModelState.IsValid)
            {
                spm.EditSP(sanpham);
                UploaProductImg(file1, string.Format("{0}-{1}-{2}", sanpham.TenSP.GenerateFriendlyName(), sanpham.MaSP, "1"));
                UploaProductImg(file2, string.Format("{0}-{1}-{2}", sanpham.TenSP.GenerateFriendlyName(), sanpham.MaSP, "2"));
                UploaProductImg(file3, string.Format("{0}-{1}-{2}", sanpham.TenSP.GenerateFriendlyName(), sanpham.MaSP, "3"));
                return RedirectToAction("Index");
            }
            return CrudProduct_View(sanpham.MaSP);
        }

        [HttpPost]
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
            return RedirectToAction("Index");
        }

        public bool UploaProductImg(HttpPostedFileBase file, string fileName)
        {
            if (file != null && file.ContentLength > 0)
            {
                var name = Path.GetExtension(file.FileName);
                if (!Path.GetExtension(file.FileName).Equals(".jpg") && !Path.GetExtension(file.FileName).Equals(".png"))
                {
                    return false;
                }
                var path = Path.Combine(Server.MapPath("~/uploads/products"), fileName + ".jpg");
                file.SaveAs(path);
                return true;
            }
            return false;
        }

        public bool DeleteProductImg(string filename)
        {
            string fullPath = Request.MapPath("~/uploads/products/" + filename);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            return false;
        }

        [HttpPost]
        public ActionResult AddProductTechnique(List<ThongSoKyThuat> productTechniques)
        {
            if (productTechniques.Count == 0)
            {
                return RedirectToAction("Index");
            }
            SanPhamModel spm = new SanPhamModel();
            foreach (var item in productTechniques)
            {
                if (!string.IsNullOrEmpty(item.ThuocTinh))
                    spm.ThemTSKT(item);
            }
            return RedirectToAction("Index");
        }

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