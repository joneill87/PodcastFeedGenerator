using PodcastFeedGenerator.Helpers;
using PodcastFeedGenerator.Models;
using PodcastFeedGenerator.Repositories;
using PodcastFeedGenerator.ViewModels.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.Factories
{
    public static class ChannelFactory
    {
        public static ChannelViewModel ToViewModel(this Channel channel, CategoryRepository categoryRepo)
        {
            string cat1Text = null;
            string cat2Text = null;
            string cat3Text = null;

            var cat1 = categoryRepo.Find(channel.PrimaryCategoryID);
            var cat2 = categoryRepo.Find(channel.Category2ID);
            var cat3 = categoryRepo.Find(channel.Category3ID);

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

            return new ChannelViewModel()
            {
                Author = channel.Author,
                Category1 = cat1Text,
                Category2 = cat2Text,
                Category3 = cat3Text,
                CopyrightNotice = channel.CopyrightNotice,
                Description = channel.Description,
                FeedDefinitionID = channel.FeedDefinitionID,
                ID = channel.ID,
                ImageURL = channel.ImageURL,
                IsExplicit = channel.IsExplicit,
                Language = channel.Language,
                LastBuildDate = channel.LastBuildDate,
                OwnerEmail = channel.OwnerEmail,
                OwnerName = channel.OwnerName,
                PublicationDate = channel.PublicationDate,
                SubTitle = channel.SubTitle,
                Summary = channel.Summary,
                Title = channel.Title,
                WebmasterEmail = channel.WebmasterEmail,
                WebsiteURL = channel.WebsiteURL
            };
        }

        public static IEnumerable<ChannelViewModel> ToViewModels(this IEnumerable<Channel> channels, CategoryRepository categoryRepo)
        {
            return channels.Select(c => c.ToViewModel(categoryRepo));
        }

        public static ChannelBindingModel Create(int feedDefinitionId, FeedDefinitionRepository fdRepo)
        {
            var feedDefinition = fdRepo.Find(feedDefinitionId);

            return new ChannelBindingModel()
            {
                FeedDefinitionID = feedDefinitionId,
                Title = feedDefinition.Title
            };
        }

        public static Channel ToChannel(this ChannelBindingModel model, CategoryRepository categoryRepo)
        {
            var cat1 = categoryRepo.FindOrCreate(model.Category1);
            var cat2 = categoryRepo.FindOrCreate(model.Category2);
            var cat3 = categoryRepo.FindOrCreate(model.Category3);

            var channel = new Channel()
            {
                Author = model.Author,
                Category2 = cat2,
                Category3 = cat3,
                CopyrightNotice = model.CopyrightNotice,
                Description = model.Description,
                FeedDefinitionID = model.FeedDefinitionID,
                ID = model.ID,
                IsExplicit = model.IsExplicit,
                Language = model.Language,
                LastBuildDate = model.LastBuildDate.HasValue ? model.LastBuildDate.Value : DateTime.Now,
                OwnerEmail = model.OwnerEmail,
                OwnerName = model.OwnerName,
                PrimaryCategory = cat1,
                PublicationDate = model.PublicationDate.HasValue ? model.PublicationDate.Value : DateTime.Now,
                SubTitle = model.SubTitle,
                Title = model.Title,
                WebmasterEmail = model.WebmasterEmail,
                WebsiteURL = model.WebsiteURL
            };

            if (model.Icon != null)
            {
                channel.ImageURL = new FTPClient().UploadFile(model.Icon);
            }

            return channel;
        }

        public static ChannelBindingModel ToBindingModel(this Channel channel, CategoryRepository categoryRepo)
        {
            string cat1Text = null;
            string cat2Text = null;
            string cat3Text = null;

            var cat1 = categoryRepo.Find(channel.PrimaryCategoryID);
            var cat2 = categoryRepo.Find(channel.Category2ID);
            var cat3 = categoryRepo.Find(channel.Category3ID);

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

            return new ChannelBindingModel()
            {
                Author = channel.Author,
                Category1 = cat1Text,
                Category2 = cat2Text,
                Category3 = cat3Text,
                CopyrightNotice = channel.CopyrightNotice,
                Description = channel.Description,
                FeedDefinitionID = channel.FeedDefinitionID,
                ID = channel.ID,
                IsExplicit = channel.IsExplicit,
                Language = channel.Language,
                LastBuildDate = channel.LastBuildDate,
                OwnerEmail = channel.OwnerEmail,
                OwnerName = channel.OwnerName,
                PublicationDate = channel.PublicationDate,
                SubTitle = channel.SubTitle,
                Summary = channel.Summary,
                Title = channel.Title,
                WebmasterEmail = channel.WebmasterEmail,
                WebsiteURL = channel.WebsiteURL
            };
        }
    }
}