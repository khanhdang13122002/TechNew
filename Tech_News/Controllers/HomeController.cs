using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tech_News.Models.DAO;
namespace Tech_News.Controllers
{
    public class HomeController : BaseController
    {
        protected HomeViewModel model = new HomeViewModel();
        public async Task<ActionResult> Index()
        {
            ViewBag.categories = await GetCategories();
            ViewBag.page = "home";
            bool check = await model.GetData();
            if (check)
            {
                return View(model);

            }
            return View();
        }
        public  JsonResult GetAritcleByCategoryId()
        {
            return  Json(null, JsonRequestBehavior.AllowGet);
        }

    }

}