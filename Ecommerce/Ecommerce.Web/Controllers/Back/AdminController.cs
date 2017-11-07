using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Web.Models;
using System.Net;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.IO;
using Ecommerce.Domain.Model;

namespace  Ecommerce.Web.Controllers
{
    public class AdminController : Controller
    {
       

        
        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult SPDetail(string id)
        {
            SanPhamModel sp = new SanPhamModel();
            return PartialView("SPDetail", sp.FindById(id));
        }
        

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        [HttpPost]
        public ActionResult ThemThongSoKT(List<ThongSoKyThuat> lstkt) 
        {
            if(lstkt.Count == 0)
            {
                return RedirectToAction("SanPham");
            }
            SanPhamModel spm = new SanPhamModel();
            foreach (var item in lstkt)
            {
                if (!string.IsNullOrEmpty(item.ThuocTinh))
                    spm.ThemTSKT(item);
            }
            return RedirectToAction("SanPham"); 
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        [HttpPost]
        public ActionResult SuaThongSoKT(List<ThongSoKyThuat> lstkt)
        {
            if (lstkt.Count == 0)
            {
                return RedirectToAction("SanPham");
            }
            SanPhamModel spm = new SanPhamModel();
            spm.DelAllTSKT(lstkt[0].MaSP);
            foreach (var item in lstkt)
            {
                if (!string.IsNullOrEmpty(item.ThuocTinh))
                    spm.ThemTSKT(item);
            }
            return RedirectToAction("SanPham");
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult SuaThongSoKT(string masp)
        {
            SanPhamModel spm = new SanPhamModel();
            if (spm.GetTSKT(masp).ToList().Count == 0)
            {
                ThongSoKyThuat ts = new ThongSoKyThuat();
                ts.MaSP = masp;
                List<ThongSoKyThuat> lst = new List<ThongSoKyThuat>();
                lst.Add(ts);
                return View("ThemThongSoKT", lst);
            }
            return View("SuaThongSoKT",spm.GetTSKT(masp).ToList());
        }
        
    }
}