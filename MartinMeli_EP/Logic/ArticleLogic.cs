using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class ArticleLogic: Connection
    {
        public bool AddArticle(article a)
        {
            if (a.authorId!=-1)
            {
                a.journalist = db.journalists.SingleOrDefault(n => n.id == a.authorId);

                db.articles.Add(a);
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public article GetBreakingNews()
        {
            return db.articles.OrderByDescending(a => a.articleId).FirstOrDefault();
        }

        public List<article> Get5MostRecentArticles()
        {
            List<article> allArticles = db.articles.OrderByDescending(a => a.articleId).ToList();
            return allArticles.Take(5).ToList();
        }
        
        public List<article> Get5MostRecentArticles(int categoryId)
        {
            List<article> allArticles = db.articles.OrderByDescending(a => a.categoryId == categoryId).ToList();
            return allArticles.Take(5).ToList();
        }

        public article GetArticleByID(int articleId)
        {
            return db.articles.SingleOrDefault(a => a.articleId == articleId);
        }

        public List<article> GetArticlesByCategory(int categoryId)
        {
            return db.articles.Where(a => a.categoryId == categoryId).ToList();
        }

        public List<article> GetArticlesByJournalist(int authorId)
        {
            return db.articles.Where(a => a.authorId == authorId).ToList();
        }

        public void DeleteJournal(int journalId)
        {
            article toRemove = GetArticleByID(journalId);
            db.articles.Remove(toRemove);
            db.SaveChanges();
        }

        public void UpdateJournal(int id, article a)
        {
            article original = db.articles.FirstOrDefault(ar => ar.articleId == id);

            original.categoryId = a.categoryId;
            original.description = a.description;
            original.headline = a.headline;

            db.Entry(original).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
