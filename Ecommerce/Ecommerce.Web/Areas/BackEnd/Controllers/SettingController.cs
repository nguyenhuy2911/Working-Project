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

namespace Ecommerce.Web.Areas.BackEnd.Controllers
{
    [AuthLog(Roles = "Quản trị viên,Nhân viên")]
    public class SettingController : Controller
    {
        private DisplayService _disPlayService = new DisplayService();
        // GET: BackEnd/Setting
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
            if (!string.IsNullOrEmpty(model.Id.ToString()) && model.Id > 0)
                return UpdateSetting(saveData, file);
            else
                return AddSetting(saveData, file);
        }

        private ActionResult AddSetting(Display addModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (addModel.Type == SettingType.Logo.ToString())
                {
                    string path = UploaImg(file, "Logo");
                    addModel.Value = path;
                }
                _disPlayService.AddDisPlay(addModel);
                return RedirectToAction("Index");
            }
            return CrudSetting_View(0);
        }

        private ActionResult UpdateSetting(Display updateModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (updateModel.Type == SettingType.Logo.ToString())
                {
                    string path = UploaImg(file, "Logo");
                    updateModel.Value = path;
                }
                _disPlayService.UpdateDisPlay(updateModel);
                return RedirectToAction("Index");
            }
            return CrudSetting_View(updateModel.Id);
        }

        public string UploaImg(HttpPostedFileBase file, string fileName)
        {
            if (file != null && file.ContentLength > 0)
            {
                var ext = Path.GetExtension(file.FileName);
                if (!Path.GetExtension(file.FileName).Equals(".jpg") && !Path.GetExtension(file.FileName).Equals(".png"))
                    return string.Empty;

                var path = Path.Combine(Server.MapPath("~/images"), string.Format("{0}{1}", fileName, ext));
                file.SaveAs(path);
                return string.Format("~/images/{0}{1}", fileName, ext);
            }
            return string.Empty;
        }
    }
}