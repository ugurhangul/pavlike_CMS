using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using pavlikeLibrary;
using pavlikeMVC.Areas.AdminPanel.Models;
using PavlikeDATA.Models;
using PavlikeDATA.Repos;
using Enum = pavlikeLibrary.Enum;
using File = PavlikeDATA.Models.File;

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
        public ActionResult _Create(Article model,HttpPostedFileBase photo)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol Ediniz", Enum.ToastrType.Warning);
                ViewBag.ArticleTypeId = new SelectList(new ArticleRepository().Types(), "Id", "Title", model.ArticleTypeId);
                ViewBag.PageId = new SelectList(new PageRepository().GetAll(), "Id", "Title", model.PageId);
                return View(model);
            }

            if (photo?.ContentLength > 0)
            {
                var photofile = FileSave(photo, "Media", Enum.FileType.Media);
                if (photofile != null)
                {
                    var media = new Media
                    {
                        FileId = photofile.Id,
                        Active = true,
                        AuthorId = new AuthenticatedAuthor().Id,
                        CreateDateTime = DateTime.Now,
                        AltText = model.Title,
                        Description = model.Title,
                        Title = model.Title
                    };
                    if (new MediaRepository().Create(media) == Enum.EntityResult.Success)
                    {
                        this.AddToastMessage("", "Logo yüklendi", Enum.ToastrType.Success);
                        model.MediaId = media.Id;
                    }
                    else
                    {
                        FileDelete(photofile);
                        this.AddToastMessage("", "Logo yüklenirken hata", Enum.ToastrType.Error);
                        this.AddToastMessage("", "Sayfayı yenileyin ve tekrar deneyin.");
                    }

                }
                else
                {
                    this.AddToastMessage("", "Logo yüklenirken hata", Enum.ToastrType.Error);
                    this.AddToastMessage("", "Sayfayı yenileyin ve tekrar deneyin.");
                }
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
            ViewBag.ArticleTypeId = new SelectList(new ArticleRepository().Types(), "Id", "Title", item.ArticleTypeId);
            return PartialView("_Create", item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _Edit(Article modified,HttpPostedFileBase photo)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol Ediniz", Enum.ToastrType.Warning);
                return View("_Create", modified);
            }

            if (photo?.ContentLength > 0)
            {
                var photofile = FileSave(photo, "Media", Enum.FileType.Media);
                if (photofile != null)
                {
                    var media = new Media
                    {
                        FileId = photofile.Id,
                        Active = true,
                        AuthorId = new AuthenticatedAuthor().Id,
                        CreateDateTime = DateTime.Now,
                        AltText = modified.Title,
                        Description = modified.Title,
                        Title = modified.Title
                    };
                    if (new MediaRepository().Create(media) == Enum.EntityResult.Success)
                    {
                        this.AddToastMessage("", "Logo yüklendi", Enum.ToastrType.Success);
                        modified.MediaId = media.Id;
                    }
                    else
                    {
                        FileDelete(photofile);
                        this.AddToastMessage("", "Logo yüklenirken hata", Enum.ToastrType.Error);
                        this.AddToastMessage("", "Sayfayı yenileyin ve tekrar deneyin.");
                    }

                }
                else
                {
                    this.AddToastMessage("", "Logo yüklenirken hata", Enum.ToastrType.Error);
                    this.AddToastMessage("", "Sayfayı yenileyin ve tekrar deneyin.");
                }
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

        public File FileSave(HttpPostedFileBase file, string folder, Enum.FileType fileType)
        {
            string newFullPath = "";
            try
            {
                int count = 1;
                string fileNameOnly = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                var mappath = "~/Content/" + "Uploads/" + folder + "/";
                newFullPath = Path.Combine(mappath + fileNameOnly + extension);

                if (!Directory.Exists(Server.MapPath(mappath)))
                {
                    Directory.CreateDirectory(Server.MapPath(mappath));
                }

                while (System.IO.File.Exists(Server.MapPath(newFullPath)))
                {
                    string tempFileName = String.Format("{0}-{1}", fileNameOnly, count++);
                    newFullPath = Path.Combine(mappath, tempFileName + extension);
                }

                file.SaveAs(Server.MapPath(newFullPath));
                var uploadedFile = new File
                {
                    Active = true,
                    AuthorId = new AuthenticatedAuthor().Id,
                    Extension = extension,
                    FileType = fileType,
                    FileName = fileNameOnly,
                    Folder = folder,
                    Url = newFullPath,
                    UploadDateTime = DateTime.Now,
                    Title = fileNameOnly
                };

                return new FileRepository().Create(uploadedFile) == Enum.EntityResult.Failed ? null : uploadedFile;
            }
            catch (Exception)
            {
                System.IO.File.Delete(Server.MapPath(newFullPath));
                return null;
            }
        }

        public bool FileDelete(File file)
        {
            if (file == null) return true;
            if (System.IO.File.Exists(Server.MapPath(file.Url)))
            {
                System.IO.File.Delete(Server.MapPath(file.Url));
            }
            return new FileRepository().Delete(file) != Enum.EntityResult.Failed;
        }

    }



}