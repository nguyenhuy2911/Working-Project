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
        //
        // GET: /KhuyenMai/
        public ActionResult Index()
        {
            return View();
        }

        public bool UploadAnh(HttpPostedFileBase file, string tenfile)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                var name = Path.GetExtension(file.FileName);
                // extract only the filename
                if (!Path.GetExtension(file.FileName).Equals(".jpg"))
                {
                    return false;
                }
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/images/khuyenmai"), tenfile + ".jpg");
                file.SaveAs(path);
                return true;
            }
            // redirect back to the index action to show the form once again
            return false;
        }

        public bool DeleteAnh(string filename)
        {
            string fullPath = Request.MapPath("~/images/khuyenmai/" + filename);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            return false;
        }

        public ActionResult EditKhuyenMai(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionService lm = new PromotionService();
            Promotion sp = lm.FindById(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditKhuyenMai([Bind(Include = "MaKM,TenCT,NgayBatDau,NgayKetThuc,NoiDung")] Promotion loai, HttpPostedFileBase ad)
        {
            PromotionService spm = new PromotionService();
            if (ModelState.IsValid)
            {
                spm.EditPromotion(loai);
                UploadAnh(ad, loai.MaKM + "1");
                return RedirectToAction("Index");
            }
            return View(loai);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemKhuyenMai([Bind(Include = "TenCT,NgayBatDau,NgayKetThuc,NoiDung")] Promotion loai, HttpPostedFileBase ad)
        {
            PromotionService spm = new PromotionService();
            if (ModelState.IsValid && spm.KiemTraTen(loai.TenCT))
            {
                string makm = spm.AddPromotion(loai);
                UploadAnh(ad, makm + "1");
                return RedirectToAction("SuaCTKhuyenMai", new { MaKM = makm });
            }
            return View("Index", loai);
        }

        public ActionResult DeleteKhuyenMai(string id)
        {
            PromotionService spm = new PromotionService();
            DeleteAnh(spm.FindById(id).AnhCT);
            spm.DeletePromotion(id);    
            return TimKhuyenMai(null, null, null, null);
        }

        [HttpPost]
        public ActionResult MultibleDel(List<string> lstdel)
        {
            if (lstdel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var item in lstdel)
            {
                PromotionService spm = new PromotionService();
                DeleteAnh(spm.FindById(item).AnhCT);
                spm.DeletePromotion(item);
            }
            return TimKhuyenMai(null, null, null, null);
        }

        public ActionResult DeleteSPKhuyenMai(string makm, string masp)
        {
            PromotionService spm = new PromotionService();
            spm.Delete_Product_Promotion(makm, masp);
            
            return RedirectToAction("DSSanPham", new { makm = makm });
        }

        public ActionResult TimKhuyenMai(string key, DateTime? start, DateTime? end, int? page)
        {
            PromotionService spm = new PromotionService();
            ViewBag.key = key;
            ViewBag.start = start;
            ViewBag.end = end;
            return PhanTrangKhuyenMai(spm.SearchPromotion(key, start, end), page, null);
        }

        public ActionResult PhanTrangKhuyenMai(IQueryable<Promotion> lst, int? page, int? pagesize)
        {
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return PartialView("KhuyenMaiPartial", lst.OrderBy(m => m.NgayBatDau).ToPagedList(pageNumber, pageSize));
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