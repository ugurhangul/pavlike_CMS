using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using pavlikeLibrary;
using pavlikeMVC.Models;
using PavlikeDATA.Models;
using PavlikeDATA.Repos;

namespace pavlikeMVC.Areas.AdminPanel.Controllers
{
    [Authorize]

    public class AuthorsController : Controller
    {
        readonly AuthorRepository _author = new AuthorRepository();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AuthorsController()
        {
        }

        public AuthorsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public ActionResult Index()
        {
            return View(_author.GetAll());
        }



        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterViewModel model)
        {
            if (!ModelState.IsValid) { this.AddToastMessage("", "Alanları kontrol ediniz.", Enum.ToastrType.Warning); return View(model); }
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = UserManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                var res = new AuthorRepository().Create(new Author
                {
                    Active = true,
                    EMail = user.Email,
                    UserGuid = user.Id,
                    Name = model.Name,
                    Surname = model.Surname,
                    DateofBirth = model.DateofBirth
                });

                if (res == Enum.EntityResult.Failed)
                {
                    UserManager.Delete(user);
                }
                else
                {
                    this.AddToastMessage("", "Kullanıcı oluşturuldu.", Enum.ToastrType.Success);
                    return RedirectToAction("Index");
                }
            }
     
            AddErrors(result);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            return View(_author.FindbyId(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol ediniz", Enum.ToastrType.Warning);
                return View(author);
            }
            var res = _author.Update(author);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Kullanıcı kaydedilirken hata", Enum.ToastrType.Error);
                return View(author);
            }
            this.AddToastMessage("", "Kayıt başarılı", Enum.ToastrType.Success);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public string Delete()
        {
            return "Kullanıcıyı silmek istiyor musunuz ?";
        }


        [HttpPost]
        public bool Delete(int id)
        {
            var res = _author.FindbyIdandDisable(id);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Kullanıcı silinirken hata.", Enum.ToastrType.Error);
                return false;
            }

            this.AddToastMessage("", "Kullanıcı silindi.", Enum.ToastrType.Success);
            return true;
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.AddToastMessage("", error, Enum.ToastrType.Error);
            }
        }
        public ActionResult _SetPassword(string userguid)
        {
            ViewBag.userguid = userguid;
            return View();
        }


        [HttpPost]
        public void _SetPassword(SetPasswordViewModel model, string userguid)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("",
                    model.ConfirmPassword != model.NewPassword ? "Parolalar uyuşmuyor." : "Alanları kontrol ediniz.",
                    Enum.ToastrType.Warning);

                return;
            }
            var user = UserManager.FindById(userguid);
            if (!UserManager.RemovePassword(user.Id).Succeeded) { this.AddToastMessage("", "şifre değiştirilirken hata.", Enum.ToastrType.Error); return; }
            var result = UserManager.AddPassword(user.Id, model.NewPassword);
            if (result.Succeeded)
            {
                this.AddToastMessage("", "Şifre Değiştirildi.", Enum.ToastrType.Success);
                return;
            }
            AddErrors(result);
        }

   

    }
}
