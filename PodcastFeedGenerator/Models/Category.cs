using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        public string ParentCategory { get; set; }

        public string Subcategory { get; set; }

        public bool IsCompound
        {
            get { return String.IsNullOrEmpty(Subcategory) == false; }
        }
    }
}