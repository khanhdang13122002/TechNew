using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tech_News.Models.EF;

namespace Tech_News.Models.DAO
{
    public class ArticleDao : BaseDao, IArticleDao
    {
        public Task<bool> Add(Article entity)
        {
            var my_article = DB.Articles.ToList();
            
            throw new NotImplementedException();
        }

        public Task<List<Article>> GetALL(Article entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Article>> GetByKeyWordAndPage(string keyword,int page,int limit)
        {
            return await DB.Articles.Where(art => art.article_title.Contains(keyword) || art.category.category_name.Contains(keyword)).OrderBy(art => art.id).Skip((page - 1) * limit).Take(limit).ToListAsync();
        }

        public Task<List<Article>> GetByPages(int currentPage, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Article> GetSingleById(int id)
        {
            return DB.Articles.FindAsync(id);
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Article entity)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Article>> GetArticleByCategory(int category_id,int limit)
        {
            return await DB.Articles.Where(art => art.category.id == category_id).OrderBy(art =>art.category.id).Take(limit).ToListAsync();

        }
        public async Task<List<Article>> GetTopArticles()
        {
            return await DB.Articles.OrderByDescending(art => art.view.total_view).Take(3).ToListAsync();
        }

        public Task<List<Article>> GetByKeyWord(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}