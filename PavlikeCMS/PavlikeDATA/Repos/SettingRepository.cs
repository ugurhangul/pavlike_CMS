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

        public Settings GetSettings()
        {
            return _db.Settings.FirstOrDefault();
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
