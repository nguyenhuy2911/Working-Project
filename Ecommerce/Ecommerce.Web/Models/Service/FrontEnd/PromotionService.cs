using Ecommerce.Domain.Infrastructure;
using Ecommerce.Domain.Model;
using Ecommerce.Web.Models.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using Ecommerce.Web.common.Helper;
namespace  Ecommerce.Web.Models
{
    public class PromotionService : BaseService
    {
       

        internal Promotion FindById(string id)
        {
            return dataContext.Promotions.Find(id);
        }

        internal void EditPromotion(Promotion promotion)
        {
            Promotion _editPromotion = dataContext.Promotions.Find(promotion.MaKM);
            _editPromotion.TenCT = promotion.TenCT;
            _editPromotion.NgayBatDau = promotion.NgayBatDau;
            _editPromotion.AnhCT = promotion.TenCT.GenerateFriendlyName() + ".jpg";
            _editPromotion.NgayKetThuc = promotion.NgayKetThuc;
            _editPromotion.NoiDung = WebUtility.HtmlEncode(promotion.NoiDung);
            _editPromotion.Advertise = WebUtility.HtmlEncode(promotion.Advertise);
            dataContext.Entry(_editPromotion).State = EntityState.Modified;
            dataContext.SaveChanges();
        }

        internal void DeletePromotion(string id)
        {
            var lst = dataContext.SanPhamKhuyenMais.Where(m => m.MaKM.Equals(id)).ToList();
            foreach(var item in lst)
            {
                Delete_Product_Promotion(item.MaKM, item.MaSP);
            }
            Promotion loai = dataContext.Promotions.Find(id);
            dataContext.Promotions.Remove(loai);
            dataContext.SaveChanges();
        }

        internal string AddPromotion(Promotion promotion)
        {
            promotion.MaKM = TaoMa();
            promotion.AnhCT = promotion.TenCT.GenerateFriendlyName() + ".jpg";
            promotion.NoiDung = WebUtility.HtmlEncode(promotion.NoiDung);
            promotion.Advertise = WebUtility.HtmlEncode(promotion.Advertise);
            dataContext.Promotions.Add(promotion);
            dataContext.SaveChanges();
            return promotion.MaKM;
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
            var temp = dataContext.Promotions.Find(maID);
            if (temp == null)
                return true;
            return false;
        }

        internal IQueryable<Promotion> SearchPromotion(string key,DateTime? start, DateTime? end)
        {
            IQueryable<Promotion> lst = dataContext.Promotions;
            if (!string.IsNullOrEmpty(key))
                lst = dataContext.Promotions.Where(u => u.TenCT.Contains(key));
            if (start != null)
                lst = dataContext.Promotions.Where(u => u.NgayBatDau >= start);
            if (end != null)
                lst = dataContext.Promotions.Where(u => u.NgayKetThuc <= end);
            return lst;
        }

        internal bool KiemTraTen(string key)
        {
            var temp = dataContext.Promotions.Where(m => m.TenCT.Equals(key)).ToList();
            if (temp.Count == 0)
                return true;
            return false;
        }

        internal IQueryable<SanPhamKhuyenMai> Planning_Promotion(string key)
        {
            return dataContext.SanPhamKhuyenMais.Where(m => m.MaKM.Equals(key));
        }

        internal void AddPromotion(SanPhamKhuyenMai item)
        {
            dataContext.SanPhamKhuyenMais.Add(item);
            dataContext.SaveChanges();
            SanPhamModel s = new SanPhamModel();
            s.UpdateGiaBan(item.MaSP);
        }

        internal void DelAllSPKM(string p)
        {
            dataContext.SanPhamKhuyenMais.RemoveRange(dataContext.SanPhamKhuyenMais.Where(m => m.MaKM == p));
            dataContext.SaveChanges();
        }

        internal IQueryable<SanPhamKhuyenMai> GetSPKM(string MaKM)
        {
            return dataContext.SanPhamKhuyenMais.Where(m => m.MaKM == MaKM);
        }

        internal IQueryable<SanPham> DSSP(string key, string maloai, string makm)
        {
            var lst = dataContext.SanPhamKhuyenMais.Where(m => m.MaKM == makm).Select(m => m.MaSP);
            var lst1 = dataContext.SanPhams.Where(m => !lst.Contains(m.MaSP));
            if (!string.IsNullOrEmpty(key))
                lst1 = lst1.Where(m => m.TenSP.Contains(key));
            if (!string.IsNullOrEmpty(maloai))
                lst1 = lst1.Where(m => m.LoaiSP.Equals(maloai));
            return lst1;
        }

        internal IQueryable<SanPham> DSSanPhamKhuyenMai(string key, string maloai, string makm)
        {
            var lst = dataContext.SanPhamKhuyenMais.Where(m => m.MaKM == makm).Select(m => m.SanPham);
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.TenSP.Contains(key));
            if (!string.IsNullOrEmpty(maloai))
                lst = lst.Where(m => m.LoaiSP.Equals(maloai));
            return lst;
        }

        internal void Delete_Product_Promotion(string makm, string masp)
        {
            SanPhamKhuyenMai sp = dataContext.SanPhamKhuyenMais.Where(m=>m.MaSP == masp && m.MaKM == makm).FirstOrDefault();
            dataContext.SanPhamKhuyenMais.Remove(sp);
            dataContext.SaveChanges();
            SanPhamModel s = new SanPhamModel();
            s.UpdateGiaBan(masp);
            
        }
    }
}