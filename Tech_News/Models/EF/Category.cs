using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Web;

namespace Tech_News.Models.EF
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [MaxLength(128)]
        public string category_name { get; set; }

        public virtual ICollection<Article> articles { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }

    }
}