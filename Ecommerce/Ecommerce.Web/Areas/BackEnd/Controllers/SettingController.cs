using Ecommerce.Domain.Model;
using Ecommerce.Web.Areas.BackEnd.Models;
using Ecommerce.Web.common;
using Ecommerce.Web.Controllers;
using Ecommerce.Web.Models.Service;
using System;
using System.Collections.Generic;
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

        public ActionResult CrudSetting_View(int Id)
        {
            var model = new CrudSetting_ViewModel();
            if (Id > 0)
            {
                var data = _disPlayService.FindById(Id);
                model.Id = data.Id;
                model.Type = data.Type;
                model.Value = data.Value;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSetting(CrudSetting_ViewModel model)
        {
            var saveData = new Display()
            {
                Id = model.Id,
                Type = model.Type,
                Value = model.Value
            };
            if (!string.IsNullOrEmpty(model.Id.ToString()) && model.Id > 0)
                return UpdateSetting(saveData);
            else
                return AddSetting(saveData);
        }

        private ActionResult AddSetting(Display addModel)
        {
            if (ModelState.IsValid)
            {
                _disPlayService.AddDisPlay(addModel);
                return RedirectToAction("Index");
            }
            return CrudSetting_View(0);
        }

        private ActionResult UpdateSetting(Display updateModel)
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