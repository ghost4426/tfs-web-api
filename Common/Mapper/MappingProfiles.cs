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

            CreateMap<Entities.Treatment, Models.CreateMoreTreatmentRequest>().ReverseMap();

            CreateMap<Models.PackagingRequest, Models.FoodData.Packaging>().ReverseMap();

            CreateMap<Entities.User, Models.CreateUserRequest>().ReverseMap();

            CreateMap<Entities.User, Models.RegisterRequest>().ReverseMap();

            CreateMap<Entities.User, Models.User>().ReverseMap();

            CreateMap<Entities.User, Models.UserData>()
                .ForMember(dest => dest.Premises, opts => opts.MapFrom(src => src.Premises.PremisesType.Name))
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src.Role.Name))
                .ReverseMap();

            CreateMap<Entities.User, Models.UpdateUserRequest>().ReverseMap();

            CreateMap<Entities.Role, Models.Role>().ReverseMap();

            CreateMap<Entities.Transaction, Models.Transaction>()
              
                .ReverseMap();
           CreateMap<Entities.Premises, Models.PremisesProvider>()
                .ReverseMap();
            CreateMap<Entities.Premises, Models.Premises>()
                 .ReverseMap();

            CreateMap<Entities.Premises, Models.RegisterRequest>()
                .ForMember(dest => dest.PremisesName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.PremisesAddress, opts => opts.MapFrom(src => src.Address))
                .ForMember(dest => dest.PremisesTypeId, opts => opts.MapFrom(src => src.TypeId))
                .ReverseMap();

            CreateMap<Entities.Food, Models.FoodFarm>()
                .ForMember(dest => dest.CategoryName, opts => opts.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.Category.CategoryId))
                .ReverseMap();

            CreateMap<Entities.Transaction, Models.TransactionRequest>().ReverseMap();

            CreateMap<Entities.Category, Models.Category>()
               .ReverseMap();

            CreateMap<Entities.ProviderFood, Models.FoodProvider>()
                .ForMember(dest => dest.TreatmentId, opts => opts.MapFrom(src => src.TreatmentId))
                .ReverseMap();

            CreateMap<Entities.Food, Models.Food>()
               .ReverseMap();

            CreateMap<Entities.FoodDetailType, Models.Option>()
                 .ForMember(dest => dest.id, opts => opts.MapFrom(src => src.TypeId))
                 .ForMember(dest => dest.text, opts => opts.MapFrom(src => src.Name))
               .ReverseMap();

            CreateMap<Entities.Premises, Models.Option>()
                .ForMember(dest => dest.id, opts => opts.MapFrom(src => src.PremisesId))
                .ForMember(dest => dest.text, opts => opts.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<Entities.Transaction, Models.TransactionReponse.FarmGetTransaction>()
                .ForMember(dest => dest.Provider, opts => opts.MapFrom(src => src.Receiver.Name))
                .ForMember(dest => dest.ProviderId, opts => opts.MapFrom(src => src.Receiver.PremisesId))
                .ForMember(dest => dest.FoodName, opts => opts.MapFrom(src => src.Food.Category.Name))
                .ForMember(dest => dest.FoodBreed, opts => opts.MapFrom(src => src.Food.Breed))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.TransactionStatus.Status))
                .ReverseMap();

            CreateMap<Entities.Transaction, Models.TransactionReponse.ProviderGetTransaction>()
                .ForMember(dest => dest.Farm, opts => opts.MapFrom(src => src.Sender.Name))
                .ForMember(dest => dest.FoodName, opts => opts.MapFrom(src => src.Food.Category.Name))
                .ForMember(dest => dest.FoodBreed, opts => opts.MapFrom(src => src.Food.Breed))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.TransactionStatus.Status))
                .ReverseMap();

            CreateMap<Entities.Transaction, Models.TransactionReponse.ProviderGetSendTransaction>()
                .ForMember(dest => dest.Distributor, opts => opts.MapFrom(src => src.Receiver.Name))
                .ForMember(dest => dest.FoodName, opts => opts.MapFrom(src => src.Food.Category.Name))
                .ForMember(dest => dest.FoodBreed, opts => opts.MapFrom(src => src.Food.Breed))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.TransactionStatus.Status))
                .ReverseMap();

            CreateMap<Entities.ProviderFood, Models.CreateProviderFoodRequest>().ReverseMap();

            CreateMap<Entities.Treatment, Models.FoodRespone.TreatmentReponse>().ReverseMap();

            CreateMap<Entities.Treatment, Models.Option>()
                 .ForMember(dest => dest.id, opts => opts.MapFrom(src => src.TreatmentId))
                 .ForMember(dest => dest.text, opts => opts.MapFrom(src => src.Name))
               .ReverseMap();

        }
    }
}
