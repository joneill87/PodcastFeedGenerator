using PodcastFeedGenerator.Helpers;
using PodcastFeedGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.Repositories
{
    public class FeedDefinitionRepository
    {
        private ApplicationDbContext context;

        public FeedDefinitionRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        private IQueryable<FeedDefinition> GetFeedDefinitions()
        {
            return context.FeedDefinitions.LoadChannelsAndItems();
        }

        public List<FeedDefinition> All()
        {
            return GetFeedDefinitions().ToList();
        }

        public FeedDefinition Find(int feedDefinitionId)
        {
            return GetFeedDefinitions().SingleOrDefault(fd => fd.ID == feedDefinitionId);
        }

        public FeedDefinition Create(FeedDefinition entity)
        {
            context.FeedDefinitions.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public FeedDefinition Update(FeedDefinition entity)
        {
            var found = Find(entity.ID);
            
            found.Title = entity.Title;
            found.XMLNameSpace = entity.XMLNameSpace;

            context.SaveChanges();

            return entity;
        }

        public void Delete(int feedDefinitionId)
        {
            var entity = GetFeedDefinitions().Single(fd => fd.ID == feedDefinitionId);
            context.FeedDefinitions.Remove(entity);
            context.SaveChanges();
        }
    }
}