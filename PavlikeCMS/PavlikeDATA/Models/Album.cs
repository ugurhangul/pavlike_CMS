using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PavlikeDATA.Models
{
    public class Album
    {
        public int Id { get; set; }
        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "*Gerekli Alan.")]
        public string Title { get; set; }
        
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        [DefaultValue(true)]
        public bool Active { get; set; }
        public virtual ICollection<AlbumMedia> AlbumMediaCollection { get; set; }

    }
}
