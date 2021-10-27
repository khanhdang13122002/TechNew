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
    public class SearchController : BaseController
    {
        // GET: Search
        protected PageList pl = new PageList();

        protected SearchViewModel model = new SearchViewModel();
        [HttpGet]
        public async Task<ActionResult> Index(string txtKeyWord = "",int page = 1 ,int limit = 6)
        {
            ViewBag.key = txtKeyWord;
            ViewBag.categories = await GetCategories();
            ViewBag.current_page = page;
            if (!string.IsNullOrEmpty(txtKeyWord))
            {
                if (await model.GetData(txtKeyWord,page))
                {
                    ViewBag.page_count = pl.GetPageInfo(limit, model.count);
                    return View(model);
                }
            }
            return View();
        }
    }
}