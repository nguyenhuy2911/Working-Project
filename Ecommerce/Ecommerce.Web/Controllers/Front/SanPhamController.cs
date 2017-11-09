using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Web.Models;
using PagedList;
using PagedList.Mvc;
using Ecommerce.Web.App_Start;
using Ecommerce.Domain.Model;
using Ecommerce.Domain.Infrastructure;
using Ecommerce.Web.Models.ViewModel;
using Ecommerce.Web.common.Helper;
using Ecommerce.Web.common.Const;

namespace Ecommerce.Web.Controllers
{
    public class SanPhamController : Controller
    {
        private EcommerceModel_DbContext db = new EcommerceModel_DbContext();
        private SanPhamModel _service = new SanPhamModel();
        HangSanXuatModel _brandService = new HangSanXuatModel();
        // GET: SanPham
        [Trackingactionfilter]
        public ActionResult Index(string id)
        {
            SanPham product = _service.FindById(id);
            var breadCrumbModel = new Breadcrumb_ViewModel()
            {
                Title1 = product.LoaiSP1.TenLoai,
                Title1_Url = Url.RouteUrl("Category", new { alias = product.LoaiSP1.Alias }),
                Title2 = product.HangSanXuat.TenHang,
                Title2_Url = Url.RouteUrl("BrandCategory", new { productTypeAlias = product.LoaiSP1.Alias, brandAlias = product.HangSanXuat.Alias }),
                Title3 = product.TenSP,
                Title3_Url = "#"
            };
            TempData[Const.TempData_BreadCrumb] = breadCrumbModel;
            return View("ProductDetail", product);
        }
        public ActionResult SearchByName(string tensp)
        {
            var splist = db.SanPhams.Where(u => u.TenSP.Contains(tensp));
            ViewBag.CurrentFilter = tensp;
            splist = splist.OrderByDescending(u => u.TenSP);
            return View(splist);
        }

        public ActionResult SearchByType(string MaLoai)
        {
            var splist = (from p in db.SanPhams where p.LoaiSP.Equals(MaLoai) select p);
            return View(splist);
        }

        public ActionResult RelateProducs(string maloai, int sl)
        {
            IQueryable<SanPham> splist = _service.SearchByType(maloai);
            splist = splist.Take(sl);
            return PartialView("_RelateProducts", splist);
        }

        public ActionResult ItemsBestSeller(int? skip)
        {
            var _listItem = _service.SPBanChay(8);
            if (_listItem.Any())
                return PartialView("_ProductTabLoadMorePartial", _listItem);
            else
                return null;
        }

        public ActionResult ItemsPromotion(int? skip)
        {
            SanPhamModel sp = new SanPhamModel();
            int skipnum = (skip ?? 0);
            IQueryable<SanPham> splist = sp.SPKhuyenMai();
            splist = splist.OrderBy(r => r.MaSP).Skip(skipnum).Take(4);
            if (splist.Any())
                return PartialView("_ProductTabLoadMorePartial", splist);
            else
                return null;
        }

        public ActionResult ItemsNewest()
        {
            var listItems = _service.NewestProduct(0);
            if (listItems.Any())
                return PartialView("_NewestItems", listItems);
            else
                return null;
        }

        public ActionResult ThongSoKyThuat(string MaSP)
        {
            SanPhamModel spm = new SanPhamModel();
            return PartialView("ThongSoKyThuatPartial", spm.GetTSKT(MaSP));
        }

        public ActionResult ItemsRecentlyView()
        {
            var model = ManagerObiect.getIntance().Laydanhsachsanphammoixem();
            return PartialView("_RecentlyViewPartial", model);
        }

        public ActionResult ProductsByBrand()
        {
            var model = _brandService.GetShowHomeBrand().ToList();
            return PartialView("_ProductsByBrand", model);
        }
    }
}