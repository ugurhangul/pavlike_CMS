using System.Web.Mvc;
using pavlikeLibrary;
using pavlikeMVC.Areas.AdminPanel.Models;
using PavlikeDATA.Models;
using PavlikeDATA.Repos;

namespace pavlikeMVC.Areas.AdminPanel.Controllers
{
    [Authorize]

    public class PagesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult _PageList()
        {
            return PartialView(new PageRepository().GetAll());
        }

        [HttpGet]
        public PartialViewResult _Create()
        {
            ViewBag.RootPage = new SelectList(new PageRepository().GetAll(), "Id", "Title");
            return PartialView(new Page());
        }

        [HttpPost]
        public bool _Create(Page model)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol Ediniz", Enum.ToastrType.Warning);

                return false;
            }
            model.AuthorId = new AuthenticatedAuthor().Id;
            var res = new PageRepository().Create(model);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Sayfa oluşturulurken hata", Enum.ToastrType.Error);
                return false;
            }

            this.AddToastMessage("", "Kayıt Başarılı", Enum.ToastrType.Success);
            return true;

        }

        [HttpGet]
        public PartialViewResult _Edit(int id)
        {
            var item = new PageRepository().FindbyId(id);
            ViewBag.RootPage = new SelectList(new PageRepository().GetAll(), "Id", "Title", item.RootPage);
            return PartialView("_Create", item);
        }

        [HttpPost]
        public bool _Edit(Page modified)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol Ediniz", Enum.ToastrType.Warning);
                return false;
            }
            var res = new PageRepository().Update(modified);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Sayfa güncellenirken hata", Enum.ToastrType.Error);
                return false;
            }

            this.AddToastMessage("", "Kayıt Başarılı", Enum.ToastrType.Success);
            return true;
        }

        [HttpGet]
        public string _Delete()
        {
            return "Bu sayfayı silmek istiyor musunuz?";
        }

        [HttpPost]
        public bool _Delete(int id)
        {
            var res = new PageRepository().FindbyIdandDisable(id);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Sayfa silinirken hata", Enum.ToastrType.Error);
                return false;
            }
            this.AddToastMessage("", "Sayfa silme başarılı", Enum.ToastrType.Success);
            return true;
        }


        [HttpGet]
        public ActionResult Preview(int id)
        {
            return View(new PageRepository().FindbyId(id));
        }
    }
}
