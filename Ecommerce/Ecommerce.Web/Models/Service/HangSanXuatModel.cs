using Ecommerce.Domain.Infrastructure;
using Ecommerce.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace  Ecommerce.Web.Models
{
    public class HangSanXuatModel
    {
        private EcommerceModel_DbContext db = new EcommerceModel_DbContext();
        public IQueryable<HangSanXuat> GetHangSX()
        {
            IQueryable<HangSanXuat> lst = db.HangSanXuats;
            return lst;
        }

        public IQueryable<HangSanXuat> GetShowHomeBrand()
        {
            IQueryable<HangSanXuat> lst = db.HangSanXuats
                                            .Where(p => p.IsHomePage == true);
            return lst;
        }

        internal HangSanXuat FindById(string id)
        {
            return db.HangSanXuats.Find(id);
        }

        internal HangSanXuat FindByAlias(string alias)
        {
            return db.HangSanXuats.Where(p=> p.Alias == alias).FirstOrDefault();
        }

        internal void EditHangSX(HangSanXuat loai)
        {
            HangSanXuat _updateItem = db.HangSanXuats.Find(loai.HangSX);
            _updateItem.TenHang = loai.TenHang;
            _updateItem.Alias = loai.TenHang.GenerateFriendlyName();
            _updateItem.TruSoChinh = loai.TruSoChinh;
            _updateItem.QuocGia = loai.QuocGia;
            _updateItem.IsHomePage = loai.IsHomePage;
            db.Entry(_updateItem).State = EntityState.Modified;
            db.SaveChanges();
        }

        internal void DeleteHangSX(string id)
        {
            HangSanXuat loai = db.HangSanXuats.Find(id);
            db.HangSanXuats.Remove(loai);
            db.SaveChanges();
        }

        internal bool KiemTraTen(string p)
        {
            var temp = db.HangSanXuats.Where(m => m.TenHang.Equals(p)).ToList();
            if (temp.Count == 0)
                return true;
            return false;
        }

        internal string ThemHangSX(HangSanXuat loai)
        {
            loai.HangSX = TaoMa();
            loai.Alias = loai.TenHang.GenerateFriendlyName();
            db.HangSanXuats.Add(loai);
            db.SaveChanges();
            return loai.HangSX;
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
            var temp = db.HangSanXuats.Find(maID);
            if (temp == null)
                return true;
            return false;
        }

        internal IQueryable<HangSanXuat> SearchByName(string key)
        {
            if (string.IsNullOrEmpty(key))
                return db.HangSanXuats;
            return db.HangSanXuats.Where(u => u.TenHang.Contains(key));
        }
    }
}