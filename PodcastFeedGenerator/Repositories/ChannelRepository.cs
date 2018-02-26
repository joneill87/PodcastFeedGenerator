using PodcastFeedGenerator.Helpers;
using PodcastFeedGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.Repositories
{
    public class ChannelRepository
    {
        private ApplicationDbContext context;

        public ChannelRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        private IQueryable<Channel> GetChannels()
        {
            return context.Channels.AsQueryable().LoadCategories().LoadItems();
        }

        public List<Channel> GetByFeedDefinition(int feedDefinitionId)
        {
            return GetChannels().Where(c => c.FeedDefinitionID == feedDefinitionId).ToList();
        }

        public Channel Find(int channelId)
        {
            return GetChannels().SingleOrDefault(c => c.ID == channelId);
        }

        private Channel AttachCategories(Channel entity)
        {
            var categoryRepo = new CategoryRepository(context);

            Category primaryCategory = null;
            Category category2 = null;
            Category category3 = null;

            if (entity.PrimaryCategory != null)
            {
                primaryCategory = categoryRepo.FindOrCreate(entity.PrimaryCategory.ParentCategory, entity.PrimaryCategory.Subcategory);
            }

            if (entity.Category2 != null)
            {
                category2 = categoryRepo.FindOrCreate(entity.Category2.ParentCategory, entity.Category2.Subcategory);
            }

            if (entity.Category3 != null)
            {
                category3 = categoryRepo.FindOrCreate(entity.Category3.ParentCategory, entity.Category3.Subcategory);
            }

            //attach the incoming categories to the entity to be updated
            if (primaryCategory != null)
            {
                entity.PrimaryCategoryID = primaryCategory.CategoryID;
            }

            if (category2 != null)
            {
                entity.Category2ID = category2.CategoryID;
            }

            if (category3 != null)
            {
                entity.Category3ID = category3.CategoryID;
            }

            return entity;
        }

        public Channel Create(Channel entity)
        {
            entity = AttachCategories(entity);
            context.Channels.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public Channel Update(Channel entity)
        {
            var found = context.Channels.Find(entity.ID);

            entity = AttachCategories(entity);
            
            found.Author = entity.Author;
            found.Category2ID = entity.Category2ID;
            found.Category3ID = entity.Category3ID;
            found.CopyrightNotice = entity.CopyrightNotice;
            found.Description = entity.Description;
            found.DocumentationSource = entity.DocumentationSource;

            if (entity.ImageURL != null)
            {
                found.ImageURL = entity.ImageURL;
            }
            
            found.IsExplicit = entity.IsExplicit;
            found.Language = entity.Language;
            found.LastBuildDate = entity.LastBuildDate;
            found.OwnerEmail = entity.OwnerEmail;
            found.OwnerName = entity.OwnerName;
            found.PrimaryCategoryID = entity.PrimaryCategoryID;
            found.PublicationDate = entity.PublicationDate;
            found.SubTitle = entity.SubTitle;
            found.Summary = entity.Summary;
            found.Title = entity.Title;
            found.WebmasterEmail = entity.WebmasterEmail;
            found.WebsiteURL = entity.WebsiteURL;

            context.SaveChanges();

            return entity;
        }

        public Channel CreateOrUpdate(Channel entity)
        {
            if (entity.ID == 0)
            {
                return Create(entity);
            }
            else
            {
                return Update(entity);
            }
        }

        public void Delete(int channelId)
        {
            var found = GetChannels().Single(c => c.ID == channelId);
            context.Channels.Remove(found);
            context.SaveChanges();
        }
    }
}