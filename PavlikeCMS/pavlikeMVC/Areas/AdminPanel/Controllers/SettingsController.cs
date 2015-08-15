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

    public class SettingsController : Controller
    {
        private readonly SettingRepository _repository = new SettingRepository();

        public ActionResult Index()
        {
            return View(_repository.GetSettings());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Settings settings, HttpPostedFileBase logoFile)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol ediniz.", Enum.ToastrType.Warning);
                return View(settings);
            }
            if (logoFile?.ContentLength > 0)
            {
                var logo = FileSave(logoFile, "Media", Enum.FileType.Media);
                if (logo != null)
                {
                    var media = new Media
                    {
                        FileId = logo.Id,
                        Active = true,
                        AuthorId = new AuthenticatedAuthor().Id,
                        CreateDateTime = DateTime.Now,
                        AltText = settings.Title,
                        Description = settings.Description,
                        Title = settings.Title
                    };
                    if (new MediaRepository().Create(media) == Enum.EntityResult.Success)
                    {
                        this.AddToastMessage("", "Logo yüklendi", Enum.ToastrType.Success);
                        settings.LogoId = media.Id;
                    }
                    else
                    {
                        FileDelete(logo);
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

            if (_repository.Update(settings) == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Ayarlar kaydedilirken hata", Enum.ToastrType.Error);
                return View(settings);
            }
            this.AddToastMessage("", "Ayarlar kaydedildi", Enum.ToastrType.Success);
            return RedirectToAction("Index");
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

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
