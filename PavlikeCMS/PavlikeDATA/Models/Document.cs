using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlikeDATA.Models
{
    public class Document
    {
        public int Id { get; set; }
        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "*Gerekli Alan")]
        public string Title { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Yazar")]
        public int? AuthorId { get; set; }
        public Author Author { get; set; }
        [Display(Name = "Dosya")]
        public int FileId { get; set; }
        public File File { get; set; }
        [DefaultValue(true)]
        public bool Active { get; set; }
        [Display(Name = "Yayınlanma")]
        public DateTime? Published { get; set; }

    }
}
