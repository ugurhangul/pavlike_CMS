using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using pavlikeLibrary;
using PavlikeDATA.Models;
using PavlikeDATA.Repos;

namespace pavlikeMVC.Controllers
{

    [RoutePrefix("")]
    public class HomeController : Controller
    {
        readonly PageRepository _page = new PageRepository();
        readonly AlbumRepository _galleries = new AlbumRepository();
        readonly MediaRepository _medias = new MediaRepository();

        [Route]
        public ActionResult Index()
        {
            return View("Homepage");
        }
        [Route("{url}")]
        public ActionResult Index(string url)
        {
            switch (url.ToLower())
            {
                case "adminpanel":
                case "admin":
                case "panel":
                    return RedirectToAction("Login", "Account", new { area = "AdminPanel" });
            }
            var page = _page.FindbyUrl(url);
            if (page != null)
            {
            return View(page.View.ViewFileName, page);
            }
            else
            {
                return HttpNotFound("Sayfa Bulunamadı");
            }
        }

        [Route("galeri")]
        public ActionResult Galeri()
        {
            return View("Gallery", _galleries.GetAll());
        }
        [Route("albumdetay/{title}/{id}")]
        public ActionResult Album(string title, int id)
        {
            ViewBag.Title = title;
            var medias = _galleries.FindbyId(id).AlbumMediaCollection.ToList().Select(item => _medias.FindbyId(item.MediaId)).ToList();
            return View("Media", medias);
        }


    }
}