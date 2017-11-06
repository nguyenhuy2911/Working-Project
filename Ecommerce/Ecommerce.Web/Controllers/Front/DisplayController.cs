using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Domain.Model;
using Ecommerce.Web.Models;
using Ecommerce.Web.Models.ViewModel;
using Ecommerce.Web.Models.Service;
using Ecommerce.Web.common;
using Ecommerce.Web.common.Const;

namespace Ecommerce.Web.Controllers
{

    public class DisplayController : Controller
    {
        // [OutputCache(CacheProfile = "SystemCache", Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Header()
        {
            var model = new Header_ViewModel();
            var disPlayService = new DisplayService();
            model.Settings = ConfigSetting.SystemConfig;
            return View(model);
        }

        [OutputCache(CacheProfile = "SystemCache", Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Footer()
        {
            var model = new Footer_ViewModel();
            var disPlayService = new DisplayService();
            model.Settings = ConfigSetting.SystemConfig;
            return PartialView("~/Views/Display/_Footer.cshtml", model);
        }

        [OutputCache(CacheProfile = "SystemCache", Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult TopContentner()
        {
            PromotionService km = new PromotionService();
            var model = km.SearchPromotion(null, null, null).Where(m => m.NgayBatDau <= DateTime.Today && m.NgayKetThuc >= DateTime.Today);
            return PartialView("~/Views/Display/_TopContentner.cshtml", model);
        }

        public ActionResult SlideShowSetting()
        {
            DisplayService gd = new DisplayService();
            List<Link> linklist = gd.GetSlideShow().ToList();
            return View(linklist);
        }

        public ActionResult SlideShow()
        {
            Link link = new Link();
            link.Group = "SlideShow";
            return View(link);
        }

        public ActionResult Breadcrumb()
        {
            var model = new Breadcrumb_ViewModel();
            if (TempData[Const.TempData_BreadCrumb] != null)
            {
                model = (Breadcrumb_ViewModel)TempData[Const.TempData_BreadCrumb];
            }
            return View(model);
        }

    }
}