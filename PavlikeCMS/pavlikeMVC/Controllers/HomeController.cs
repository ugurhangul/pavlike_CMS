using System.Web.Mvc;
using PavlikeDATA.Migrations;
using PavlikeDATA.Models;

namespace pavlikeMVC.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            new Configuration().Seed(new Context());
            return View();
        }

        public ActionResult About()
        {

            ViewBag.Message = "Your application description page.";
       
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}