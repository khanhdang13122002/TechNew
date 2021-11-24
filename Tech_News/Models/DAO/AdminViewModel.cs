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
        public async Task<int> GetTotalCategory()
        {
            return await DB.Categories.CountAsync();
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
        public async Task<bool> UpdateArticle(Article obj)
        {
            if (obj != null)
            {
                try
                {
                    var ef = await DB.Articles.FindAsync(obj.id);
                    if (ef != obj)
                    {
                        var default_thumb = "~/Public/assets/img/tech_about.jpg";
                        ef.article_content = obj.article_content;
                        ef.article_title = obj.article_title;
                        ef.category_id = obj.category_id;
                        ef.update_at = DateTime.Now;
                        if (obj.article_thumbnail != null && obj.article_thumbnail != default_thumb)
                        {
                            ef.article_thumbnail = obj.article_thumbnail;
                        }
                        await DB.SaveChangesAsync();
                        return true;
                    }
                   
                }
                catch
                {
                    return false;
                }
            }
            return false;
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
        public async Task<List<Article>> SearchArticleByKeyword(string keyword, int page)
        {
            return await DB.Articles.Where(
                ef => ef.article_title.Contains(keyword)
                ).OrderBy(ef => ef.id).Skip((page - 1) * 10).Take(10).ToListAsync();
        }
        public async Task<List<Category>> SearchCategoryByKeyword(string keyword,int page)
        {
            return await DB.Categories.Where(
                ef => ef.category_name.Contains(keyword)
                ).OrderBy(ef => ef.id).Skip((page - 1) * 10).Take(10).ToListAsync();
        }
        public async Task<bool> GetCategoryByPage(int page,int limit = 10)
        {
            try
            {
                AllCategory = await DB.Categories.OrderBy(x => x.id).Skip((page - 1) * limit).Take(limit).ToListAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> AddCategory(Category category)
        {
            try
            {
                category.update_at = DateTime.Now;
                category.create_at = DateTime.Now;
                DB.Categories.Add(category);
                await DB.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateCategory(Category category)
        {
            try
            {
                var ef = await GetCategoryById(category.id);
                if (ef != null)
                {
                    ef.category_name = category.category_name;
                    ef.update_at = DateTime.Now;
                    await DB.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> RemoveCategory(long id)
        {
            try
            {
                var ef = await DB.Articles.Where(x => x.category_id == id).ToListAsync();
                var category = await DB.Categories.FindAsync(id);
                DB.Articles.RemoveRange(ef);
                DB.Categories.Remove(category);
                await DB.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Category> GetCategoryById(long id)
        {
            return await DB.Categories.FindAsync(id);
        }
        public async Task<int> GetCountCategory()
        {
            return await DB.Categories.CountAsync();
        }
        public async Task<int> GetCountArticle()
        {
            return await DB.Articles.CountAsync();
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