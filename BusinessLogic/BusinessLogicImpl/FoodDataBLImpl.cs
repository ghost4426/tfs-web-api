using BusinessLogic.IBusinessLogic;
using Common.Utils;
using ContractInteraction.Services;
using DataAccess.IRepositories;
using Entities = DTO.Entities;
using DTO.Models.FoodData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BusinessLogic.BusinessLogicImpl
{
    public class FoodDataBLImpl : IFoodDataBL
    {
        private IService _service;
        private ITreatmentRepository _treatmentRepository;
        private IPremesisRepository _premesisRepository;
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public FoodDataBLImpl(IService service,
            ITreatmentRepository treatmentRepository,
            IPremesisRepository premesisRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _service = service;
            _treatmentRepository = treatmentRepository;
            _premesisRepository = premesisRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<string> CreateFood(Entities.Food newFood, int farmId)
        {
            var Premises = _premesisRepository.GetById(farmId);
            var Cat = _categoryRepository.GetById(newFood.CategoryId);
            var FoodData = new FoodData()
            {
                FoodId = newFood.FoodId,
                Breed = newFood.Breed,
                Category = Cat.Name,
                Farm = _mapper.Map<Farm>(Premises),
                StartedDate = DateTime.Now
            };

            return await SaveFoodData(FoodData);
        }


        public async Task<string> AddTreatment(long foodId, int treamentId)
        {
            var Treament = await _treatmentRepository.FindAllAsync(t => t.TreatmentParentId == treamentId);
            var FoodData = await GetFoodDataByID(foodId);
            List<string> TreatmentProcess = new List<string>();
            for (int i = 0; i < Treament.Count; i++)
            {
                TreatmentProcess.Add(Treament[i].Name);
            }
            FoodData.Provider.Treatment.TreatmentProcess = TreatmentProcess;
            FoodData.Provider.Treatment.TreatmentDate = DateTime.Now;

            return await SaveFoodData(FoodData);
        }


        public Task<FoodData> GetFoodDataByID(long Id)
        {
            return _service.GetFoodDataByID(Id);
        }

        public async Task<string> SaveFoodData(FoodData Food)
        {
            return await _service.SaveFoodData(Food);
        }

        public async Task<string> AddFeedings(long foodId, List<string> feedings)
        {
            var FoodData = await GetFoodDataByID(foodId);
            if(FoodData.Farm.Feedings == null)
            {
                FoodData.Farm.Feedings = new List<string>();
            }
            FoodData.Farm.Feedings.AddRange(feedings);

            return await SaveFoodData(FoodData);
        }

        public async Task<string> AddVaccination(long foodId, string vaccinationType)
        {
            var FoodData = await GetFoodDataByID(foodId);
            var vaccin = new Vaccination()
            {
                VaccinationType = vaccinationType,
                VaccinationDate = DateTime.Now
            };

            if(FoodData.Farm.Vaccinations == null)
            {
                FoodData.Farm.Vaccinations = new List<Vaccination>();
            }

            FoodData.Farm.Vaccinations.Add(vaccin);

            return await SaveFoodData(FoodData);
        }

        public async Task<string> AddCertification(long foodId, string certificationNumber)
        {
            var FoodData = await GetFoodDataByID(foodId);

            FoodData.Farm.CertificationNumber = certificationNumber;
            FoodData.Farm.CertificationDate = DateTime.Now;

            return await SaveFoodData(FoodData);
        }

        public async Task<string> Packaging(long foodId, Packaging packaging)
        {
            packaging.PackagingDate = DateTime.Now;

            var FoodData = await GetFoodDataByID(foodId);
            FoodData.Provider.Packaging = packaging;

            return await SaveFoodData(FoodData);
        }

        public async Task<string> AddProvider(long foodId, int providerId)
        {
            var Premises = _premesisRepository.GetById(providerId);
            var FoodData = await GetFoodDataByID(foodId);

            FoodData.Provider = _mapper.Map<Provider>(Premises);

            return await SaveFoodData(FoodData);
        }
    }
}
