using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tech_News.Models.EF
{
    public class Article
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long id { get; set; }

        [Required(ErrorMessage = "Title can't be null")]
        [MinLength(10,ErrorMessage = "Title can't less than 10 character")]
        public string article_title { get; set; }

        public string article_thumbnail { get; set; }

        [MinLength(255,ErrorMessage = "Content can't less than 255 character")]
        [Required(ErrorMessage ="Content can't be null")]
        public string article_content { get; set; }

        [Required]
        [ForeignKey("author")]
        public long author_id { get; set; }
        [Required]
        [ForeignKey("category")]
        public int category_id { get; set; }
        [Required]
        [ForeignKey("view")]
        public long view_id { get; set; }

        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }

        public virtual Category category { get; set; }
        public virtual View view { get; set; }
        public virtual Author author { get; set; }
        public virtual ICollection<Comment> comments { get; set; }
        
        public virtual Tag tag { get; set; }
        [NotMapped]
        public HttpPostedFileBase uploadImage { get; set; }

        public Article()
        {
            article_thumbnail = "~/Public/assets/img/tech_about.jpg";
        }
    }
}