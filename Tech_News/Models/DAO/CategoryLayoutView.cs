using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tech_News.Models.EF;
namespace Tech_News.Models.DAO
{
    public class CategoryLayoutView
    {
        protected CategoryDao categoryDao = new CategoryDao();
        public List<Category> categories;
        public async Task<List<Category>> GetCateGories()
        {
            categories = await categoryDao.GetByLimit(5);
            return categories;
        }
    }
}