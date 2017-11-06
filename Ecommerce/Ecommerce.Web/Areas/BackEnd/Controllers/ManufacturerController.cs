using Ecommerce.Domain.Model;
using Ecommerce.Web.Controllers;
using Ecommerce.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Areas.BackEnd.Controllers
{
    [AuthLog(Roles = "Quản trị viên,Nhân viên")]
    public class ManufacturerController : Controller
    {
        // GET: BackEnd/Manufacturer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetManufacturer(string key)
        {
            HangSanXuatModel _service = new HangSanXuatModel();
            ViewBag.key = key;
            var listManufacturer = _service.SearchByName(key).OrderBy(p => p.TenHang).ToList();
            return PartialView("~/Areas/BackEnd/Views/Manufacturer/_ListManufacture.cshtml", listManufacturer);
        }

        public ActionResult CrudManufacturer_View(string id)
        {
            HangSanXuatModel _service = new HangSanXuatModel();
            var model = new HangSanXuat();
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Thêm mới";
            }
            else
            {
                ViewBag.Title = "Cập nhật";
                model = _service.FindById(id);
                if (model == null)
                    model = new HangSanXuat();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveManufacturer(HangSanXuat model)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.HangSX))
                    isSuccess = AddManufacturer(model);
                else
                    isSuccess = EditManufacturer(model);
            }
            if (isSuccess)
                return RedirectToAction("Index", "Manufacturer");
            else
                return View("~/Areas/BackEnd/Views/Manufacturer/CrudManufacturer_View.cshtml", model);
        }

        private bool AddManufacturer(HangSanXuat loai)
        {
            bool checkIsSucces = false;
            HangSanXuatModel spm = new HangSanXuatModel();
            if (ModelState.IsValid && spm.KiemTraTen(loai.TenHang))
            {
                string maloai = spm.ThemHangSX(loai);
                checkIsSucces = true;
            }
            return checkIsSucces;
        }

        private bool EditManufacturer(HangSanXuat loai)
        {
            bool checkIsSucces = false;
            HangSanXuatModel _service = new HangSanXuatModel();
            if (ModelState.IsValid)
            {
                _service.EditHangSX(loai);
                checkIsSucces = true;
            }
            return checkIsSucces;
        }

        [HttpPost]
        public bool DeleteManufacturer(string id)
        {
            HangSanXuatModel _service = new HangSanXuatModel();
            if (_service.FindById(id) == null)
                return false;
            _service.DeleteHangSX(id);
            return true;
        }


    }
}