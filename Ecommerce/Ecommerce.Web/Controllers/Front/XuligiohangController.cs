using Ecommerce.Web.Models;
using Ecommerce.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace  Ecommerce.Web.Controllers
{
    public class XuligiohangController : Controller
    {
        private SanPhamModel _productService = new SanPhamModel();
        public ActionResult Addcart(string sp, int quantity)
        {
            try
            {
                var temp = _productService.FindById(sp);
                int index = Kiemtratontai(sp);
                if(index == -1)
                {
                    Chitietgiohang tam = new Chitietgiohang();
                    tam.sanPham = temp;
                    tam.Soluong = quantity;
                    ManagerObiect.getIntance().giohang.addCart(tam);
                }
                else
                {
                    ManagerObiect.getIntance().giohang.getGiohang()[index].Soluong += quantity;
                }
                return PartialView("Addcart1", ManagerObiect.getIntance().giohang);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
            
        }
        public int Kiemtratontai(string id)
        {
            for (int i = 0; i < ManagerObiect.getIntance().giohang.getGiohang().Count; i++)
            {
                if (ManagerObiect.getIntance().giohang.getGiohang()[i].sanPham.MaSP == id)
                    return i;
            }
            return -1;
        }
        // GET: Xuligiohang
        public ActionResult Xoagiohang(int index)
        {
            ManagerObiect.getIntance().giohang.removeCart(index);
            return RedirectToAction("basicXuLiGiohang");
        }
        public ActionResult Thaydoisoluong(int index,string value)
        {
            ManagerObiect.getIntance().giohang.Changequanlity(index, value);
            return RedirectToAction("basicXuLiGiohang");
        }
        
        public ActionResult basicXuLiGiohang()
        {
            return PartialView("basicXuLiGiohang", ManagerObiect.getIntance().giohang);
        }
        public ActionResult UpdategiohangContent()
        {
            return PartialView("Addcart1", ManagerObiect.getIntance().giohang);
        }
        public ActionResult CartTitle()
        {
            return PartialView("Addcart1",ManagerObiect.getIntance().giohang);
        }
        public ActionResult CartOrder()
        {
            return PartialView("Ordercheckout", ManagerObiect.getIntance().giohang);
        }
    }
}