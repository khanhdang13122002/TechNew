namespace Tech_News.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;


    internal sealed class Configuration : DbMigrationsConfiguration<Tech_News.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Tech_News.Models.ApplicationDbContext";
        }

        protected override void Seed(Tech_News.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            // default ta
            /*       context.Views.Add(new Models.EF.View { id = 1, article_id = 1, total_view = 200 });*/
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
          /*  for (int i = 0; i < 20; i++)
            {
                Models.EF.View v = new Models.EF.View();
                v = context.Views.Add(new Models.EF.View { total_view = i * 100, update_at = DateTime.Now, create_at = DateTime.Now });
                context.Articles.Add(new Models.EF.Article
                {
                    article_title = "Sony launce a new Android tablets for new generation",
                    article_content = "<p>But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful.</p>< p > Nor again is there anyone who loves or pursues or desires to obtain pain of itself,because it is pain,but because occasionally circumstances occur in which toil and pain can procure him some great pleasure.To take a trivial example,which of us ever undertakes laborious physical exercise,except to obtain some advantage from it?</ p >< blockquote class='pull-left'>But I must explain to you how all this mistaken idea of denouncing pleasure</blockquote><p>But who has any right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who avoids a pain that produces no resultant pleasure? On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee.Nor again is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure.To take a trivial example, which of us ever undertakes laborious physical exercise, except to obtain some advantage from it? Nor again is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure. To take a trivial example, which of us ever</p><p>But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful.</p>",
                    article_thumbnail = "~/Public/assets/img/tab_top1.jpg",
                    update_at = DateTime.Now,
                    create_at = DateTime.Now,
                    view_id = 2,
                    category_id = 3,
                    author_id = 2

                });
            }*/
        }
    }
}
