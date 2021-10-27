using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tech_News.Models.DAO;
using Tech_News.Models.EF;

namespace Tech_News.Controllers
{
    public class CategoryController : BaseController
    {
        protected CategoryViewModel model = new CategoryViewModel();
        PageList pl = new PageList();
        // GET: Category
        public async Task<ActionResult> Index(int id = 1,int page = 1, int limit = 13)
        {
            ViewBag.current_page = page;
            ViewBag.page_count = pl.GetPageInfo(limit, await model.GetTotalArticleByCategory(id));
            ViewBag.categories = await GetCategories();
            bool check = await model.GetData(id,page,limit);
            ViewBag.page = "category";
            if (check)
            {
                return View(model);
            }
            return View();
        }
    }
}