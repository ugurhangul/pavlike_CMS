using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PavlikeDATA.Models;
using Enum = pavlikeLibrary.Enum;

namespace PavlikeDATA.Repos
{
    public class PageRepository
    {
        readonly Context _db = new Context();
        public List<Page> GetAll()
        {
            return _db.Pages.Where(c => c.Active).Include(c => c.Author).Include(c => c.RootPage).OrderBy(c => c.PageOrder).ToList();
            }

        public Enum.EntityResult Create(Page page)
        {
            try
            {
                page.Active = true;
                _db.Pages.Add(page);
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception e)
            {
                return Enum.EntityResult.Failed;
            }
        }

        public Page FindbyId(int id)
        {
            return _db.Pages.SingleOrDefault(c => c.Id == id);

        }

        public Enum.EntityResult Update(Page modified)
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

        public Enum.EntityResult Disable(Page disable)
        {
            disable.Active = false;
            return Update(disable);

        }

        public Enum.EntityResult Delete(Page delete)
        {
            try
            {
                _db.Pages.Remove(delete); _db.SaveChanges();
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
