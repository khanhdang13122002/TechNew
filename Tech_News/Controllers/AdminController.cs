using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tech_News.Models.DAO;
using Tech_News.Models.EF;
using System.IO;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Script.Serialization;

namespace Tech_News.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        PageList pl = new PageList();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        protected AdminViewModel model  = new AdminViewModel();
        public AdminController()
        {

        }
        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

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
        public async Task<ActionResult> Profiles(HttpPostedFileBase uploadImages,string user_id,string txt_username,string txt_email,string txt_current_password,string txt_new_password,string txt_phone_num)
        {
            try
            {
                AppUser us = new AppUser();
                await model.GetSingle(user_id);
                if (uploadImages != null)
                {
                    string FileName = txt_username;
                    string extension = Path.GetExtension(uploadImages.FileName);
                    FileName = FileName + "_" +  DateTime.Now.ToString("yymmssff") + extension;
                    us.avatar = "~/Public/Admin/plugins/images/users/" + FileName;
                    FileName = Path.Combine(Server.MapPath("~/Public/Admin/plugins/images/users/"), FileName);
                    uploadImages.SaveAs(FileName);
                // change infor
                //update 
                }
                us.Id = user_id;
                us.UserName = txt_username;
                us.Email = txt_email;
                us.PhoneNumber = txt_phone_num;
                if (!string.IsNullOrEmpty(txt_current_password)&&!string.IsNullOrEmpty(txt_new_password))
                {
                    if((txt_current_password == txt_new_password)) {
                        ViewBag.passwordErr = "Old Password can't equal New Password";
                        return View();
                    }

                    bool checkPass = await UserManager.CheckPasswordAsync(model.single_user, txt_current_password);
                    if (checkPass)
                    {
                        var checkChange = await UserManager.ChangePasswordAsync(user_id, txt_current_password, txt_new_password);
                        if (checkChange.Succeeded)
                        {
                            ViewBag.changed = "Password has been changed";
                        }
                        else
                        {
                            ViewBag.changed = "Can't change your password";
                        }
                    }
                    else
                    {
                        ViewBag.passwordErr = "Current Password is not correct";
                    }
                }
                bool check = await model.Update(us);
                if (check)
                {
                    await model.GetSingle(user_id);
                    return View(model);
                }
            }
            catch
            {
                return View();
            }
            return View();
        }
       

        public async Task<ActionResult> Articles(int currentPage = 1)
        {
            ViewBag.user_id = GetId();
            bool check = await model.GetSingle(ViewBag.user_id);
            bool checkData = await model.GetAllArticle(currentPage);
            if (checkData)
            {
                try
                {
                    await model.GetAllCategory();
                    ViewBag.category = model.AllCategory;

                }
                catch
                {
                    await model.GetAllCategory();
                }
                ViewBag.page_cout = pl.GetPageInfo(10, await model.GetToTalArticle());
                ViewBag.current_page = currentPage;
                return View(model);
            }
            return View();
        }
        [HttpPost,ValidateInput(false)]
        public async Task<JsonResult> AddArticle(Article obj)
        {
            
            if (Request.IsAjaxRequest())
            {
                if (obj != null)
                {
                    if (obj.uploadImage != null)
                    {
                        var fileName = ProcessImage(obj.uploadImage.FileName);
                        obj.article_thumbnail = "~/Public/assets/img/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Public/assets/img/"), fileName);
                        obj.uploadImage.SaveAs(fileName);
                    }
                    string id =  GetId();
                    bool checkAdd = await model.AddArticle(obj,id);
                    if (checkAdd)
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            RedirectToAction("Articles");
            return Json(false, JsonRequestBehavior.AllowGet);
        }


        //
        public async Task<ActionResult> Categories(int page = 1) {

            ViewBag.user_id = GetId();
            bool check = await model.GetSingle(ViewBag.user_id);
            bool getData = await model.GetCategoryByPage(page);

            if (getData)
            {
                ViewBag.current_page = page;
                ViewBag.page_cout = pl.GetPageInfo(10, await model.GetTotalCategory());
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AddCategory(Category obj)
        {
            if (Request.IsAjaxRequest())
            {
                if (obj != null)
                {
                    bool check = await model.AddCategory(obj);
                    if (check)
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<JsonResult> GetCategoryData(long id)
        {
            if (Request.IsAjaxRequest())
            {
                if (id > 0)
                {
                    var ef = await model.GetCategoryById(id);
                    return Json(new
                    {
                        reuslt = new Category
                        {
                            category_name = ef.category_name,
                            update_at = ef.update_at,
                            create_at = ef.create_at
                        }
                    }, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateCategory(Category category)
        {
            if (Request.IsAjaxRequest())
            {
                if (category != null)
                {
                    bool check = await model.UpdateCategory(category);
                    if (check)
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        [HttpPost] 
        public async Task<JsonResult> RemoveCategory(long id)
        {
            if (Request.IsAjaxRequest())
            {
                if (id > 0)
                {
                    bool check = await model.RemoveCategory(id);
                    if (check)
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> RemovedArticle(long id)
        {
            if (Request.IsAjaxRequest())
            {
                if (id > 0)
                {
                    bool checkRemove = await model.RemoveArticleById(id);
                    if (checkRemove)
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> GetTotalCategory()
        {
            if (Request.IsAjaxRequest())
            {
                var ef = await model.GetCountCategory();
                int count =  pl.GetPageInfo(10,ef);
                return Json(new { result = count },JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> CategorySearch(string queryStr,int page = 1)
        {
            if (Request.IsAjaxRequest())
            {
                var data = await model.SearchCategoryByKeyword(queryStr, page);
                if (data != null)
                {
                    List<Category> new_ef = new List<Category>();
                    foreach(var item in data)
                    {
                        Category ef = new Category();
                        ef.category_name = item.category_name;
                        ef.id = item.id;
                        ef.update_at = item.update_at;
                        ef.create_at = item.create_at;
                        new_ef.Add(ef);
                    }
                    return Json(new { result = new_ef }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async  Task<JsonResult> GetArticleData(long id)
        {
            if (Request.IsAjaxRequest())
            {
                if (id > 0)
                {
                    var data = await model.GetSingleArticleById(id);
                   
                    if (data != null)
                    {
                      
                        return Json(new {result = new Article()
                        {
                            article_content = data.article_content,
                            article_title = data.article_title,
                            article_thumbnail = data.article_thumbnail,
                            author_id = data.author_id,
                            view_id = data.view_id,
                            category_id = data.category_id,

                        }}, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public string ProcessImage(string path)
        {
            var fileName = Path.GetFileNameWithoutExtension(path);
            var extension = Path.GetExtension(path);
            fileName =  fileName + DateTime.Now.ToString("yymmssfff") + extension;
            return fileName;
        }

        [HttpPost,ValidateInput(false)]
        public async Task<JsonResult> UpdateArticle(Article obj)
        {
            if (Request.IsAjaxRequest())
            {
                if (obj != null)
                {
                    if (obj.uploadImage != null)
                    {
                        var fileName =  ProcessImage(obj.uploadImage.FileName);
                        obj.article_thumbnail = "~/Public/assets/img/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Public/assets/img/"), fileName);
                        obj.uploadImage.SaveAs(fileName);
                    }

                    bool check = await model.UpdateArticle(obj);
                    if (check)
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> GetCountArticle()
        {
            if (Request.IsAjaxRequest())
            {
                var ef = await model.GetCountArticle();
                int count = pl.GetPageInfo(10, ef);
                return Json(new { result = count }, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);

        }
        [HttpPost,ValidateInput(false)]
        public async Task<JsonResult> ArticleSearch(string queryStr, int page = 1)
        {
            if (Request.IsAjaxRequest())
            {
                var data = await model.SearchArticleByKeyword(queryStr, page);
                if (data != null)
                {
                    List<Article> new_ef = new List<Article>();
                    
                    foreach (var item in data)
                    {
                        View ef_view = new View();
                        ef_view.total_view = item.view.total_view;
                        ef_view.id = item.view.id;
                        Article ef = new Article();
                        ef.id = item.id;
                        ef.article_title = item.article_title;
                        ef.article_content = item.article_content;
                        ef.article_thumbnail = item.article_thumbnail;
                        ef.category_id = item.category_id;
                        ef.create_at = item.create_at;
                        ef.update_at = item.update_at;
                        ef.view_id = item.view_id;
                        ef.view = ef_view;
                        new_ef.Add(ef);
                    }
                    return Json(new_ef, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Fonts()
        {
            ViewBag.user_id = GetId();
            bool check = await model.GetSingle(ViewBag.user_id);
            return View(model);
        }
        public ActionResult BlankPage()
        {
            return View();
        }

        public ActionResult GoogleMap()
        {
            return View();
        }
    }
   
}