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
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        protected AdminViewModel model  = new AdminViewModel();
        protected ApplicationUserManager _userManager;
        // GET: Admin
        public async  Task<ActionResult> Index()
        {
            ViewBag.user_id = GetId();
            bool us = await model.GetSingle(ViewBag.user_id);
            bool check = await model.GetData();
            if (check && us)
            {
                return View(model);
            }
            return View();
        }
        
        
        public async Task<ActionResult> Profiles() {
            ViewBag.user_id = GetId();
            bool check = await model.GetSingle(ViewBag.user_id);
            if (check)
            {
            return View(model);

            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Profiles(AppUser us)
        {
            
            return View();
        }
        public ActionResult Fonts()
        {
            return View( );
        }

        public async Task<ActionResult> Articles()
        {
            ViewBag.user_id = GetId();
            bool check = await model.GetSingle(ViewBag.user_id);

            return View(model);
        }

        public ActionResult BlankPage()
        {
            return View( );
        }

        public ActionResult GoogleMap()
        {
            return View( );
        }
    }
}