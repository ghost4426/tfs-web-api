using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Models = DTO.Models;
using Entities = DTO.Entities;

namespace Common.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Entities.Premises, Models.FoodData.Farm>()
            .ForMember(dest => dest.FarmId, opts => opts.MapFrom(src => src.PremisesId)).ReverseMap();

            CreateMap<Entities.Premises, Models.FoodData.Provider>()
            .ForMember(dest => dest.ProviderId, opts => opts.MapFrom(src => src.PremisesId)).ReverseMap();

            CreateMap<Entities.Food, Models.CreateFoodRequest>()
                .ReverseMap();

            CreateMap<Entities.Treatment, Models.CreateTreatmentRequest>().ReverseMap();

            CreateMap<Models.PackagingRequest, Models.FoodData.Packaging>().ReverseMap();

            CreateMap<Entities.User, Models.CreateUserRequest>().ReverseMap();

            CreateMap<Entities.User, Models.RegisterRequest>().ReverseMap();

            CreateMap<Entities.User, Models.User>().ReverseMap();

            CreateMap<Entities.Role, Models.Role>().ReverseMap();

            CreateMap<Entities.Premises, Models.RegisterRequest>()
                .ForMember(dest => dest.PremisesName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.PremisesAddress, opts => opts.MapFrom(src => src.Address))
                .ForMember(dest => dest.PremisesTypeId, opts => opts.MapFrom(src => src.TypeId))
                .ReverseMap();

            CreateMap<Entities.Category, Models.Category>()
               .ReverseMap();

            CreateMap<Entities.Food, Models.Food>()
               .ReverseMap();

            CreateMap<Entities.FoodDetailType, Models.Option>()
                 .ForMember(dest => dest.id, opts => opts.MapFrom(src => src.TypeId))
                 .ForMember(dest => dest.text, opts => opts.MapFrom(src => src.Name))
               .ReverseMap();
        }
    }
}
