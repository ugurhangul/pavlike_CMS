using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PavlikeDATA.Models
{
    public class ArticleType
    {
        public int Id { get; set; }
        [Display(Name = "Tip")]
        public string Title { get; set; }
        [DefaultValue(true)]
        public bool Active { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public virtual ICollection<Article> ArticleCollection { get; set; }

    }
}
