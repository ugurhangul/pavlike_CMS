using System.Web.Mvc;
using pavlikeLibrary;
using PavlikeDATA.Models;
using PavlikeDATA.Repos;

namespace pavlikeMVC.Areas.AdminPanel.Controllers
{
    public class LinkController : Controller
    {
        readonly LinkRepository _repo = new LinkRepository();
        [HttpGet]

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public PartialViewResult _linkList()
        {

            return PartialView(_repo.GetAll());
        }

        [HttpGet]

        public PartialViewResult _Create()
        {
            return PartialView();
        }

        [HttpPost]
        public bool _Create(Link collection)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol ediniz.", Enum.ToastrType.Warning);
                return false;
            }
            if (_repo.Create(collection) == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Link eklenirken hata.", Enum.ToastrType.Error);
                return false;
            }
            else
            {
                this.AddToastMessage("", "Link başarıyla kaydedildi.", Enum.ToastrType.Success);
                return true;
            }
        }

        [HttpGet]

        public ActionResult _Edit(int id)
        {
            return View("_Create", _repo.FindbyId(id));
        }

        [HttpPost]
        public bool _Edit(Link collection)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol ediniz.", Enum.ToastrType.Warning);
                return false;
            }
            if (_repo.Update(collection) == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Link kaydedilirken hata.", Enum.ToastrType.Error);
                return false;
            }
            else
            {
                this.AddToastMessage("", "Link başarıyla kaydedildi.", Enum.ToastrType.Success);
                return true;
            }
        }

        [HttpGet]
        public string _disable()
        {
            return "Linki silmek istiyor musunuz ?";
        }

        [HttpPost]
        public bool _disable(int id)
        {
            if (_repo.FindbyIdandDisable(id) == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Link silinirken hata!", Enum.ToastrType.Error);
            }
            else
            {
                this.AddToastMessage("", "Link silindi.", Enum.ToastrType.Success);
            }
            return true;
        }
    }
}
