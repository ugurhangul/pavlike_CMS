using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pavlikeLibrary;
using PavlikeDATA.Models;
using PavlikeDATA.Repos;
using Enum = pavlikeLibrary.Enum;

namespace pavlikeMVC.Areas.AdminPanel.Controllers
{
    public class ThemeOptionsController : Controller
    {
        readonly ThemeOptionsRepository _repository = new ThemeOptionsRepository();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Views()
        {
            return View(_repository.GetViews());
        }

        public ActionResult RegisterView()
        {
            return View(new View());
        }


        [HttpPost]
        public ActionResult Create(View model)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları Kontrol Ediniz", Enum.ToastrType.Warning);
                return View(model);
            }
            var res = _repository.RegisterView(model);
            if (res == Enum.EntityResult.Success)
            {
                this.AddToastMessage("", "View Kaydedildi", Enum.ToastrType.Success);
                return RedirectToAction("Views");

            }
            else
            {
                this.AddToastMessage("", "View Kaydedilirken Hata", Enum.ToastrType.Error);
                return View(model);

            }

        }


        [HttpPost]
        public void Delete(int id)
        {
            if (_repository.DeleteView(id) == Enum.EntityResult.Success)
            {
                this.AddToastMessage("", "View Silindi", Enum.ToastrType.Success);
            }
            else
            {
                this.AddToastMessage("", "View Silinirken Hata", Enum.ToastrType.Error);
            }
        }
    }
}
