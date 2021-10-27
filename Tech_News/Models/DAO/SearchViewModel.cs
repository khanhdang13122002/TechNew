using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tech_News.Models.EF;

namespace Tech_News.Models.DAO
{
    public class SearchViewModel:LeftContentViewModel
    {
        public ArticleDao article_dao = new ArticleDao();
        public List<Article> ArticleByKeyword;
        public int count = 0;
        public async Task<bool> GetData(string keyword, int page = 1)
        {
            try
            {
                await GetDataL();
                ArticleByKeyword = await article_dao.GetByKeyWordAndPage(keyword,page,6);
                count = getArticleCount(keyword);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public int getArticleCount(string keyword)
        {
            return  DB.Articles.Where(art => art.article_title.Contains(keyword) || art.category.category_name.Contains(keyword)).Count();

        }

    }
}