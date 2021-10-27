using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tech_News.Models.DAO;
using Tech_News.Models.EF;

namespace Tech_News.Controllers
{
    
    public class NewDetailsController : BaseController
    {
        protected PageList pl = new PageList();
        protected NewDetailsViewModel model = new NewDetailsViewModel();
        // GET: NewDetails
        public async Task<ActionResult> Index(int id = 1, int cmt_page  = 1 , int cmt_limit = 2)
        {
            ViewBag.categories = await GetCategories();
            bool check = await model.GetData(id);
            ViewBag.user_id = GetId();
            ViewBag.article_id = model.singleArticle.id;
            ViewBag.comments = await model.GetCommentByPage(model.singleArticle.id,cmt_page, cmt_limit);
            ViewBag.page_cout = pl.GetPageInfo(cmt_limit,await model.GetCoutComment(model.singleArticle.id));
            if (check)
            {
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult>AddComment(Comment cmt)
        {
            if (Request.IsAjaxRequest())
            {
                if (cmt != null)
                {

                    Comment cmt_ = new Comment()
                    {
                        article_id = cmt.article_id,
                        comment_content = cmt.comment_content,
                        user_id = cmt.user_id,
                        parent_id = cmt.parent_id,
                        update_at = DateTime.Now,
                        create_at = DateTime.Now
                    };
                    var result = await model.AddComment(cmt_);
                    if (result)
                    {
                        RedirectToAction("Index","NewDetails",new { id = cmt.article_id});
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);

        }

        [ChildActionOnly]
        public ActionResult GetChildComments(int parent_id)
        {
            var data =  model.GetChildComment(parent_id);
            ViewBag.cmt = data;
            if (data != null)
            {
                return PartialView("ChildComments");

            }
            return PartialView("ChildComments");
        }
        
    }
}