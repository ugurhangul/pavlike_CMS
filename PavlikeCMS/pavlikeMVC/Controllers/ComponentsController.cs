using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PavlikeDATA.Models;
using PavlikeDATA.Repos;

namespace pavlikeMVC.Controllers
{    [RoutePrefix("Components")]
    public class ComponentsController : Controller
    {
        // GET: Components
    [Route("GetPages")]
        public JsonResult GetPages()
        {
            var json = JsonConvert.SerializeObject(new PageRepository().GetforPublish());
 
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}