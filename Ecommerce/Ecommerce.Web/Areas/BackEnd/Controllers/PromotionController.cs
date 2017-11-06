using Ecommerce.Domain.Model;
using Ecommerce.Web.Controllers;
using Ecommerce.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Areas.BackEnd.Controllers
{
    [AuthLog(Roles = "Quản trị viên,Nhân viên")]
    public class PromotionController : Controller
    {
        PromotionService _service = new PromotionService();
        // GET: BackEnd/Promotion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPromotion(string key, DateTime? start, DateTime? end, int? page)
        {
            ViewBag.key = key;
            ViewBag.start = start;
            ViewBag.end = end;
            var listPromotion = _service.SearchPromotion(key, start, end).OrderByDescending(p => p.NgayBatDau).ToPagedList(page ?? 1, 10);
            return PartialView("~/Areas/BackEnd/Views/Promotion/_ListPromotion.cshtml", listPromotion);

        }

        public ActionResult CrudPromotion_View(string id)
        {
            var model = new Promotion();
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Thêm mới";
            }
            else
            {
                ViewBag.Title = "Cập nhật";
                model = _service.FindById(id);
                if (model == null)
                    model = new Promotion();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePromotion(Promotion model, HttpPostedFileBase file)
        {

            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.MaKM))
                    isSuccess = AddPromotion(model, file);
                else
                    isSuccess = EditPromotion(model, file);
            }
            if (isSuccess)
                return RedirectToAction("Index", "Promotion");
            else
                return View("~/Areas/BackEnd/Views/Promotion/CrudPromotion_View.cshtml", model);
        }

        private bool AddPromotion(Promotion model, HttpPostedFileBase file)
        {
            bool checkSuccess = false;
            if (ModelState.IsValid && _service.KiemTraTen(model.TenCT))
            {
                string makm = _service.AddPromotion(model);
                UploadAnh(file, makm + "1");
                checkSuccess = true;
            }
            return checkSuccess;
        }

        public bool EditPromotion(Promotion loai, HttpPostedFileBase file)
        {
            bool checkSucess = false;
            if (ModelState.IsValid)
            {
                _service.EditPromotion(loai);
                UploadAnh(file, loai.MaKM + "1");
                checkSucess = true;
            }
            return checkSucess;
        }

        [HttpPost]
        public bool DeletePromotion(string id)
        {
            PromotionService spm = new PromotionService();
            DeleteAnh(spm.FindById(id).AnhCT);
            spm.DeletePromotion(id);
            return true;
        }


        public bool UploadAnh(HttpPostedFileBase file, string fileName)
        {
            if (file != null && file.ContentLength > 0)
            {
                var name = Path.GetExtension(file.FileName);
                if (!Path.GetExtension(file.FileName).Equals(".jpg"))
                {
                    return false;
                }
                var path = Path.Combine(Server.MapPath("~/images/khuyenmai"), fileName + ".jpg");
                file.SaveAs(path);
                return true;
            }
            return false;
        }

        public bool DeleteAnh(string filename)
        {
            string fullPath = Request.MapPath("~/images/khuyenmai/" + filename);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            return false;
        }


    }
}