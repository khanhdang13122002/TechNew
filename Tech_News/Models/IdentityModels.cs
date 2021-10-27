using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Tech_News.Models.EF;
namespace Tech_News.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        
        public ApplicationDbContext()
            : base("Tech_News", throwIfV1Schema: false)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<View> Views { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Comment> Comments { get; set; }
   
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
       
       
    }
   
}