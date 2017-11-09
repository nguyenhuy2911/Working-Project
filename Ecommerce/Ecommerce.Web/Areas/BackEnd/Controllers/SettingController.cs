using Ecommerce.Domain.Model;
using Ecommerce.Web.Areas.BackEnd.Models;
using Ecommerce.Web.common;
using Ecommerce.Web.common.Const.Enum;
using Ecommerce.Web.Controllers;
using Ecommerce.Web.Models.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
namespace Ecommerce.Web.Areas.BackEnd.Controllers
{
    [AuthLog(Roles = "Quản trị viên,Nhân viên")]
    public class SettingController : Controller
    {
        private DisplayService _disPlayService = new DisplayService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSetting()
        {
            var model = _disPlayService.GetDisPlays().ToList();
            return PartialView("_ListSetting", model);
        }

        public ActionResult CrudSetting_View(int? id)
        {
            var model = new CrudSetting_ViewModel();
            ViewBag.Title = "Thêm cài đặt";
            if (id != null && id > 0)
            {
                var data = _disPlayService.FindById(id ?? 0);
                model.Id = data.Id;
                model.Type = data.Type;
                model.Value = data.Value;
                if (model.Type == SettingType.About.ToString() || model.Type == SettingType.Contact.ToString() || model.Type == SettingType.Logo.ToString())
                {
                    model.Description = model.Value;
                }
                ViewBag.Title = "Cập nhật";
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSetting(CrudSetting_ViewModel model, HttpPostedFileBase file)
        {
            var saveData = new Display()
            {
                Id = model.Id,
                Type = model.Type,
                Value = model.Value
            };
           
            if (model.Type == SettingType.About.ToString() || model.Type == SettingType.Contact.ToString()|| model.Type == SettingType.Logo.ToString())
            {
                saveData.Value = WebUtility.HtmlEncode(model.Description);
            }
            if (!string.IsNullOrEmpty(model.Id.ToString()) && model.Id > 0)
                return UpdateSetting(saveData, file);
            else
                return AddSetting(saveData, file);
        }

        private ActionResult AddSetting(Display addModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                _disPlayService.AddDisPlay(addModel);
                return RedirectToAction("Index");
            }
            return CrudSetting_View(0);
        }

        private ActionResult UpdateSetting(Display updateModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                _disPlayService.UpdateDisPlay(updateModel);
                return RedirectToAction("Index");
            }
            return CrudSetting_View(updateModel.Id);
        }
    }
}