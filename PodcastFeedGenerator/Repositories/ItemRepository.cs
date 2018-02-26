using PodcastFeedGenerator.Helpers;
using PodcastFeedGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.Repositories
{
    public class ItemRepository
    {
        private ApplicationDbContext context;

        public ItemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        private IQueryable<Item> GetItems()
        {
            return context.Items.Include("Channel").LoadCategories();
        }

        public List<Item> GetByChannel(int channelId)
        {
            return GetItems().Where(i => i.ChannelID == channelId).ToList();
        }

        public Item Find(int itemId)
        {
            return GetItems().SingleOrDefault(i => i.ID == itemId);
        }

        public Item Find(int? itemId)
        {
            if (itemId.HasValue == false)
            {
                return null;
            }
            return Find(itemId.Value);
        }

        private Item AttachCategories(Item entity)
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

        public Item Create(Item entity)
        {
            entity = AttachCategories(entity);
            context.Items.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public Item Update(Item entity)
        {
            entity = AttachCategories(entity);

            var found = Find(entity.ID);
            found.Author = entity.Author;
            found.Category2ID = entity.Category2ID;
            found.Category3ID = entity.Category3ID;
            found.Description = entity.Description;
            found.FileSizeInBytes = entity.FileSizeInBytes;
            found.FileURL = entity.FileURL;
            found.IsExplicit = entity.IsExplicit;
            found.Keywords = entity.Keywords;
            found.LengthInSeconds = entity.LengthInSeconds;
            found.MimeType = entity.MimeType;
            found.PrimaryCategoryID = entity.PrimaryCategoryID;
            found.PublicationDate = entity.PublicationDate;
            found.SubTitle = entity.SubTitle;
            found.Summary = entity.Summary;
            found.Title = entity.Title;
            found.URL = entity.URL;

            context.SaveChanges();
            return entity;

        }

        public void Delete(int itemId)
        {
            var found = GetItems().Single(i => i.ID == itemId);
            context.Items.Remove(found);
            context.SaveChanges();
        }

    }
}