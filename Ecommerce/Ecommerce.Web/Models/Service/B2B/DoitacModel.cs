using Ecommerce.Domain.Infrastructure;
using Ecommerce.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  Ecommerce.Web.Models
{
    public class DoitacModel
    {
        public List<NhaCungCap> LayDoitac()
        {
           using(EcommerceModel_DbContext db = new EcommerceModel_DbContext())
            {
                var ds = (from p in db.NhaCungCaps where p.Net_user == null select p).ToList();
                //var ds = (from p in db.NhaCungCaps select p).ToList();
                return ds;
            }
        }
    }
}