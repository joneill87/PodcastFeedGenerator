using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.Models
{
    public class FeedDefinition
    {
        public FeedDefinition()
        {
            Channels = new List<Channel>();
        }

        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string XMLNameSpace { get; set; }

        public ICollection<Channel> Channels { get; set; }
    }
}