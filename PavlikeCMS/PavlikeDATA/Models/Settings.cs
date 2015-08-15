
using System.ComponentModel.DataAnnotations;

namespace PavlikeDATA.Models
{
    public class Settings
    {
        public int Id { get; set; }
        [Display(Name = "Site Başlığı")]
        public string Title { get; set; }
        [Display(Name = "Site Detayı")]
        public string Description { get; set; }
        [Display(Name = "Etiketler")]
        public string MetaTags { get; set; }
        [Display(Name = "Site Adresi")]
        [DataType(DataType.Url)]
        public string Url { get; set; }
        [Display(Name = "Yönetici E-Posta")]
        public string AdminEmail { get; set; }
        [Display(Name = "Sunucu")]
        public string MailServer { get; set; }
        [Display(Name = "Sunucu Portu")]
        public int MailPort { get; set; }
        [Display(Name = "Gönderici E-Posta")]
        public string SenderEMail { get; set; }
        [Display(Name = "Gönderici Parola")]
        public string SenderPassword { get; set; }
        [Display(Name = "Gönderici Başlığı")]
        public string SenderDisplayName { get; set; }
        [Display(Name = "Sunucu SSL Etkinleştir")]
        public bool MailServerSsl { get; set; }
        [Display(Name = "Site Logosu")]
        public int? LogoId { get; set; }
        public virtual Media Logo { get; set; }
        [Display(Name = "Görsel Yüksekliği")]
        public int? SliderHeight { get; set; }
        [Display(Name = "Görsel Genişliği")]
        public int? SliderWidht { get; set; }

    }
}
