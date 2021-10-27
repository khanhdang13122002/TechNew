using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tech_News.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        /*protected AdminViewModel   = new AdminViewModel();*/
        // GET: Admin
       /* [Authorize(Roles = "admin")]*/
        public ActionResult Index()
        {
            AuthorizeAttribute demo = new AuthorizeAttribute();
            ViewBag.demo = demo.Roles;
            return View();
        }
        

        public ActionResult Profiles() {
            return View( );
        }

        public ActionResult Fonts()
        {
            return View( );
        }

        public ActionResult Articles()
        {
            return View( );
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