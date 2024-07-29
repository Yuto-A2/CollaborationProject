using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class NutritionalNeeds
    {
        [Key]
        public int need_id { get; set; }
        public int protein {  get; set; }
        public int calories { get; set; }
    }
}