using Ecommerce.Domain.Infrastructure;
using Ecommerce.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace  Ecommerce.Web.Models
{
    public class ConfigAPIModel
    {
        public bool ThemmoiConfig(ConfigAPI a)
        {
            using(EcommerceModel_DbContext db = new EcommerceModel_DbContext())
            {
                try
                {
                    if (kiemtratontai(a.MaNCC, db))
                    {
                        db.Entry(a).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.ConfigAPIs.Add(a);
                        db.SaveChanges();
                    }
                    return true;
                }
                catch (Exception e) { return false; }
            }
        }
        private bool kiemtratontai(string NCC, EcommerceModel_DbContext db)
        {
            var config = (from p in db.ConfigAPIs where p.MaNCC == NCC select new { p.MaNCC }).ToList();
            if (config.Count == 0)
                return false;
            return true;
        }
        public ConfigAPI getConfig(string MaNCC)
        {
            using(EcommerceModel_DbContext db = new EcommerceModel_DbContext())
            {
                var config = (from p in db.ConfigAPIs where p.MaNCC == MaNCC select p).FirstOrDefault();
                return config;
            }
        }
    }
}