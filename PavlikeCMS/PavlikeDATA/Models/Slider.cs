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
    public class Slider
    {
        public int Id { get; set; }
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Display(Name = "Alt Başlık")]
        public string SubTitle { get; set; }
        [Display(Name = "Detay")]
        public string Detail { get; set; }
        [Required(ErrorMessage = "*Gerekli Alan")]
        [Display(Name = "Dosya")]
        public int FileId { get; set; }
        public File File { get; set; }

        [DefaultValue(true)]
        public bool Active { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }


    }
}
