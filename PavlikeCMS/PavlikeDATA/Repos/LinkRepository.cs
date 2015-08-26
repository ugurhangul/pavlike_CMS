using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PavlikeDATA.Models;
using Enum = pavlikeLibrary.Enum;

namespace PavlikeDATA.Repos
{
    public class LinkRepository
    {
        readonly Context _db = new Context();
        public List<Link> GetAll()
        {
            return _db.Links.Where(c=> c.Active).ToList();
        }

        public Enum.EntityResult Create(Link link)
        {
            try
            {
                link.Active = true;
                _db.Links.Add(link);
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception e)
            {
                return Enum.EntityResult.Failed;
            }
        }

        public Link FindbyId(int id)
        {
            return _db.Links.SingleOrDefault(c => c.Id == id);

        }

        public Enum.EntityResult Update(Link modified)
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

        public Enum.EntityResult Disable(Link disable)
        {
            disable.Active = false;
            return Update(disable);
        }

        public Enum.EntityResult Delete(Link delete)
        {
            try
            {
                _db.Links.Remove(delete);
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception e)
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