using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PavlikeDATA.Models;
using Enum = pavlikeLibrary.Enum;

namespace PavlikeDATA.Repos
{
    public class SliderRepository
    {

        readonly Context _db = new Context();
        public List<Slider> GetAll()
        {
            return _db.Sliders.Where(c => c.Active).OrderBy(c => c.Title).Include(c => c.File).ToList();
        }

        public Enum.EntityResult Create(Slider slider)
        {
            try
            {
                slider.Active = true;
                _db.Sliders.Add(slider);
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception)
            {
                return Enum.EntityResult.Failed;
            }
        }

        public Slider FindbyId(int id)
        {

            _db.SaveChanges();
            return _db.Sliders.SingleOrDefault(c => c.Id == id);

        }

        public Enum.EntityResult Update(Slider modified)
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

        public Enum.EntityResult Disable(Slider disable)
        {
            disable.Active = false;
            return Update(disable);
        }

        public Enum.EntityResult Delete(Slider delete)
        {
            try
            {
                _db.Sliders.Remove(delete);
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
