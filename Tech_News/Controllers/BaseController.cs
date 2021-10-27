using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tech_News.Models.DAO;
using Tech_News.Models.EF;


namespace Tech_News.Controllers
{
    public class BaseController: Controller , IController
    {
        protected CategoryLayoutView category_layout = new CategoryLayoutView();
        List<Category> categories;
        public async Task<List<Category>> GetCategories()
        {
            categories = await category_layout.GetCateGories();
            return categories;
        }
        public string GetId() {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdValue = "";
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    userIdValue = userIdClaim.Value;
                    return userIdValue;
                }
            }
            return null;
        }
    }
    
}
