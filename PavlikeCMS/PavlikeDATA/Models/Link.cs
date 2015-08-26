using System.ComponentModel.DataAnnotations;

namespace PavlikeDATA.Models
{
    public class Link
    {
        public int Id { get; set; }
        [Display(Name = "Site Adresi")]
        [DataType(DataType.Url)]
        public string Url { get; set; }
        [Display(Name = "Site Başlığı")]
        public string Title { get; set; }
        public bool Active { get; set; }
    }
}
