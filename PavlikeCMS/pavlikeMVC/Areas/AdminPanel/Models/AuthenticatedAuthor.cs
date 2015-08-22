using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using PavlikeDATA.Models;

namespace pavlikeMVC.Areas.AdminPanel.Models
{
    public class AuthenticatedAuthor
    {

        private readonly Context _db = new Context();
        readonly string _userid = HttpContext.Current.User.Identity.GetUserId();
        public Author Author;
        public int Id;
        public string Picture;
        public string Name;
        public string Surname;

        public AuthenticatedAuthor()
        {
            Author = _db.Authors.SingleOrDefault(c => c.UserGuid == _userid);
            if (Author == null) return;
            if (Author.Picture != null) Picture = Author.Picture.File.Url;
            Id = Author.Id;
            Name = Author.Name;
            Surname = Author.Surname;
        }
    }
}