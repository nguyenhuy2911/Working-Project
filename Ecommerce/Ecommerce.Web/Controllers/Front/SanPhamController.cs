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

namespace Ecommerce.Web.Controllers
{
    public class SanPhamController : Controller
    {
        private EcommerceModel_DbContext db = new EcommerceModel_DbContext();
        private SanPhamModel _service = new SanPhamModel();
        // GET: SanPham
        [Trackingactionfilter]
        public ActionResult Index(string id)
        {
            SanPham sp = _service.FindById(id);
            return View("ProductDetail", sp);
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


    }
}