using Ecommerce.Domain.Infrastructure;
using Ecommerce.Domain.Model;
using Ecommerce.Web.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Models
{
    public class SanPhamModel
    {
        EcommerceModel_DbContext db = new EcommerceModel_DbContext();
        public IQueryable<SanPham> SearchByName(string term)
        {
            IQueryable<SanPham> lst;
            lst = db.SanPhams.Where(u => u.TenSP.Contains(term));
            return lst;
        }

        public IQueryable<SanPham> AdvancedSearch(string term, string loai, string hangsx, int? minprice, int? maxprice)
        {
            IQueryable<SanPham> lst = db.SanPhams;
            if (!string.IsNullOrEmpty(term))
                lst = SearchByName(term);
            if (!string.IsNullOrEmpty(loai))
                lst = from p in lst where p.LoaiSP.Equals(loai) select p;
            if (!string.IsNullOrEmpty(hangsx))
                lst = from p in lst where p.HangSX.Equals(hangsx) select p;
            if (minprice != null)
                lst = from p in lst where p.GiaTien >= minprice select p;
            if (maxprice != null)
                lst = from p in lst where p.GiaTien <= maxprice select p;
            return lst;
        }
        public IQueryable<SanPham> SearchByType(string term)
        {
            var splist = (from p in db.SanPhams where p.LoaiSP.Equals(term) select p);
            return splist;
        }

        internal List<SanPham> NewestProduct(int skip)
        {
            var _listItems = db.SanPhams.OrderByDescending(p => p.Id)
                                    .ThenByDescending(p => p.CreateDate)
                                    .Take(12).Skip(skip).ToList();
            return _listItems;
        }

        internal IQueryable<SanPham> SPKhuyenMai()
        {
            var splist = from p in db.SanPhamKhuyenMais
                         orderby p.GiamGia descending
                         where DateTime.Today >= p.Promotion.NgayBatDau && DateTime.Today <= p.Promotion.NgayKetThuc
                         select p.SanPham;
            return splist;
        }

        internal IQueryable<SanPham> SPBanChay(int takenum)
        {
            var s = from p in db.ChiTietDonHangs
                    group p by p.MaSP into gro
                    select new { MaSP = gro.Key, sl = gro.Sum(r => r.SoLuong) };
            var splist = from p in db.SanPhams join ca in s on p.MaSP equals ca.MaSP orderby ca.sl descending select p;
            return splist.Take(takenum);
        }

        internal IQueryable<SanPham> GetAll()
        {
            return db.SanPhams;
        }

        internal SanPham FindById(string id)
        {
            return db.SanPhams.Where(p => p.MaSP == id)?.FirstOrDefault();
        }

        internal IQueryable<HangSanXuat> GetAllHangSX()
        {
            return db.HangSanXuats;
        }

        internal IQueryable<LoaiSP> GetAllLoaiSP()
        {
            return db.LoaiSPs;
        }

        internal void EditSP(SanPham product)
        {

            SanPham _productUpdate = this.FindById(product.MaSP);
            _productUpdate.TenSP = product.TenSP;
            _productUpdate.LoaiSP = product.LoaiSP;
            _productUpdate.HangSX = product.HangSX;
            _productUpdate.XuatXu = product.XuatXu;
            _productUpdate.GiaGoc = product.GiaGoc;
            _productUpdate.AnhDaiDien = string.Format("{0}-{1}-{2}", product.TenSP.GenerateFriendlyName(), product.MaSP, "1.jpg");
            _productUpdate.AnhNen = string.Format("{0}-{1}-{2}", product.TenSP.GenerateFriendlyName(), product.MaSP, "2.jpg");
            _productUpdate.AnhKhac = string.Format("{0}-{1}-{2}", product.TenSP.GenerateFriendlyName(), product.MaSP, "3.jpg");
            _productUpdate.GiaTien = tinhgiatien(_productUpdate.MaSP, _productUpdate.GiaGoc);
            _productUpdate.MoTa = WebUtility.HtmlEncode(product.MoTa);
            _productUpdate.SoLuong = product.SoLuong;
            _productUpdate.isnew = product.isnew;
            _productUpdate.ishot = product.ishot;
            product.ModifyDate = DateTime.Now;
            db.Entry(_productUpdate).State = EntityState.Modified;
            db.SaveChanges();
        }

        private decimal? tinhgiatien(string masp, decimal? giagoc)
        {
            IQueryable<SanPhamKhuyenMai> s = db.SanPhamKhuyenMais.Where(m => m.MaSP.Equals(masp)).OrderByDescending(m => m.GiamGia);
            if (s.Any())
            {
                return (giagoc * (100 - s.First().GiamGia) / 100);
            }
            return giagoc;
        }

        internal void DeleteProduct(string id)
        {
            SanPham product = db.SanPhams.Find(id);
            db.SanPhams.Remove(product);
            db.SaveChanges();
        }

        internal string AddProduct(SanPham product)
        {
            product.MaSP = TaoMa();
            product.MoTa = WebUtility.HtmlEncode(product.MoTa);
            product.GiaTien = product.GiaGoc;
            product.AnhDaiDien = string.Format("{0}-{1}-{2}", product.TenSP.GenerateFriendlyName(), product.MaSP, "1.jpg");
            product.AnhNen = string.Format("{0}-{1}-{2}", product.TenSP.GenerateFriendlyName(), product.MaSP, "2.jpg");
            product.AnhKhac = string.Format("{0}-{1}-{2}", product.TenSP.GenerateFriendlyName(), product.MaSP, "3.jpg");
            product.CreateDate = DateTime.Now;
            product.ModifyDate = DateTime.Now;
            db.SanPhams.Add(product);
            db.SaveChanges();
            return product.MaSP;
        }

        private string TaoMa()
        {
            string maID;
            Random rand = new Random();
            do
            {
                maID = "";
                for (int i = 0; i < 5; i++)
                {
                    maID += rand.Next(9);
                }
            }
            while (!KiemtraID(maID));
            return maID;
        }

        private bool KiemtraID(string maID)
        {
            using (EcommerceModel_DbContext db = new EcommerceModel_DbContext())
            {
                var temp = db.SanPhams.Where(p => p.MaSP == maID)?.FirstOrDefault();
                if (temp == null)
                    return true;
                return false;
            }
        }
        public SanPham getSanPham(string id)
        {
            var sp = (from p in db.SanPhams where (p.MaSP == id) select p).FirstOrDefault();
            return sp;
        }

        internal void ThemTSKT(ThongSoKyThuat item)
        {
            db.ThongSoKyThuats.Add(item);
            db.SaveChanges();
        }

        internal IQueryable<ThongSoKyThuat> GetTSKT(string masp)
        {
            return db.ThongSoKyThuats.Where(m => m.MaSP == masp);
        }

        internal void DelAllTSKT(string p)
        {
            db.ThongSoKyThuats.RemoveRange(db.ThongSoKyThuats.Where(m => m.MaSP == p));
            db.SaveChanges();
        }

        internal IQueryable<SanPham> SPHot()
        {
            return db.SanPhams.Where(s => s.ishot == true);
        }

        internal void UpdateGiaBan(string p)
        {
            var s = db.SanPhams.Find(p);
            s.GiaTien = tinhgiatien(p, s.GiaGoc);
            db.Entry(s).State = EntityState.Modified;
            db.SaveChanges();
        }

        internal void UpdateGiaBans(List<SanPham> lst)
        {
            using (var db = new EcommerceModel_DbContext())
            {
                lst.ForEach(m => m.GiaTien = tinhgiatien(m.MaSP, m.GiaGoc));
                db.SaveChanges();
            }
        }
        internal void UpdateSL(string masp, int? sl, bool? loaihd)
        {
            if (sl != null)
            {
                var s = db.SanPhams.Find(masp);
                if (loaihd == true)
                    s.SoLuong += sl;
                else if (loaihd == false)
                    s.SoLuong -= sl;
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}