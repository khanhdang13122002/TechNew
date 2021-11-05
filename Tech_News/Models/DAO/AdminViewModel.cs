using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tech_News.Models.EF;
namespace Tech_News.Models.DAO
{
    public class AdminViewModel:BaseDao
    {
     
        public int Total_Articles;
        public int Total_User;
        public int Total_Comment;
        public List<Article> Re_Articles;
        public List<AppUser> Re_Users;
        public List<Comment> Re_Comments;
        public AppUser single_user;
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
        
    }
}