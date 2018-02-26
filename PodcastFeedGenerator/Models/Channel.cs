using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.Models
{
    public class Channel
    {
        public Channel()
        {
            DocumentationSource = "http://blogs.law.harvard.edu/tech/rss";
        }

        [Key]
        public int ID { get; set; }

        [ForeignKey("FeedDefinition")]
        public int FeedDefinitionID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string WebsiteURL { get; set; }

        public string Language { get; set; }

        public string CopyrightNotice { get; set; }

        public DateTime LastBuildDate { get; set; }

        public DateTime PublicationDate { get; set; }

        public string DocumentationSource { get; set; }

        public string WebmasterEmail { get; set; }

        public string Author { get; set; }

        public string SubTitle { get; set; }

        public string Summary { get; set; }

        public string OwnerName { get; set; }

        public string OwnerEmail { get; set; }

        public bool IsExplicit { get; set; }

        public string ImageURL { get; set; }

        [ForeignKey("PrimaryCategory")]
        public int PrimaryCategoryID { get; set; }
        [ForeignKey("Category2")]
        public int? Category2ID { get; set; }
        [ForeignKey("Category3")]
        public int? Category3ID { get; set; }

        public Category PrimaryCategory { get; set; }

        public Category Category2 { get; set; }

        public Category Category3 { get; set; }

        [InverseProperty("Channels")]
        public FeedDefinition FeedDefinition { get; set; }

        public ICollection<Item> Items { get; set; }

        public IEnumerable<Category> AllCategories {
            get
            {
                return new List<Category>() { PrimaryCategory, Category2, Category3 }.Where(c => c != null);
            }
        }
    }
}