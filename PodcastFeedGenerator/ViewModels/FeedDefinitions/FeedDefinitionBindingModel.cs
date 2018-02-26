using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.ViewModels.FeedDefinitions
{
    public class FeedDefinitionBindingModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }
    }
}