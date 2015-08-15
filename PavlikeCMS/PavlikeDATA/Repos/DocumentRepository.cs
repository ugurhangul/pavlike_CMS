using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PavlikeDATA.Models;
using Enum = pavlikeLibrary.Enum;

namespace PavlikeDATA.Repos
{
    public class DocumentRepository
    {
        readonly Context _db = new Context();
        public List<Document> GetAll()
        {
            return _db.Documents.Where(c => c.Active).OrderBy(c => c.Title).Include(c => c.Author).Include(c => c.File).ToList();
        }

        public int Count()
        {
            return _db.Documents.Count(c => c.Active);
        }

        public Enum.EntityResult Create(Document document)
        {
            try
            {
                document.Published = DateTime.Now;
                document.Active = true;
                _db.Documents.Add(document);
                _db.SaveChanges();

                return Enum.EntityResult.Success;
            }
            catch (Exception)
            {
                return Enum.EntityResult.Failed;
            }

        }

        public Document FindbyId(int id)
        {
            return _db.Documents.SingleOrDefault(c => c.Id == id);

        }

        public Enum.EntityResult Update(Document modified)
        {
            try
            {
                _db.Entry(modified).State = EntityState.Modified;
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception r)
            {
                return Enum.EntityResult.Failed;
            }
        }

        public Enum.EntityResult Disable(Document disable)
        {
            disable.Active = false;
            return Update(disable);

        }
        public Enum.EntityResult Delete(Document delete)
        {
            try
            {
                _db.Documents.Remove(delete);
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
