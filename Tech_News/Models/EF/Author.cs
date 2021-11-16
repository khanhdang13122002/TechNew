
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tech_News.Models.EF
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Required]
        [MinLength(6)]
        public string author_name { get; set; }
        public string author_facebook { get; set; }

        public string author_instagram { get; set; }
        public string author_twitter { get; set; }
        public virtual AppUser user { get; set; }
        public virtual ICollection<Article> articles { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
        [NotMapped]
        public string user_id { get; set; }


    }
}