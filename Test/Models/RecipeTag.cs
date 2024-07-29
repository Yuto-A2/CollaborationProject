using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class RecipeTag
    {
        [Key]
        public int recipe_tag_id { get; set; }

        [ForeignKey("Tag")]
        public int recipe_id { get; set; }
        public int tag_id { get; set; }
        public virtual Tag Tag { get; set; }
    }
}