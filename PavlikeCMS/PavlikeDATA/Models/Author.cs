using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PavlikeDATA.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string UserGuid { get; set; }
        public string RoleGuid { get; set; }
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Soyadı")]
        public string Surname { get; set; }
        [Display(Name = "Doğum Tarihi")]
        public DateTime? DateofBirth { get; set; }
        [Display(Name = "E-Posta")]
        public string EMail { get; set; }
        [Display(Name = "Fotoğraf")]
        public int? PictureId { get; set; }
        public Media Picture { get; set; }
        [Display(Name = "Aktif")]
        public bool Active { get; set; }
        public virtual ICollection<Page> PageCollection { get; set; }
        public virtual ICollection<Article> ArticleCollection { get; set; }
        public virtual ICollection<File> FileCollection { get; set; }
        public virtual ICollection<Album> AlbumCollection { get; set; }
        public virtual ICollection<Media> MediaCollection { get; set; }

    }
}
