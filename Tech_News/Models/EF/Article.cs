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

        [Required]
        [MinLength(50)]
        public string article_title { get; set; }

        [Required]
        public string article_thumbnail { get; set; }

        [MinLength(255)]
        [Required]
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

    }
}