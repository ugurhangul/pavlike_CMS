using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pavlikeLibrary;
using pavlikeMVC.Areas.AdminPanel.Models;
using PavlikeDATA.Models;
using PavlikeDATA.Repos;
using static System.String;
using Enum = pavlikeLibrary.Enum;
using File = PavlikeDATA.Models.File;

namespace pavlikeMVC.Areas.AdminPanel.Controllers
{

    [Authorize]
    public class FilesController : Controller
    {
        readonly AlbumRepository _albumRepo = new AlbumRepository();
        readonly DocumentRepository _documentRepo = new DocumentRepository();
        readonly SliderRepository _sliderRepo = new SliderRepository();

        public ActionResult Index()
        {
            return View();
        }


        #region Gallery
        public ActionResult Gallery()
        {
            ViewBag.AlbumCount = new AlbumRepository().Count();
            ViewBag.MediaCount = new MediaRepository().Count();
            return View(new AlbumRepository().GetAll());
        }


        [HttpGet]
        public PartialViewResult _galleryCreate()
        {

            return PartialView(new Album());
        }

        [HttpPost]
        public int _galleryCreate(Album model)
        {

            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol Ediniz", Enum.ToastrType.Warning);

                return 0;
            }
            model.AuthorId = new AuthenticatedAuthor().Id;
            var res = new AlbumRepository().Create(model);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Album oluşturulurken hata", Enum.ToastrType.Error);
                return 0;
            }

            this.AddToastMessage("", "Kayıt Başarılı", Enum.ToastrType.Success);
            return model.Id;

        }



        [HttpGet]
        public PartialViewResult GalleryView(int id)
        {
            var item = new AlbumRepository().FindbyId(id);
            return PartialView(item);
        }

        [HttpPost]
        public void _galleryEdit(string baslik, int id)
        {
            var album = _albumRepo.FindbyId(id);
            album.Title = baslik;
            var res = _albumRepo.Update(album);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Album güncellenirken hata", Enum.ToastrType.Error);

            }
            else
            {
                this.AddToastMessage("", "Kayıt Başarılı", Enum.ToastrType.Success);
            }



        }

        [HttpPost]
        public void _galleryDelete(int id)
        {
            var res = new AlbumRepository().FindbyIdandDisable(id);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Album silinirken hata", Enum.ToastrType.Error);

            }
            else
            {
                this.AddToastMessage("", "Album silme başarılı", Enum.ToastrType.Success);
            }
        }
        #endregion

        #region Media


        [HttpGet]
        public PartialViewResult _mediaEdit(int id)
        {
            return PartialView(new MediaRepository().FindbyId(id));
        }
        [HttpPost]
        public void AlbumMediaUpload(int albumId)
        {

            var file = Request.Files[0];

            if (!(file?.ContentLength > 0)) return;
            var res = FileSave(file, "Media", Enum.FileType.Media);
            if (res == null)
            {
                this.AddToastMessage("", "Dosya yüklenirken hata!");
                return;
            }
            var media = new Media
            {
                Active = true,
                AuthorId = new AuthenticatedAuthor().Id,
                CreateDateTime = DateTime.Now,
                FileId = res.Id,
                Title = res.Title

            };
            if (new MediaRepository().Create(media) == Enum.EntityResult.Success)
            {

                if (new AlbumMediaRepository().Create(new AlbumMedia { AlbumId = albumId, MediaId = media.Id }) !=
                    Enum.EntityResult.Success)
                {
                    this.AddToastMessage("", "Album kaydedilirken hata");
                }
            }
            else
            {
                this.AddToastMessage("", "Album kaydedilirken hata");
            }

        }
        [HttpPost]
        public bool _mediaEdit(Media media)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol edniz", Enum.ToastrType.Warning);
                return false;
            }
            var res = new MediaRepository().Update(media);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Görsel kaydedilirken hata", Enum.ToastrType.Error);
                return false;
            }
            this.AddToastMessage("", "Görsel kaydedildi.", Enum.ToastrType.Success);
            return true;
        }
        [HttpPost]
        public bool _mediaDelete(int id)
        {
            if (new AlbumMediaRepository().DeleteAllbyMediaId(id) == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Görsel silinirken hata", Enum.ToastrType.Error);
                return false;
            }
            if (new MediaRepository().FindbyIdandDisable(id) == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Görsel silinirken hata.", Enum.ToastrType.Error);
                return false;
            }

            this.AddToastMessage("", "Görsel silindi.", Enum.ToastrType.Success);
            return true;
        }
        [HttpPost]
        public bool _AlbumMediaDelete(int mediaId, int albumId)
        {
            var res = new AlbumMediaRepository().FindandDelete(mediaId, albumId);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Görsel silinirken hata", Enum.ToastrType.Error);
                return false;
            }
            this.AddToastMessage("", "Görsel silindi", Enum.ToastrType.Success);
            return true;
        }
        [HttpPost]
        public bool _AlbumMediaInsert(int[] medias, int albumId)
        {
            if (medias.Select(item => new AlbumMediaRepository().Create(new AlbumMedia { AlbumId = albumId, MediaId = item })).Any(res => res == Enum.EntityResult.Failed))
            {
                this.AddToastMessage("", "Görseller eklenirken hata!", Enum.ToastrType.Error);
                return false;
            }
            this.AddToastMessage("", "Görseller eklendi", Enum.ToastrType.Success);
            return true;
        }

        [HttpGet]
        public PartialViewResult _mediaList()
        {
            return PartialView(new MediaRepository().GetAll());
        }
        #endregion

        #region Documents
        public ActionResult Documents()
        {
            ViewBag.Count = _documentRepo.Count();
            return View(_documentRepo.GetAll());
        }

        [HttpGet]
        public ActionResult DocumentCreate()
        {
            return View(new Document());
        }

        [HttpPost]
        public ActionResult DocumentCreate(Document model, HttpPostedFileBase uploadFile)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol Ediniz", Enum.ToastrType.Warning);

                return View(model);
            }

            if (!(uploadFile?.ContentLength > 0))
            {
                this.AddToastMessage("", "Belge seçmelisiniz!", Enum.ToastrType.Error);
                return View(model);
            }
            var uploadedFile = FileSave(uploadFile, "Document", Enum.FileType.Document);
            if (uploadedFile == null)
            {
                this.AddToastMessage("", "Dosya kaydedilirken hata.", Enum.ToastrType.Error);
                return View(model);
            }

            model.AuthorId = new AuthenticatedAuthor().Id;
            model.FileId = uploadedFile.Id;
            var res = _documentRepo.Create(model);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Dosya kaydedilirken hata", Enum.ToastrType.Error);
                FileDelete(new FileRepository().FindbyId(model.FileId));
                return View(model);
            }

            this.AddToastMessage("", "Kayıt Başarılı", Enum.ToastrType.Success);
            return RedirectToAction("Documents");

        }

        [HttpGet]
        public ActionResult DocumentEdit(int id)
        {
            return PartialView("DocumentCreate", _documentRepo.FindbyId(id));
        }

        [HttpPost]
        public ActionResult DocumentEdit(Document modified)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol Ediniz", Enum.ToastrType.Warning);
                return View("DocumentCreate", modified);
            }
            var res = new DocumentRepository().Update(modified);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Belge güncellenirken hata", Enum.ToastrType.Error);
                return View("DocumentCreate", modified);
            }

            this.AddToastMessage("", "Kayıt Başarılı", Enum.ToastrType.Success);
            return RedirectToAction("Documents");
        }

        [HttpPost]
        public bool _documentDelete(int id)
        {
            var res = new DocumentRepository().FindbyIdandDisable(id);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Belge silinirken hata", Enum.ToastrType.Error);
                return false;
            }
            this.AddToastMessage("", "Belge silme başarılı", Enum.ToastrType.Success);
            return true;
        }

        #endregion

        #region Sliders

        public ActionResult Slider()
        {
            return View(_sliderRepo.GetAll());
        }

        [HttpGet]
        public ActionResult SliderCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SliderCreate(Slider model, HttpPostedFileBase uploadedFile)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol Ediniz", Enum.ToastrType.Warning);
                return View(model);
            }
            if (uploadedFile == null)
            {
                this.AddToastMessage("", "Görsel seçmelisiniz", Enum.ToastrType.Warning);
                return View(model);
            }
            if (uploadedFile.ContentLength < 0)
            {
                this.AddToastMessage("", "Görsel seçmelisiniz", Enum.ToastrType.Warning);
                return View(model);
                }

            var file = FileSave(uploadedFile, "Slider", Enum.FileType.Media);
            if (file == null)
            {
                this.AddToastMessage("", "Görsel yüklenirken hata!", Enum.ToastrType.Error);
                return View(model);
            }
            model.AuthorId = new AuthenticatedAuthor().Id;
            model.FileId = file.Id;
            var res = new SliderRepository().Create(model);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Slider oluşturulurken hata", Enum.ToastrType.Error);
                return View(model);
            }

            this.AddToastMessage("", "Kayıt Başarılı", Enum.ToastrType.Success);
            return RedirectToAction("Slider");

        }

        [HttpGet]
        public ActionResult SliderEdit(int id)
        {
            var item = new SliderRepository().FindbyId(id);
            return View("SliderCreate", item);
        }

        [HttpPost]
        public ActionResult SliderEdit(Slider modified)
        {
            if (!ModelState.IsValid)
            {
                this.AddToastMessage("", "Alanları kontrol Ediniz", Enum.ToastrType.Warning);
                return View("SliderCreate", modified);
            }
            var res = new SliderRepository().Update(modified);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Slider güncellenirken hata", Enum.ToastrType.Error);
                return View("SliderCreate", modified);
            }

            this.AddToastMessage("", "Kayıt Başarılı", Enum.ToastrType.Success);
            return RedirectToAction("Slider");
        }

        [HttpPost]
        public void _sliderDelete(int id)
        {
            var res = new SliderRepository().FindbyIdandDisable(id);
            if (res == Enum.EntityResult.Failed)
            {
                this.AddToastMessage("", "Slider silinirken hata", Enum.ToastrType.Error);
          
            }
            else
            {
                this.AddToastMessage("", "Slider silme başarılı", Enum.ToastrType.Success);
            }
            
       
        }
#endregion 
        #region FileWorks

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
                    string tempFileName = Format("{0}-{1}", fileNameOnly, count++);
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

        #endregion

    }
}

