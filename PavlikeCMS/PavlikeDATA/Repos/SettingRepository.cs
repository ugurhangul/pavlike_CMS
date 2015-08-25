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

        public class GoogleMapSetting
        {
            public bool Active { get; set; }
            public string Content { get; set; }
            public int Latitude { get; set; }
            public int Longitude { get; set; }
        }
        public class SliderSetting
        {
            public int? Width { get; set; }
            public int? Height { get; set; }
        }

        public Settings GetSettings()
        {
            return _db.Settings.FirstOrDefault();
        }

        public GoogleMapSetting GoogleMapSettings()
        {
            var result = (from x in _db.Settings select new GoogleMapSetting { Active = x.GoogleMap, Content = x.GoogleMapContent, Latitude = x.GoogleMaplat, Longitude = x.GoogleMaplng }).FirstOrDefault();
            return result;
        }

        public SliderSetting SliderSettings()
        {
            var result = (from x in _db.Settings select new SliderSetting { Width = x.SliderWidht, Height = x.SliderHeight}).FirstOrDefault();
            return result;
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
