using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Tag
    {
        [Key]
        public int tag_id { get; set; }

        public string tag_name { get; set; }
    }
}