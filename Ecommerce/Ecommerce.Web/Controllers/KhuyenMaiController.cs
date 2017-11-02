using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Ecommerce.Web.Models;
using System.Net;
using System.IO;
using Ecommerce.Domain.Model;

namespace  Ecommerce.Web.Controllers
{
    [AuthLog(Roles = "Quản trị viên,Nhân viên")]
    public class KhuyenMaiController : Controller
    {
        public ActionResult DeleteSPKhuyenMai(string makm, string masp)
        {
            PromotionService spm = new PromotionService();
            spm.Delete_Product_Promotion(makm, masp);
            
            return RedirectToAction("DSSanPham", new { makm = makm });
        }
        
        public ActionResult kiemtra(string key)
        {
            PromotionService spm = new PromotionService();
            if (spm.KiemTraTen(key))
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CTKhuyenMai(string id)
        {
            PromotionService km = new PromotionService();
            var lst = km.Planning_Promotion(id);
            if (lst.Any())
                return PartialView("KhuyenMaiDetail", lst);
            return null;
        }

        [HttpPost]
        public ActionResult ThemSPKhuyenMai(List<SanPhamKhuyenMai> lstkt)
        {
            if (lstkt.Count == 0)
            {
                return RedirectToAction("Index");
            }
            PromotionService spm = new PromotionService();
            foreach (var item in lstkt)
            {
                if (!string.IsNullOrEmpty(item.MaSP) && !string.IsNullOrEmpty(item.MaKM))
                    spm.AddPromotion(item);
            }
            return RedirectToAction("Index");
        }

        [HttpPost] 
        public ActionResult ThemSP1KhuyenMai([Bind(Include = "MaKM,MaSP,MoTa,GiamGia")] SanPhamKhuyenMai spkm)
        {
            PromotionService spm = new PromotionService();
            spm.AddPromotion(spkm);
            return RedirectToAction("DSSanPham", new { makm = spkm.MaKM });
        }

        [HttpPost]
        public ActionResult SuaCTKhuyenMai(List<SanPhamKhuyenMai> lstkt)
        {
            if (lstkt.Count == 0)
            {
                return RedirectToAction("Index");
            }
            PromotionService spm = new PromotionService();
            spm.DelAllSPKM(lstkt[0].MaKM);
            foreach (var item in lstkt)
            {
                if (!string.IsNullOrEmpty(item.MaSP) && !string.IsNullOrEmpty(item.MaKM))
                    spm.AddPromotion(item);
            }
            return RedirectToAction("Index");
        }

        public ActionResult SuaCTKhuyenMai(string MaKM)
        {
            SanPhamModel sp = new SanPhamModel();
            ViewBag.LoaiSP = new SelectList(sp.GetAllLoaiSP(), "MaLoai", "TenLoai");
            ViewBag.makm = MaKM;
            return View("SuaSPKhuyenMai");
        }


        public ActionResult DSSanPham(string key, string maloai, string makm, int? page)
        {
            ViewBag.key = key;
            ViewBag.maloai = maloai;
            ViewBag.makm = makm;
            PromotionService km = new PromotionService();
            IQueryable<SanPham> lst = km.DSSP(key, maloai, makm);
            if (lst.Any())
                return PhanTrangSP(lst, "DSSanPham", page, null);
            return null;
        }

        public ActionResult DSSanPhamKhuyenMai(string key, string maloai, string makm, int? page)
        {
            ViewBag.key = key;
            ViewBag.maloai = maloai;
            ViewBag.makm = makm;
            PromotionService km = new PromotionService();
            IQueryable<SanPham> lst = km.DSSanPhamKhuyenMai(key, maloai, makm);
            if (lst.Any())
                return PhanTrangSP(lst, "DSSanPhamKhuyenMai", page, null);
            return null;
        }


        public ActionResult PhanTrangSP(IQueryable<SanPham> lst, string stringview, int? page, int? pagesize)
        {
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return PartialView(stringview, lst.OrderBy(m => m.MaSP).ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        public ActionResult KhuyenMaiPost(string id)
        {
            PromotionService km = new PromotionService();
            return View("KhuyenMaiPostView", km.FindById(id));
        }

    }
}