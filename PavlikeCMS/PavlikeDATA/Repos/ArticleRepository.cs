using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PavlikeDATA.Models;
using Enum = pavlikeLibrary.Enum;

namespace PavlikeDATA.Repos
{
    public class ArticleRepository
    {
        readonly Context _db = new Context();
        public List<Article> GetAll(int? typeId)
        {
            return typeId == null? _db.Articles.Where(c => c.Active).OrderBy(c => c.Title).Include(c=>c.View).Include(c => c.Media).Include(c => c.Author).Include(c => c.ArticleType).Include(c => c.Page).ToList() : _db.Articles.Where(c => c.Active && c.ArticleTypeId == typeId).OrderBy(c => c.Title).Include(c => c.Media).Include(c => c.Author).Include(c => c.ArticleType).Include(c => c.Page).ToList();
        }

        public List<ArticleType> Types()
        {
            return _db.ArticleTypes.ToList();
        }

        public Enum.EntityResult Create(Article article)
        {
            try
            {
                article.Active = true;
                _db.Articles.Add(article);
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception e)
            {
                return Enum.EntityResult.Failed;
            }
        }

        public Article FindbyId(int id)
        {
            return _db.Articles.Include(c=> c.View).Include(c=>c.Author).SingleOrDefault(c => c.Id == id);

        }

        public Enum.EntityResult Update(Article modified)
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

        public Enum.EntityResult Disable(Article disable)
        {
            disable.Active = false;
            return Update(disable);
        }

        public Enum.EntityResult Delete(Article delete)
        {
            try
            {
                _db.Articles.Remove(delete);
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

        public List<ArticleType> GetAllTypes()
        {
            return _db.ArticleTypes.ToList();
        }

        public Enum.EntityResult CreateType(ArticleType model)
        {
            try
            {
                _db.ArticleTypes.Add(model);
                _db.SaveChanges();
                return Enum.EntityResult.Success;
            }
            catch (Exception e)
            {
                return Enum.EntityResult.Failed;
            }
        }

        public ArticleType FindTypeById(int id)
        {
            return _db.ArticleTypes.SingleOrDefault(c => c.Id == id);
        }

        public Enum.EntityResult DisableType(int id)
        {
            try
            {
                var disable = FindTypeById(id);
                if (disable == null) return Enum.EntityResult.Failed;
                disable.Active = false;
                _db.Entry(disable).State = EntityState.Modified;
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
