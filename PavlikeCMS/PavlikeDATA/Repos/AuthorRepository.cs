using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PavlikeDATA.Models;
using Enum = pavlikeLibrary.Enum;

namespace PavlikeDATA.Repos
{
    public class AuthorRepository
    {
        readonly Context _db = new Context();
  
        public List<Author> GetAll()
        {
            return _db.Authors.ToList();
        }

        public Author FindbyId(int id)
        {
            return _db.Authors.SingleOrDefault(c => c.Id == id);
        }


        public Enum.EntityResult Create(Author authormodel)
        {
            try
            {
                _db.Authors.Add(authormodel);
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception e)
            {
                return Enum.EntityResult.Failed;
            }
        }
        public Enum.EntityResult Update(Author modified)
        {
            try
            {
                _db.Entry(modified).State = EntityState.Modified;
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception)
            {
                return Enum.EntityResult.Failed;
            }
        }

        public Enum.EntityResult Disable(Author disable)
        {
            disable.Active = false;
            return Update(disable);
        }

        public Enum.EntityResult Delete(Author delete)
        {
            try
            {
                _db.Authors.Remove(delete);
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception)
            {
                return Enum.EntityResult.Failed;
            }
        }
        public Enum.EntityResult FindbyIdandDisable(int id)
        {
            var disableitem = FindbyId(id);
            return disableitem == null ? Enum.EntityResult.Failed : Disable(disableitem);
        }
    }
}

