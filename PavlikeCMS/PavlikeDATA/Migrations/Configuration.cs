using System;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using PavlikeDATA.Models;

namespace PavlikeDATA.Migrations
{
    public class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "PavlikeDATA.Models.Context";
       
        }

        public void Seed(Context context)
        {
            ApplicationDbContext.Create();
            var usercontext = new ApplicationDbContext();
            var passwordHash = new PasswordHasher();
            var user = new ApplicationUser
            {
                UserName = "SuperUser",
                PasswordHash = passwordHash.HashPassword("pavlikeCMS"),
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = "ugurhangul@sapmazbilisim.com",
                EmailConfirmed = true
            };
            usercontext.Users.AddOrUpdate(c=>c.UserName,user);
            usercontext.SaveChanges();
            context.Authors.AddOrUpdate(c => c.Name, new Author
            {
                UserGuid = user.Id,
                Name = "SuperUser",
                EMail = user.Email,
                DateofBirth = DateTime.Now,
                Active = true
            });
            context.Settings.AddOrUpdate(c => c.Url, new Models.Settings
            {
                Title = "PavlikeCMS",
                Description = "pavlikeCMS - Asp.Net MVC için açık kaynaklı içerik yönetim sistemi",
                MetaTags = "pavlike,CMS,Asp,Asp.net,MVC,için,açık,kaynak,içerik,yönetim,sistemi",
                Url = "pavlikeCMS.com",
                AdminEmail = "cms@sitem.com",
                MailServer = "mail.sitem.com",
                MailPort = 587,
                SenderEMail = "bilgi@sitem.com",
                SenderPassword = "epostasifrem",
                SenderDisplayName = "Benim güzel sitem",
                MailServerSsl = false,
                SliderHeight = 900,
                SliderWidht = 1440
                
            });

            context.SaveChanges();
        }
    }
}
