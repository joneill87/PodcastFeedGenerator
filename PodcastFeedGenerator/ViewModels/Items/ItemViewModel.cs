using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.ViewModels.Items
{
    public class ItemViewModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string URL { get; set; }

        public string Description { get; set; }

        public string Category1 { get; set; }

        public string Category2 { get; set; }

        public string Category3 { get; set; }

        public bool IsExplicit { get; set; }

        public string Keywords { get; set; }

        public string FileURL { get; set; }

        public int ChannelID { get; set; }

        public DateTime? PublicationDate { get; set; }
    }
}