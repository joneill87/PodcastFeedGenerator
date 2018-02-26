using PodcastFeedGenerator.Models;
using PodcastFeedGenerator.ViewModels.FeedDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.Factories
{
    public static class FeedDefinitionFactory
    {
        public static FeedDefinitionViewModel ToViewModel(this FeedDefinition feedDefinition)
        {
            return new FeedDefinitionViewModel()
            {
                ID = feedDefinition.ID,
                Title = feedDefinition.Title
            };
        }

        public static IEnumerable<FeedDefinitionViewModel> ToViewModels(this IEnumerable<FeedDefinition> feedDefinitions)
        {
            return feedDefinitions.Select(fd => new FeedDefinitionViewModel() { ID = fd.ID, Title = fd.Title });
        }

        public static FeedDefinition Create()
        {
            return new FeedDefinition()
            {
                XMLNameSpace = "http://www.itunes.com/dtds/podcast-1.0.dtd"
            };
        }

        public static FeedDefinition ToFeedDefinition(this FeedDefinitionBindingModel model)
        {
            var fd = Create();
            fd.ID = model.ID;
            fd.Title = model.Title;
            return fd;
        }

        public static FeedDefinitionBindingModel ToBindingModel(this FeedDefinition model)
        {
            return new FeedDefinitionBindingModel()
            {
                ID = model.ID,
                Title = model.Title
            };
        }
    }
}