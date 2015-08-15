using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PavlikeDATA.Models;
using Enum = pavlikeLibrary.Enum;

namespace PavlikeDATA.Repos
{
    public class MediaRepository
    {

        readonly Context _db = new Context();
        public List<Media> GetAll()
        {
           return _db.Medias.Where(c => c.Active).OrderBy(c => c.Title).Include(c => c.Author).Include(c => c.File).ToList();
        }

        public int Count()
        {
            return _db.Medias.Count(c => c.Active);
        }

        public Enum.EntityResult Create(Media media)
        {
            try
            {
                media.Active = true;
                _db.Medias.Add(media);
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception r)
            {
                return Enum.EntityResult.Failed;
            }

        }

        public Media FindbyId(int? id)
        {
            return id == null ? null : _db.Medias.Include(c => c.Author).Include(c => c.File).SingleOrDefault(c => c.Id == id);
        }

        public Enum.EntityResult Update(Media modified)
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

        public Enum.EntityResult Disable(Media disable)
        {
            disable.Active = false;
            return Update(disable);

        }

        public Enum.EntityResult Delete(Media delete)
        {
            try
            {
                _db.Medias.Remove(delete);
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
