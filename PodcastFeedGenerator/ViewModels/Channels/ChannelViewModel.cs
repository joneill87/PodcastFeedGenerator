using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.ViewModels.Channels
{
    public class ChannelViewModel
    {
        public int ID { get; set; }

        public int FeedDefinitionID { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string WebsiteURL { get; set; }

        public string Language { get; set; }

        public string CopyrightNotice { get; set; }

        public DateTime LastBuildDate { get; set; }

        public DateTime PublicationDate { get; set; }

        public string WebmasterEmail { get; set; }

        public string Author { get; set; }

        public string OwnerName { get; set; }

        public string OwnerEmail { get; set; }

        public bool IsExplicit { get; set; }

        public string ImageURL { get; set; }

        public string Category1 { get; set; }

        public string Category2 { get; set; }

        public string Category3 { get; set; }
    }
}