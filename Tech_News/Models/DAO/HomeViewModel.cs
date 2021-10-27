using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tech_News.Models.EF;
namespace Tech_News.Models.DAO
{
    public class HomeViewModel:LeftContentViewModel
    {
        protected ArticleDao articleDao = new ArticleDao();
        protected CategoryDao categoryDao = new CategoryDao();
        public List<Article> MobileArticle;
        public List<Article> TopThreeArticle;
        public List<Article> TabletArticle;
        public List<Article> GragetsArticle;
        public List<Article> CameraArticle;
        public List<Article> DesignnArticle;
        //leftContent

        public async Task<bool> GetData()
        {
            try
            {
                MobileArticle = await articleDao.GetArticleByCategory(1,5);
                TopThreeArticle = await articleDao.GetTopArticles();
                TabletArticle = await articleDao.GetArticleByCategory(4, 2);
                GragetsArticle = await articleDao.GetArticleByCategory(5, 4);
                CameraArticle = await articleDao.GetArticleByCategory(3, 3);
                DesignnArticle = await articleDao.GetArticleByCategory(6, 4);
                await GetDataL();
                return true;
            }
            catch
            {
                return false;

            }
        }

        
    }
}