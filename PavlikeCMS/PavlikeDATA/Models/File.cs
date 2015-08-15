using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Enum = pavlikeLibrary.Enum;

namespace PavlikeDATA.Models
{

    public class File
    {
        public int Id { get; set; }
        [Display(Name = "Başlık")]
   
        public string Title { get; set; }

        [Display(Name = "Detaylar")]
        public string Detail { get; set; }


        public string Folder { get; set; }

        [Display(Name = "Dosya Adresi")]
    
        public string Url { get; set; }

        [Display(Name = "Dosya Adı")]

        public string FileName { get; set; }

        [Display(Name = "Dosya Uzantısı")]

        public string Extension { get; set; }

        [Display(Name = "Yüklenme Zamanı")]
    
        public DateTime UploadDateTime { get; set; }

        [Display(Name = "Dosya Türü")]
     
   
        public Enum.FileType FileType { get; set; }

        [Display(Name = "Yükleyen")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [DefaultValue(true)]
        public bool Active { get; set; }

        public virtual ICollection<Slider> SliderCollection { get; set; }
        public virtual ICollection<Media> MediaCollection { get; set; }
        public virtual ICollection<Document> DocumentCollection { get; set; }


    }
}
