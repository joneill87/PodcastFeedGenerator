using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.ViewModels.Items
{
    public class ItemBindingModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string URL { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category1 { get; set; }

        public string Category2 { get; set; }

        public string Category3 { get; set; }

        public bool IsExplicit { get; set; }

        public string Keywords { get; set; }

        public string Author { get; set; }

        public DateTime? PublicationDate { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }

        public int ChannelID { get; set; }
    }
}