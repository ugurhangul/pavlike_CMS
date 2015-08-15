using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PavlikeDATA.Models;
using Enum = pavlikeLibrary.Enum;

namespace PavlikeDATA.Repos
{
    public class FileRepository
    {
        readonly Context _db = new Context();
        public List<File> GetAll()
        {
            return _db.Files.Where(c => c.Active).OrderBy(c => c.Title).Include(c => c.Author).Include(c => c.FileType).ToList();
        }

        public Enum.EntityResult Create(File file)
        {
            try
            {
                file.Active = true;

                _db.Files.Add(file);
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception)
            {
                return Enum.EntityResult.Failed;
            }
        }

        public File FindbyId(int id)
        {
            return _db.Files.SingleOrDefault(c => c.Id == id);
        }

        public Enum.EntityResult Update(File modified)
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

        public Enum.EntityResult Disable(File disable)
        {
            disable.Active = false;
            return Update(disable);

        }

        public Enum.EntityResult Delete(File delete)
        {
            try
            {
                _db.Files.Remove(delete);
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
