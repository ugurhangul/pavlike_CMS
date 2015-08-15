using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PavlikeDATA.Models;
using Enum = pavlikeLibrary.Enum;

namespace PavlikeDATA.Repos
{
    public class AlbumMediaRepository
    {
        readonly Context _db = new Context();
        public List<AlbumMedia> GetAll()
        {
            return _db.AlbumMedias.Include(c => c.Album).Include(c => c.Media).ToList();
        }
        public List<AlbumMedia> GetAllbyAlbumId(int albumId)
        {
            return _db.AlbumMedias.Where(c => c.AlbumId == albumId).Include(c => c.Album).Include(c => c.Media).ToList();
        }
        public List<AlbumMedia> GetAllbyMediaId(int mediaId)
        {
            return _db.AlbumMedias.Where(c => c.MediaId == mediaId).Include(c => c.Album).Include(c => c.Media).ToList();
        }

        public int Count()
        {
            return _db.AlbumMedias.Count();
        }
        public int CountbyAlbumId(int albumId)
        {
            return _db.AlbumMedias.Count(c => c.AlbumId == albumId);
        }

        public Enum.EntityResult Create(AlbumMedia albumMedia)
        {
            try
            {
                _db.AlbumMedias.Add(albumMedia);
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception)
            {
                return Enum.EntityResult.Failed;
            }
       }

        public AlbumMedia FindbyId(int id)
        {

            _db.SaveChanges();
            return _db.AlbumMedias.SingleOrDefault(c => c.Id == id);

        }
        public AlbumMedia FindbyAlbumandMediaId(int mediaId, int albumId)
        {

            _db.SaveChanges();
            return _db.AlbumMedias.SingleOrDefault(c => c.MediaId == mediaId && c.AlbumId == albumId);

        }

        public Enum.EntityResult DeleteAllbyMediaId(int mediaId)
        {
            var willDeletedList = GetAllbyMediaId(mediaId);
            var errorCount = willDeletedList.Count(item => Delete(item) == Enum.EntityResult.Failed);
            return errorCount == 0 ? Enum.EntityResult.Success : Enum.EntityResult.Failed;
        }

        public Enum.EntityResult FindandDelete(int mediaId, int albumId)
        {
            var willDeleted = FindbyAlbumandMediaId(mediaId, albumId);
            return willDeleted == null ? Enum.EntityResult.Failed : Delete(willDeleted);
        }

        public Enum.EntityResult Delete(AlbumMedia delete)
        {
            try
            {
                _db.AlbumMedias.Remove(delete);
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception)
            {
                return Enum.EntityResult.Failed;
            }
        }
    }
}
