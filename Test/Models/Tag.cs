using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    // represents keywords for users to type
    // a possible extra feature
    public class Tag
    {
        [Key]
        public int tag_id { get; set; }

        public string tag_name { get; set; }
    }
}