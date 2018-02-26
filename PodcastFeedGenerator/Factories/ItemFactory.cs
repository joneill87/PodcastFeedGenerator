using PodcastFeedGenerator.Helpers;
using PodcastFeedGenerator.Models;
using PodcastFeedGenerator.Repositories;
using PodcastFeedGenerator.ViewModels.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.Factories
{
    public static class ItemFactory
    {
        public static ItemViewModel ToViewModel(this Item item, CategoryRepository categoryRepo)
        {
            
            string cat1Text = null;
            string cat2Text = null;
            string cat3Text = null;

            var cat1 = categoryRepo.Find(item.PrimaryCategoryID);
            var cat2 = categoryRepo.Find(item.Category2ID);
            var cat3 = categoryRepo.Find(item.Category3ID);

            if (cat1 != null)
            {
                cat1Text = cat1.ParentCategory;
            }

            if (cat2 != null)
            {
                cat2Text = cat2.ParentCategory;
            }

            if (cat3 != null)
            {
                cat3Text = cat3.ParentCategory;
            }

            return new ItemViewModel()
            {
                Category1 = cat1Text,
                Category2 = cat2Text,
                Category3 = cat3Text,
                ChannelID = item.ChannelID,
                Description = item.Description,
                FileURL = item.FileURL,
                ID = item.ID,
                IsExplicit = item.IsExplicit,
                Keywords = item.Keywords,
                PublicationDate = item.PublicationDate,
                SubTitle = item.SubTitle,
                Title = item.Title,
                URL = item.URL
            };
        }

        public static IEnumerable<ItemViewModel> ToViewModels(this IEnumerable<Item> items, CategoryRepository categoryRepo)
        {
            return items.Select(i => i.ToViewModel(categoryRepo));
        }

        public static Item Create(int channelId, ChannelRepository channelRepo, CategoryRepository categoryRepo)
        {
            var channel = channelRepo.Find(channelId);

            var cat1 = categoryRepo.Find(channel.PrimaryCategoryID);
            var cat2 = categoryRepo.Find(channel.Category2ID);
            var cat3 = categoryRepo.Find(channel.Category3ID);

            return new Item()
            {
                Author = channel.Author,
                Category2 = cat2,
                Category3 = cat3,
                IsExplicit = channel.IsExplicit,
                PrimaryCategory = cat1
            };
        }

        public static Item ToItem(this ItemBindingModel model, CategoryRepository categoryRepo)
        {
            var cat1 = categoryRepo.FindOrCreate(model.Category1);
            var cat2 = categoryRepo.FindOrCreate(model.Category2);
            var cat3 = categoryRepo.FindOrCreate(model.Category3);

            var fileMeta = FileHelpers.GetMp3Meta(model.File);
            var fileURL = new FTPClient().UploadFile(model.File);

            return new Item()
            {
                Author = model.Author,
                Category2=cat2,
                Category3=cat3,
                ChannelID=model.ChannelID,
                Description = model.Description,
                FileSizeInBytes = fileMeta.FileSizeInBytes,
                FileURL = fileURL,
                ID = model.ID,
                IsExplicit = model.IsExplicit,
                Keywords = model.Keywords,
                LengthInSeconds = fileMeta.LengthInSeconds,
                MimeType = fileMeta.MimeType,
                PrimaryCategory=cat1,
                PublicationDate = model.PublicationDate,
                SubTitle = model.SubTitle,
                Summary = model.Description,
                Title = model.Title,
                URL = model.URL
            };
        }

        public static ItemBindingModel ToBindingModel(this Item item, CategoryRepository categoryRepo)
        {
            string cat1Text = null;
            string cat2Text = null;
            string cat3Text = null;

            var cat1 = categoryRepo.Find(item.PrimaryCategoryID);
            var cat2 = categoryRepo.Find(item.Category2ID);
            var cat3 = categoryRepo.Find(item.Category3ID);

            if (cat1 != null)
            {
                cat1Text = cat1.ParentCategory;
            }

            if (cat2 != null)
            {
                cat2Text = cat2.ParentCategory;
            }

            if (cat3 != null)
            {
                cat3Text = cat3.ParentCategory;
            }

            return new ItemBindingModel()
            {
                Author = item.Author,
                Category1 = cat1Text,
                Category2 = cat2Text,
                Category3 = cat3Text,
                ChannelID = item.ChannelID,
                Description = item.Description,
                ID = item.ID,
                IsExplicit = item.IsExplicit,
                Keywords = item.Keywords,
                PublicationDate = item.PublicationDate,
                SubTitle = item.SubTitle,
                Title = item.Title,
                URL = item.URL
            };
        }
    }
}