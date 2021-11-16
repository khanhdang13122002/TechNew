using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tech_News.Models.EF;
namespace Tech_News.Models.DAO
{
    public class NewDetailsViewModel:LeftContentViewModel
    {
        public ArticleDao article_dao = new ArticleDao();
        public Article singleArticle;
        public List<Article> relatedArticle;
        public List<Comment>comments_;
        public async Task<bool> GetData(int id = 1)
        {
            try
            {
                await incrementViewById(id);
                await GetDataL();
                singleArticle = await article_dao.GetSingleById(id);
                relatedArticle = await article_dao.GetArticleByCategory(singleArticle.category_id,4);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public async Task<bool>incrementViewById(int article_id)
        {
            try
            {
                var article = await DB.Articles.FindAsync(article_id);
                article.view.total_view += 1;
                await DB.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        
        }
        public async Task<bool> AddComment(Comment cmt)
        {
            try
            {
                DB.Comments.Add(cmt);
                await DB.SaveChangesAsync();
                return true;
               
            }
            catch(Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public List<Comment> GetChildComment(int parent_id)
        {
            var ef =  DB.Comments.Where(cmt => cmt.parent_id.Equals(parent_id)).ToList();
            return ef;
        }

        public async Task<List<Comment>> GetCommentByPage(long id,int page,int limit) {

            var ef = await DB.Comments.Where(cmt=>cmt.article_id == id && cmt.parent_id == 0 ).OrderBy(cmt => cmt.id).Skip((page - 1) * limit).Take(limit).ToListAsync();
            return ef;

        }
        public async Task<int> GetCoutComment(long articleId)
        {
            return await DB.Comments.Where(art => art.parent_id == 0 && art.article_id == articleId).CountAsync();
        }
            
    }
}