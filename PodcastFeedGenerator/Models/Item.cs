using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.Models
{
    public class Item
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Episode Title, should be short
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Link to the podcast episode's webpage
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Full episode description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Link to the downloadable file
        /// </summary>
        public string FileURL { get; set; }

        /// <summary>
        /// The size of the file, in bytes
        /// </summary>
        public long FileSizeInBytes { get; set; }

        /// <summary>
        /// Length of the audio file, in seconds
        /// </summary>
        public int LengthInSeconds { get; set; }

        /// <summary>
        /// MIME type of downloadable file
        /// </summary>
        public string MimeType { get; set; }

        [ForeignKey("PrimaryCategory")]
        public int PrimaryCategoryID { get; set; }
        [ForeignKey("Category2")]
        public int? Category2ID { get; set; }
        [ForeignKey("Category3")]
        public int? Category3ID { get; set; }

        /// <summary>
        /// The primary category of this podcast. Some platforms only allow you to list one category.
        /// This is the category that will be shown in that case
        /// </summary>
        public Category PrimaryCategory { get; set; }

        /// <summary>
        /// Itunes allows up to 3 categories. This represents all categories to which the podcast belongs
        /// </summary>
        public Category Category2 { get; set; }

        public Category Category3 { get; set; }

        /// <summary>
        /// Date episode was first published
        /// </summary>
        public DateTime? PublicationDate { get; set; }

        /// <summary>
        /// Author name
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// For Itunes, indicates whether podcast contains explicit material
        /// </summary>
        public bool IsExplicit { get; set; }

        /// <summary>
        /// For Itunes, a subtitle for the episdoe
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// For Itunes, summary of the episode
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// For Itunes, a comma-separated list of keywords relating to the episode
        /// </summary>
        public string Keywords { get; set; }

        [ForeignKey("Channel")]
        public int ChannelID { get; set; }

        [InverseProperty("Items")]
        public Channel Channel { get; set; }

        public IEnumerable<Category> AllCategories
        {
            get
            {
                return new List<Category>() { PrimaryCategory, Category2, Category3 }.Where(c => c != null);
            }
        }
    }
}