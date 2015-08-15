using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pavlikeMVC.Areas.AdminPanel.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        // GET: AdminPanel/AdminPanel
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult _Toastr()
        {
            return PartialView();
        }
    }
}
