using System.Web.Mvc;
using pavlikeLibrary;
using pavlikeMVC.Areas.AdminPanel.Models;
using PavlikeDATA.Models;
using PavlikeDATA.Repos;

namespace pavlikeMVC.Areas.AdminPanel.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult _List()
        {
            return PartialView(new ArticleRepository().GetAll());
        }

        [HttpGet]
        public PartialViewResult _Create()
        {
            ViewBag.ArticleTypeId = new SelectList(new ArticleRepository().Types(), "Id", "Title");
            ViewBag.PageId = new SelectList(new PageRepository().GetAll(), "Id", "Title");

            return PartialView(new Article());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _Create(Article model)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol Ediniz", Enum.ToastrType.Warning);
                ViewBag.ArticleTypeId = new SelectList(new ArticleRepository().Types(), "Id", "Title", model.ArticleTypeId);
                ViewBag.PageId = new SelectList(new PageRepository().GetAll(), "Id", "Title", model.PageId);
                return View(model);
            }
            model.AuthorId = new AuthenticatedAuthor().Id;
            var res = new ArticleRepository().Create(model);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Makale oluşturulurken hata", Enum.ToastrType.Error);
                ViewBag.ArticleTypeId = new SelectList(new ArticleRepository().Types(), "Id", "Title", model.ArticleTypeId);
                ViewBag.PageId = new SelectList(new PageRepository().GetAll(), "Id", "Title", model.PageId);
                return View(model);
            }

            this.AddToastMessage("", "Kayıt Başarılı", Enum.ToastrType.Success);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public PartialViewResult _Edit(int id)
        {
            var item = new ArticleRepository().FindbyId(id);
            ViewBag.PageId = new SelectList(new PageRepository().GetAll(), "Id", "Title");
            ViewBag.ArticleTypeId = new SelectList(new ArticleRepository().GetAll(), "Id", "Title", item.ArticleTypeId);
            return PartialView("_Create", item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _Edit(Article modified)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol Ediniz", Enum.ToastrType.Warning);
                return View("_Create", modified);
            }
            var res = new ArticleRepository().Update(modified);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Makale güncellenirken hata", Enum.ToastrType.Error);
                return View("_Create", modified);
            }

            this.AddToastMessage("", "Kayıt Başarılı", Enum.ToastrType.Success);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public string _Delete()
        {
            return "Makaleyi silmek istiyor musunuz?";
        }

        [HttpPost]
        public bool _Delete(int id)
        {
            var res = new ArticleRepository().FindbyIdandDisable(id);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Makale silinirken hata", Enum.ToastrType.Error);
                return false;
            }
            this.AddToastMessage("", "Makale silme başarılı", Enum.ToastrType.Success);
            return true;
        }

        [HttpGet]
        public PartialViewResult _createType()
        {
            return PartialView();
        }

        [HttpPost]
        public bool _createType(ArticleType model)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol ediniz.", Enum.ToastrType.Warning);
            }
            else
            {
                model.AuthorId = new AuthenticatedAuthor().Id;
                if (new ArticleRepository().CreateType(model) == Enum.EntityResult.Success)
                {
                    this.AddToastMessage("", "Makale Tipi oluşturuldu", Enum.ToastrType.Success);
                    return true;
                }
                this.AddToastMessage("", "Makale Tipi oluşturulurken hata.", Enum.ToastrType.Error);
            }
            return false;
        }
        [HttpGet]
        public string _DeleteType()
        {
            return "Makale tipini silmek istiyor musunuz?";
        }
        [HttpPost]
        public bool _DeleteType(int id)
        {
            var res = new ArticleRepository().DisableType(id);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Makale tipi silinirken hata", Enum.ToastrType.Error);
                return false;
            }
            this.AddToastMessage("", "Makale tipi silindi", Enum.ToastrType.Success);
            return true;
        }

    }
}