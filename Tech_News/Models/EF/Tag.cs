using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Web;

namespace Tech_News.Models.EF
{
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        [Required]
        public string tag_name { get; set; }

        public ICollection<Article> articles { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }

    }
}