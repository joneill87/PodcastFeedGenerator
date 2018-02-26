using PodcastFeedGenerator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PodcastFeedGenerator.Helpers;

namespace PodcastFeedGenerator.Controllers
{
    public class RssGenerationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private FeedDefinition GetFeedDefinition(int id)
        {
            var categoryPropertyNames = new List<string>() { "PrimaryCategory", "Category2", "Category3" };
            var channelPropertyNames = categoryPropertyNames.Select(pn => "Channels." + pn);
            var itemPropertyNames = categoryPropertyNames.Select(pn => "Channels.Items." + pn);
            var includePaths = channelPropertyNames.Concat(itemPropertyNames);
            var feedDefinitionQuery = db.FeedDefinitions.AsQueryable();
            foreach (var includePath in includePaths)
            {
                feedDefinitionQuery = feedDefinitionQuery.Include(includePath);
            }

            var feedDefinition = feedDefinitionQuery.SingleOrDefault(fd => fd.ID == id);
            return feedDefinition;
        }

        public ActionResult Generate(int id)
        {
            var feedDefinition = GetFeedDefinition(id);
            if (feedDefinition == null)
            {
                return HttpNotFound();
            }

            var feedContents = this.RenderRazorViewToString("Index", feedDefinition).Trim();
            var webAccessibleURI = new FTPClient().UploadFeed(feedContents);
            return View((object)webAccessibleURI);
        }

        // GET: RssGeneration
        public ActionResult Index(int id)
        {
            var categoryPropertyNames = new List<string>() { "PrimaryCategory", "Category2", "Category3" };
            var channelPropertyNames = categoryPropertyNames.Select(pn => "Channels." + pn);
            var itemPropertyNames = categoryPropertyNames.Select(pn => "Channels.Items." + pn);
            var includePaths = channelPropertyNames.Concat(itemPropertyNames);
            var feedDefinitionQuery = db.FeedDefinitions.AsQueryable();
            foreach (var includePath in includePaths)
            {
                feedDefinitionQuery = feedDefinitionQuery.Include(includePath);
            }

            var feedDefinition = feedDefinitionQuery.SingleOrDefault(fd => fd.ID == id);
            if (feedDefinition == null)
            {
                return HttpNotFound();
            }

            return View(feedDefinition);
        }
    }
}