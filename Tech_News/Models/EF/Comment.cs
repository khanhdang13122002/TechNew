using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Web;

namespace Tech_News.Models.EF
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public long parent_id { get; set; }
        [Required]
        [ForeignKey("user")]
        public string user_id { get; set; }
        [Required]
        [ForeignKey("articles")]

        public long article_id { get; set; }
        [Required]
        [MinLength(20)]
        public string comment_content { get; set; }
        public virtual AppUser user{ get; set; }
        public virtual Article articles { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }

    }
}