using System;
using System.Collections.Generic;
using System.Linq;
using PavlikeDATA.Models;
using Enum = pavlikeLibrary.Enum;

namespace PavlikeDATA.Repos
{
    public class ThemeOptionsRepository
    {
        readonly Context _db = new Context();

        public List<View> GetViews()
        {
            return _db.Views.ToList();
        }

        public Enum.EntityResult RegisterView(View model)
        {
            try
            {
                _db.Views.Add(model);
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception e)
            {
                return Enum.EntityResult.Failed;
            }
        }

        public Enum.EntityResult DeleteView(int id)
        {
            try
            {
                _db.Views.Remove(_db.Views.SingleOrDefault(c => c.Id == id));
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception r)
            {
                return Enum.EntityResult.Failed;
            }
        }
    }
}
