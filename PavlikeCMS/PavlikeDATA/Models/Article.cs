using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PavlikeDATA.Models
{

    public class Article
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*Gerekli Alan")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name = "İçerik")]
        public string Content { get; set; }

        [Display(Name = "Yayınlanacak Sayfa")]
        [Required(ErrorMessage = "*Gerekli Alan")]
        public int PageId { get; set; }
        public Page Page { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [Display(Name = "Yazı Türü")]
        [Required(ErrorMessage = "*Gerekli Alan")]
        public int? ArticleTypeId { get; set; }
        public ArticleType ArticleType { get; set; }

        [DefaultValue(true)]
        public bool Active { get; set; }
        [Display(Name = "Öne Çıkarılan Fotoğraf")]
        public int? MediaId { get; set; }
        public virtual Media Media { get; set; }
        [Display(Name = "Dış bağlantı")]
        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}

