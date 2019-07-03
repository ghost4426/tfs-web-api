﻿using AutoMapper;
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
                //.ForMember(dest => dest.CategoriesId, opts => opts.MapFrom(src => src.CategoriesId))
                //.ForMember(dest => dest.Breed, opts => opts.MapFrom(src => src.Breed))
                //.ForMember(dest => dest.FarmerId, opts => opts.MapFrom(src => src.FarmerId))
                .ReverseMap();

            CreateMap<Entities.Treatment, Models.CreateTreatmentRequest>().ReverseMap();

            CreateMap<Models.PackagingRequest, Models.FoodData.Packaging>().ReverseMap();

            CreateMap<Entities.User, Models.CreateUserRequest>().ReverseMap();
        }
    }
}