using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tech_News.Models.EF;

namespace Tech_News.Models.DAO
{
    public class CategoryDao : BaseDao, ICategoryDao
    {

        public Task<bool> Add(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetALL(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetByKeyWord(string keyword)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetSingleById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Article>> GetArticleByIdAndPage(int id,int page,int limit)
        {
            return await DB.Articles.Where(art => art.category_id == id).OrderBy(art=>art.id).Skip((page - 1) * limit).Take(limit).ToListAsync();
        }

        public async Task<List<Category>> GetByLimit(int limit)
        {
            return await DB.Categories.OrderBy(ef => ef.id).Take(limit).ToListAsync();
        }
    }
}