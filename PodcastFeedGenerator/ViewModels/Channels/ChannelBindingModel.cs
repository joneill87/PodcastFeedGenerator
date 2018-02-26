using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.ViewModels.Channels
{
    public class ChannelBindingModel
    {
        [Key]
        public int ID { get; set; }

        public int FeedDefinitionID { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string WebsiteURL { get; set; }

        public string Language { get; set; }

        public string CopyrightNotice { get; set; }

        public string WebmasterEmail { get; set; }

        public string Author { get; set; }

        public string OwnerName { get; set; }

        public string OwnerEmail { get; set; }

        public bool IsExplicit { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Icon { get; set; }

        public string Category1 { get; set; }

        public string Category2 { get; set; }

        public string Category3 { get; set; }

        public DateTime? LastBuildDate { get; set; }

        public DateTime? PublicationDate { get; set; }
    }
}