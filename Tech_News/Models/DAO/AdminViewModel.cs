using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tech_News.Models.EF;
using System.IO;
namespace Tech_News.Models.DAO
 
{
    public class  AdminViewModel:BaseDao
    {
        public int Total_Articles;
        public int Total_User;
        public int Total_Comment;
        public List<Article> Re_Articles;
        public List<AppUser> Re_Users;
        public List<Comment> Re_Comments;
        public AppUser single_user;
        public List<Article> All_Article;
        public List<Category> AllCategory;
        public async Task<bool> GetData()
        {
            try
            {
                Total_Articles = await DB.Articles.CountAsync();
                Total_User = await DB.Users.CountAsync();
                Total_Comment = await DB.Comments.CountAsync();
                Re_Articles = await DB.Articles.OrderByDescending(Art => Art.update_at).Take(6).ToListAsync();
                Re_Users = await DB.Users.OrderByDescending(us => us.Id).Take(6).ToListAsync();
                Re_Comments = await DB.Comments.OrderByDescending(cmt => cmt.update_at).Take(4).ToListAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> AddView(View view)
        {
            try
            {
                DB.Views.Add(view);
                await DB.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Article> GetSingleArticleById(long id)
        {
            return await DB.Articles.Where(art => art.id == id).FirstOrDefaultAsync();
        }
        public async Task<int> GetToTalArticle()
        {
            return await DB.Articles.CountAsync();
        }
        public async Task<bool> RemoveArticleById(long id)
        {
            try
            {
                var ef = await DB.Articles.FindAsync(id);
                DB.Articles.Remove(ef);
                await DB.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
          

        }
        public async Task<bool> GetAllArticle(int page,int limit = 10)
        {
            try {
                All_Article = await DB.Articles.OrderBy(art => art.id).Skip((page - 1) * limit).Take(limit).ToListAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Update(AppUser us)
        {
            AppUser ef = await DB.Users.Where(us_ => us_.Id == us.Id).FirstOrDefaultAsync();
            if (ef != null)
            {
                ef.Email = us.Email;
                ef.UserName = us.UserName;
                if (us.avatar != null) { ef.avatar = us.avatar; }
                ef.PhoneNumber = us.PhoneNumber;
                await DB.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> GetSingle(string id)
        {
            try
            {
                single_user = await DB.Users.Where(us => us.Id == id).FirstOrDefaultAsync();

                return true;

            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> GetAllCategory()
        {
            try
            {
                AllCategory = await DB.Categories.ToListAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> AddViewsToArtcile()
        {
            try
            {
                View efView = new View();
                efView.total_view = 0;
                efView.update_at = DateTime.Now;
                efView.create_at = DateTime.Now;
                DB.Views.Add(efView);
                await DB.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Author> CreateAuthor(string user_id)
        {
            try
            {
                Author auth = new Author
                {
                    create_at = DateTime.Now,
                    update_at = DateTime.Now,

                };
                var efUser = DB.Users.Find(user_id);
                auth.user = efUser;
                auth.author_name = efUser.UserName;
                DB.Authors.Add(auth);
                await DB.SaveChangesAsync();
                return auth;
            }
            catch
            {
                return null;
            }
        }
        public async Task<long> GetAuthorIdByUserId(string id)
        {
            var ef = await DB.Authors.Where(author => author.user.Id == id).FirstOrDefaultAsync();
            if (ef == null)
            {
                ef = await CreateAuthor(id);
            }
            return ef.id;

        }
        public async Task<long> GetLastViewId()
        {
            var efView = await DB.Views.OrderByDescending(v => v.create_at).FirstOrDefaultAsync();
            return efView.id;
        }
        public async Task<bool> AddArticle(Article article,string id)
        {
            try
            {
                
                // add view
                bool checkAddView = await AddViewsToArtcile();
                if (checkAddView)
                {
                    long authorId = await GetAuthorIdByUserId(id);
                    var efViewId = await GetLastViewId();

                    Article ef = new Article {
                        article_title = article.article_title,
                        article_content = article.article_content,
                        article_thumbnail = article.article_thumbnail,
                        category_id = article.category_id,
                        author_id = authorId,
                        view_id = efViewId,
                        create_at = DateTime.Now,
                        update_at = DateTime.Now,
                    };
                    
                    DB.Articles.Add(ef);
                    await DB.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}