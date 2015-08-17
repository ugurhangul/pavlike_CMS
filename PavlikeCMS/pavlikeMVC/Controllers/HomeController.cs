using System.Web.Mvc;
using PavlikeDATA.Repos;

namespace pavlikeMVC.Controllers
{
   
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        readonly PageRepository _page = new PageRepository();
        [Route]
        public ActionResult Index()
        {
            return View();
        }
        [Route("{url}")]
        public ActionResult Index(string url)
        {
       
            return View("GetPage", _page.FindbyUrl(url));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}