using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PavlikeDATA.Models
{
    public class Page
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Gerekli*")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Kalıcı Bağlantı")]
        public string Url { get; set; }
        [Display(Name = "Üst Sayfa")]
        public int? RootPage { get; set; }

        [Display(Name = "Yazar")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        [Required]
        [Display(Name = "Yayında")]
        [DefaultValue(true)]
        public bool Published { get; set; }
        [Display(Name = "Aktif")]
        [DefaultValue(true)]
        public bool Active { get; set; }
        [Display(Name = "Sayfa Sıralaması")]
        public int PageOrder { get; set; }
        [Display(Name = "Taslak")]
        [Required(ErrorMessage = "*Taslak Seçmeniz Gerekiyor")]
        public int ViewId { get; set; }
        public virtual View View { get; set; }


        public virtual ICollection<Article> ArticleCollection { get; set; }
        public virtual ICollection<Page> RootPages { get; set; }


    }

}
