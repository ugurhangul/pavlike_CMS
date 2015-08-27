using System;
using System.Data.Entity;
using System.Linq;
using PavlikeDATA.Models;
using Enum = pavlikeLibrary.Enum;

namespace PavlikeDATA.Repos
{
    public class SettingRepository
    {
        private readonly Context _db = new Context();

        public class GoogleMapSetting
        {
            public string Title { get; set; }
            public bool Active { get; set; }
            public string Content { get; set; }
            public int Latitude { get; set; }
            public int Longitude { get; set; }
        }
        public class SiteSetting
        {

            public string Title { get; set; }
            public string Description { get; set; }
            public string MetaTags { get; set; }
            public string Url { get; set; }
            public string AdminEmail { get; set; }
            public string LogoUrl { get; set; }
            public string LogoAltText { get; set; }

        }
        public class SliderSetting
        {
            public int? Width { get; set; }
            public int? Height { get; set; }
        }
        public class ContactSetting
        {
            public bool ContactForm { get; set; }
        }
        public class MailSetting
        {
            public string Server { get; set; }

            public int Port { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public string DisplayName { get; set; }

            public bool Ssl { get; set; }
        }


        public Settings GetAllSettings()
        {
            return _db.Settings.FirstOrDefault();
        }
        public SiteSetting SiteSettings()
        {
            return (from x in _db.Settings select new SiteSetting { Title = x.Title, Description = x.Description, MetaTags = x.MetaTags, Url = x.Url, AdminEmail = x.AdminEmail, LogoAltText = x.Logo.AltText, LogoUrl = x.Logo.File.Url }).FirstOrDefault();

        }
        public ContactSetting ContactSettings()
        {
            return (from c in _db.Settings select new ContactSetting { ContactForm = c.ContactForm }).FirstOrDefault();
        }
        public GoogleMapSetting GoogleMapSettings()
        {
            return (from x in _db.Settings select new GoogleMapSetting { Title = x.Title, Active = x.GoogleMap, Content = x.GoogleMapContent, Latitude = x.GoogleMaplat, Longitude = x.GoogleMaplng }).FirstOrDefault();
        }
        public SliderSetting SliderSettings()
        {
            return (from x in _db.Settings select new SliderSetting { Width = x.SliderWidht, Height = x.SliderHeight }).FirstOrDefault();
        }
        public MailSetting MailSettings()
        {
            return (from x in _db.Settings select new MailSetting { Server = x.MailServer, Port = x.MailPort, Email = x.SenderEMail, Password = x.SenderPassword, DisplayName = x.SenderDisplayName, Ssl = x.MailServerSsl }).FirstOrDefault();

        }

        public Enum.EntityResult Update(Settings modified)
        {
            try
            {
                _db.Entry(modified).State = EntityState.Modified;
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception e)
            {
                return Enum.EntityResult.Failed;
            }

        }

    }
}
