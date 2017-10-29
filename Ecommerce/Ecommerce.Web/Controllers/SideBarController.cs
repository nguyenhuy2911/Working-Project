using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Web.Models;
using Ecommerce.Domain.Model;

namespace  Ecommerce.Web.Controllers
{
    public class SideBarController : Controller
    {
        // GET: SlideBar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductFilter()
        {
            return PartialView("ProductFilter");
        }

        public ActionResult GiamGiaNhieu()
        {
            SanPhamModel sp = new SanPhamModel();
            IQueryable<SanPham> splist = sp.SPKhuyenMai();
            splist = splist.Take(5);
            return PartialView("_GiamGiaNhieuPartial", splist);
        }

        public ActionResult KhuyenMaiPost()
        {
            PromotionService km = new PromotionService();
            return PartialView("_KhuyenMaiPost",km.SearchPromotion(null, null, null).Where(m=> m.NgayBatDau <= DateTime.Today && m.NgayKetThuc >= DateTime.Today));
        }
    }
}