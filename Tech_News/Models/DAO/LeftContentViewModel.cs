using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tech_News.Models.EF;

namespace Tech_News.Models.DAO
{
    public class LeftContentViewModel:BaseDao
    {
        public ArticleDao artDao = new ArticleDao();
        public List<Article> popularArticle;
        public List<Article> ReviewArticle;
        public List<Article> recomendArticle;
        public async Task<bool> GetDataL() {

            try
            {
                popularArticle = await DB.Articles.OrderByDescending(art => art.view.total_view).Take(4).ToListAsync();
                ReviewArticle = popularArticle;
                recomendArticle = await DB.Articles.OrderByDescending(art => art.comments.Count()).Take(4).ToListAsync();
                return true;
            }
            catch
            {
                return false;
            } 

        }
    }
}