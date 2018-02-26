using PodcastFeedGenerator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.Helpers
{
    public static class QueryHelpers
    {
        public static IQueryable<FeedDefinition> LoadChannels(this IQueryable<FeedDefinition> query)
        {
            return query.Include(fd => fd.Channels)
                .Include("Channels.PrimaryCategory")
                .Include("Channels.Category2")
                .Include("Channels.Category3");
        }

        public static IQueryable<FeedDefinition> LoadChannelsAndItems(this IQueryable<FeedDefinition> query)
        {
            return query.Include(fd => fd.Channels)
                .Include("Channels.PrimaryCategory")
                .Include("Channels.Category2")
                .Include("Channels.Category3")
                .Include("Channels.Items.PrimaryCategory")
                .Include("Channels.Items.Category2")
                .Include("Channels.Items.Category3");
        }

        public static IQueryable<Channel> LoadCategories(this IQueryable<Channel> query)
        {
            return query.Include(c => c.PrimaryCategory)
                .Include(c => c.Category2)
                .Include(c => c.Category3);
        }

        public static IQueryable<Channel> LoadItems(this IQueryable<Channel> query)
        {
            return query.Include(c => c.Items)
                .Include("Items.PrimaryCategory")
                .Include("Items.Category2")
                .Include("Items.Category3");
        }

        public static IQueryable<Item> LoadCategories(this IQueryable<Item> query)
        {
            return query.Include(c => c.PrimaryCategory)
                .Include(c => c.Category2)
                .Include(c => c.Category3);
        }
    }
}