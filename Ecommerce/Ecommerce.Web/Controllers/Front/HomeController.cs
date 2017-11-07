using Ecommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Ecommerce.Domain.Infrastructure;
using Ecommerce.Domain.Model;
using Ecommerce.Web.Models.Service;
using Ecommerce.Web.common.Const.Enum;
using Ecommerce.Web.Models.ViewModel;
using Ecommerce.Web.common.Const;

namespace Ecommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private DisplayService disPlayService = new DisplayService();

        // [OutputCache(CacheProfile = "SystemCache", Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            ManagerObiect.getIntance();
            return View();
        }

        public ActionResult Cart()
        {
            return View(ManagerObiect.getIntance().giohang);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên,Khách hàng")]
        //Đơn hàng
        public ActionResult Xemdonhang(string maKH)
        {
            List<DonhangKHModel> temp = new List<DonhangKHModel>();
            if (maKH.Length != 0)
            {
                DonhangKHModel dh = new DonhangKHModel();
                temp = dh.Xemdonhang(maKH);
            }
            return View(temp);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên,Khách hàng")]
        public ActionResult Huydonhang(string maDH)
        {
            DonhangKHModel dh = new DonhangKHModel();
            dh.HuyDH(maDH);
            var donhang = dh.Xemdonhang(User.Identity.GetUserId());
            return View(donhang);
        }

        [AllowAnonymous]
        public ActionResult Checkout()
        {

            DonhangKHModel dh = new DonhangKHModel();
            Donhangtongquan model = new Donhangtongquan();
            dh.nguoiMua = dh.Xemttnguoidung(User.Identity.GetUserId());
            if (dh.nguoiMua != null)
            {
                model = new Donhangtongquan()
                {
                    OrderBy = dh.nguoiMua.HoTen,
                    ReceivedPerson = dh.nguoiMua.HoTen,
                    PhoneNumber = dh.nguoiMua.PhoneNumber,
                    Address = dh.nguoiMua.DiaChi
                };
            }

            return View(model);

        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Checkout(Donhangtongquan dh)
        {

            DonhangKHModel dhmodel = new DonhangKHModel();
            dhmodel.Luudonhang(dh, !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "customer", ManagerObiect.getIntance().giohang);
            ManagerObiect.getIntance().giohang.EmptyCart();
            return RedirectToAction("Index", "Home");

        }

        public ActionResult MainMenu()
        {
            MainMenuModel mnmodel = new MainMenuModel();
            var menulist = mnmodel.GetMenuList();
            return PartialView("_MainMenuPartial", menulist);
        }

        public ActionResult About()
        {
            var display = disPlayService.GetDisPlays()?.Where(p => p.Type == SettingType.About.ToString())?.FirstOrDefault();
            var breadCrumbModel = new Breadcrumb_ViewModel()
            {
                Title1 = "Giới thiệu",
                Title1_Url = "#",

            };
            TempData[Const.TempData_BreadCrumb] = breadCrumbModel;
            return View(display);
        }

        [OutputCache(CacheProfile = "SystemCache", Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Contact()
        {

            var display = disPlayService.GetDisPlays()?.Where(p => p.Type == SettingType.Contact.ToString())?.FirstOrDefault();
            var breadCrumbModel = new Breadcrumb_ViewModel()
            {
                Title1 = "Liên hệ",
                Title1_Url = "#",
               
            };
            TempData[Const.TempData_BreadCrumb] = breadCrumbModel;
            return View(display);
        }

    }
}