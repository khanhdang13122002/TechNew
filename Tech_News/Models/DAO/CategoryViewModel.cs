using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tech_News.Models.EF;

namespace Tech_News.Models.DAO
{
    public class CategoryViewModel:LeftContentViewModel
    {
        protected List<Article> ArticleOfCategory;
        protected CategoryDao category_dao = new CategoryDao();
        public List<Article> ArticleByPage;
        public async Task<bool> GetData(int id,int page,int limit)
        {
            try
            {
                await GetDataL();
                ArticleByPage = await category_dao.GetArticleByIdAndPage(id,page,limit);
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> GetTotalArticleByCategory(int id) {
            return await DB.Articles.Where(art=>art.category_id == id).CountAsync();
        }
    }
}