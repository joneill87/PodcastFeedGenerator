using AutoMapper;
using AutoMapper.Configuration;
using PodcastFeedGenerator.Models;
using PodcastFeedGenerator.ViewModels.Channels;
using PodcastFeedGenerator.ViewModels.FeedDefinitions;
using PodcastFeedGenerator.ViewModels.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.Infrastructure
{
    public static class AutoMapConfigurator
    {
        public static void Configurate(IMapperConfigurationExpression config)
        {
            ConfigureFeedDefinitionMappings(config);

            ConfigureChannelMappings(config);

            ConfigureItemMappings(config);
        }

        private static void ConfigureFeedDefinitionMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<FeedDefinition, FeedDefinitionBindingModel>();

            config.CreateMap<FeedDefinition, FeedDefinitionViewModel>();

            config.CreateMap<FeedDefinitionBindingModel, FeedDefinition>()
                .ForMember(
                    dest => dest.XMLNameSpace,
                    opt => opt.UseValue<string>("http://www.itunes.com/dtds/podcast-1.0.dtd"))
                .ForMember(
                    dest => dest.Channels,
                    opt => opt.Ignore());
        }

        private static void ConfigureChannelMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Channel, ChannelBindingModel>()
                .ForMember(
                    dest => dest.Category1,
                    opt => opt.MapFrom<string>(src => src.PrimaryCategory.ParentCategory))
                .ForMember(
                    dest => dest.Category2,
                    opt => opt.MapFrom<string>(src => src.Category2 == null ? String.Empty : src.Category2.ParentCategory))
                .ForMember(
                    dest => dest.Category3,
                    opt => opt.MapFrom<string>(src => src.Category3 == null ? String.Empty : src.Category3.ParentCategory))
                .ForMember(
                    dest => dest.Icon,
                    opt => opt.Ignore());

            config.CreateMap<Channel, ChannelViewModel>().ForMember(
                    dest => dest.Category1,
                    opt => opt.MapFrom<string>(src => src.PrimaryCategory.ParentCategory))
                .ForMember(
                    dest => dest.Category2,
                    opt => opt.MapFrom<string>(src => src.Category2 == null ? String.Empty : src.Category2.ParentCategory))
                .ForMember(
                    dest => dest.Category3,
                    opt => opt.MapFrom<string>(src => src.Category3 == null ? String.Empty : src.Category3.ParentCategory));

            config.CreateMap<ChannelBindingModel, Channel>()
                .ForMember(
                    dest => dest.PrimaryCategory,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Category2,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Category3,
                    opt => opt.Ignore())
                    .ForMember(
                    dest => dest.PrimaryCategoryID,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Category2ID,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Category3ID,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Items,
                    opt => opt.Ignore())
                 .ForMember(
                    dest => dest.ImageURL,
                    opt => opt.Ignore())
                 .ForMember(
                    dest => dest.LastBuildDate,
                    opt => opt.NullSubstitute(DateTime.Now))
                .ForMember(
                    dest => dest.PublicationDate,
                    opt => opt.NullSubstitute(DateTime.Now))
                .ForMember(
                    dest => dest.DocumentationSource,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.FeedDefinition,
                    opt => opt.Ignore());
        }

        private static void ConfigureItemMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Item, ItemViewModel>()
                 .ForMember(
                    dest => dest.Category1,
                    opt => opt.MapFrom<string>(src => src.PrimaryCategory.ParentCategory))
                .ForMember(
                    dest => dest.Category2,
                    opt => opt.MapFrom<string>(src => src.Category2 == null ? String.Empty : src.Category2.ParentCategory))
                .ForMember(
                    dest => dest.Category3,
                    opt => opt.MapFrom<string>(src => src.Category3 == null ? String.Empty : src.Category3.ParentCategory));

            config.CreateMap<Item, ItemBindingModel>()
                .ForMember(
                   dest => dest.Category1,
                   opt => opt.MapFrom<string>(src => src.PrimaryCategory.ParentCategory))
               .ForMember(
                   dest => dest.Category2,
                   opt => opt.MapFrom<string>(src => src.Category2 == null ? String.Empty : src.Category2.ParentCategory))
               .ForMember(
                   dest => dest.Category3,
                   opt => opt.MapFrom<string>(src => src.Category3 == null ? String.Empty : src.Category3.ParentCategory))
               .ForMember(
                    dest => dest.File,
                    opt => opt.Ignore());

            config.CreateMap<ItemBindingModel, Item>()
                .ForMember(
                    dest => dest.FileURL,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.FileSizeInBytes,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.LengthInSeconds,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.MimeType,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.PublicationDate,
                    opt => opt.NullSubstitute(DateTime.Now))
                .ForMember(
                    dest => dest.PrimaryCategory,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Category2,
                    opt => opt.Ignore())
                    .ForMember(
                    dest => dest.Category3,
                    opt => opt.Ignore())
                    .ForMember(
                    dest => dest.PrimaryCategoryID,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Category2ID,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Category3ID,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Summary,
                    opt => opt.ResolveUsing(src => src.Description))
                .ForMember(
                    dest => dest.Channel,
                    opt => opt.Ignore());
        }
    }
}