using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Areas.BackEnd.Controllers
{
    public class DasBoardController : Controller
    {
        // GET: BackEnd/DasBoard
        public ActionResult Index()
        {
            return View();
        }
    }
}