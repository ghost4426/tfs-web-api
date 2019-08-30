using BusinessLogic.IBusinessLogic;
using Common.Utils;
using ContractInteraction.ContractServices;
using DataAccess.IRepositories;
using Entities = DTO.Entities;
using Models = DTO.Models;
using DTO.Models.FoodData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Common.Constant;

namespace BusinessLogic.BusinessLogicImpl
{
    public class FoodDataBLImpl : IFoodDataBL
    {
        private readonly IContractServices _service;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IPremisesRepository _premesisRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFoodDetailRepository _foodDetailRepository;
        private readonly IMapper _mapper;

        public FoodDataBLImpl(IContractServices service,
            ITreatmentRepository treatmentRepository,
            IPremisesRepository premesisRepository,
            ICategoryRepository categoryRepository,
            IUserRepository userRepository,
            IFoodDetailRepository foodDetailRepository,
            IMapper mapper)
        {
            _service = service;
            _treatmentRepository = treatmentRepository;
            _premesisRepository = premesisRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _foodDetailRepository = foodDetailRepository;
            _mapper = mapper;
        }

        public async Task<string> CreateFood(Entities.Food newFood, int farmId, int createById)
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

            return await _service.AddNewFoodData(FoodData, _userRepository.GetById(createById).Username);
        }


        public async Task<string> AddTreatment(long foodId, int treamentId, int providerId, int createById)
        {
            var Treament = await _treatmentRepository.FindAllAsync(t => t.TreatmentParentId == treamentId);

            var FoodData = await GetFoodDataByID(foodId);
            if (!ValidateFoodData(FoodData, foodId))
            {
                throw new InvalidDataException(MessageConstant.IVALID_DATA);
            }
            List<string> TreatmentProcess = new List<string>();
            for (int i = 0; i < Treament.Count; i++)
            {
                TreatmentProcess.Add(Treament[i].Name);
            }
            foreach (var p in FoodData.Providers)
            {
                if (p.ProviderId == providerId)
                {
                    p.Treatment = new Treatments()
                    {
                        TreatmentDate = DateTime.Now,
                        TreatmentProcess = new List<string>()
                    };
                    p.Treatment.TreatmentProcess = TreatmentProcess;
                }
            }
            FoodData.Providers[0].Treatment.TreatmentProcess = TreatmentProcess;
            var setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string newData = JsonConvert.SerializeObject(TreatmentProcess, setting);
            return await _service.SaveFoodData(FoodData, newData, _userRepository.GetById(createById).Username);
        }


        public Task<FoodData> GetFoodDataByID(long Id)
        {
            return _service.GetFoodDataByID(Id);
        }



        public async Task<string> AddFeedings(long foodId, List<Models.AddFeedingInfoToFoodDataRequest> feedings, int createById)
        {
            var FoodData = await GetFoodDataByID(foodId);
            if (!ValidateFoodData(FoodData, foodId))
            {
                throw new InvalidDataException(MessageConstant.IVALID_DATA);
            }
            foreach (var feeding in feedings)
                {
                    if (FoodData.Farm.Feedings == null)
                    {
                        FoodData.Farm.Feedings = new List<string>();
                    }
                    FoodData.Farm.Feedings.Add(feeding.FeedingName);
                }
                var setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                string newData = JsonConvert.SerializeObject(feedings, setting);
                return await _service.SaveFoodData(FoodData, newData, _userRepository.GetById(createById).Username);
        }

        public async Task<string> AddVaccination(long foodId, List<Models.AddVaccineInfoToFoodDataRequest> vaccines, int createById)
        {
            var FoodData = await GetFoodDataByID(foodId);
            if (!ValidateFoodData(FoodData, foodId))
            {
                throw new InvalidDataException(MessageConstant.IVALID_DATA);
            }
            foreach (var vaccine in vaccines)
            {
                var vaccineData = new VaccineData()
                {
                    VaccinationType = vaccine.VaccineName,
                    VaccinationDate = vaccine.VaccineDate
                };
                if (FoodData.Farm.Vaccinations == null)
                {
                    FoodData.Farm.Vaccinations = new List<VaccineData>();
                }

                FoodData.Farm.Vaccinations.Add(vaccineData);
            }

            var setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string newData = JsonConvert.SerializeObject(vaccines, setting);
            return await _service.SaveFoodData(FoodData, newData, _userRepository.GetById(createById).Username);
        }


        public async Task<string> ProviderAddCertification(long foodId, int providerId, string certificationNumber, int createById)
        {
            var FoodData = await GetFoodDataByIDAndProviderID(foodId, providerId);
            if (!ValidateFoodData(FoodData, foodId))
            {
                throw new InvalidDataException(MessageConstant.IVALID_DATA);
            }
            FoodData.Providers[0].CertificationNumber = certificationNumber;
            FoodData.Providers[0].CertificationDate = DateTime.Now;

            var setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string newData = JsonConvert.SerializeObject(certificationNumber, setting);
            return await _service.SaveFoodData(FoodData, newData, _userRepository.GetById(createById).Username);
        }

        public async Task<string> Packaging(long foodId, Packaging packaging, int providerId, int createById)
        {
            packaging.PackagingDate = DateTime.Now;

            var FoodData = await GetFoodDataByID(foodId);
            if (!ValidateFoodData(FoodData, foodId))
            {
                throw new InvalidDataException(MessageConstant.IVALID_DATA);
            }
            foreach (var p in FoodData.Providers)
            {
                if (p.ProviderId == providerId)
                {
                    p.Packaging = packaging;
                }
            }
            var setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string newData = JsonConvert.SerializeObject(packaging, setting);
            return await _service.SaveFoodData(FoodData, newData, _userRepository.GetById(createById).Username);
        }

        public async Task<string> AddProvider(long foodId, int providerId, int createById)
        {
            var Premises = _premesisRepository.GetById(providerId);
            var FoodData = await GetFoodDataByID(foodId);
            if (!ValidateFoodData(FoodData, foodId))
            {
                throw new InvalidDataException(MessageConstant.IVALID_DATA);
            }
            if (FoodData.Providers == null)
            {
                FoodData.Providers = new List<Provider>();
            }
            FoodData.Providers.Add(_mapper.Map<Provider>(Premises));

            var setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string newData = JsonConvert.SerializeObject(_mapper.Map<Provider>(Premises), setting);
            return await _service.SaveFoodData(FoodData, newData, _userRepository.GetById(createById).Username);
        }

        public async Task<IList<string>> GetFeedingsById(int foodId)
        {
            var FoodData = await GetFoodDataByID(foodId);
            return FoodData.Farm.Feedings;
        }

        public async Task<IList<VaccineData>> GetVaccinsById(int foodId)
        {
            var FoodData = await GetFoodDataByID(foodId);
            return FoodData.Farm.Vaccinations;
        }

        public async Task<FoodData> GetFoodDataByIDAndProviderID(long foodId, int providerId)
        {
            FoodData food = await _service.GetFoodDataByID(foodId);
            food.Providers = food.Providers.Where(x => x.ProviderId == providerId).ToList();
            return food;
        }

        public async Task<string> AddDistributor(long foodId, int distributorId, int createById)
        {
            var Premises = _premesisRepository.GetById(distributorId);
            var FoodData = await GetFoodDataByID(foodId);
            if (!ValidateFoodData(FoodData, foodId))
            {
                throw new InvalidDataException(MessageConstant.IVALID_DATA);
            }
            //FoodData.Provider = _mapper.Map<Provider>(Premises);
            if (FoodData.Distributors == null)
            {
                FoodData.Distributors = new List<Distributor>();
            }
            FoodData.Distributors.Add(_mapper.Map<Distributor>(Premises));

            var setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string newData = JsonConvert.SerializeObject(_mapper.Map<Distributor>(Premises), setting);
            return await _service.SaveFoodData(FoodData, newData, _userRepository.GetById(createById).Username);
        }
        public async Task<FoodData> GetFoodDataByIDAndProviderIDAndDistributorID(long foodId, int providerId, int distributorId)
        {
            FoodData food = await _service.GetFoodDataByID(foodId);
            food.Providers = food.Providers.Where(x => x.ProviderId == providerId).ToList();
            food.Distributors = food.Distributors.Where(x => x.DistributorId == distributorId).ToList();
            return food;
        }

        public async Task<string> AddCertification(long foodId, string certificationNumber, int createById)
        {
            var FoodData = await GetFoodDataByID(foodId);
            if (!ValidateFoodData(FoodData, foodId))
            {
                throw new InvalidDataException(MessageConstant.IVALID_DATA);
            }
            FoodData.Farm.CertificationNumber = certificationNumber;
            FoodData.Farm.CertificationDate = DateTime.Now;
            var setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string newData = JsonConvert.SerializeObject(certificationNumber, setting);
            return await _service.SaveFoodData(FoodData, newData, _userRepository.GetById(createById).Username);
        }

        private bool ValidateFoodData(FoodData foodData, long foodIdl)
        {
            int foodId = (int)foodIdl;
            var detail = _foodDetailRepository.GetIQueryable().Where(f => f.FoodId == foodId).OrderByDescending(f => f.CreateDate).Take(1).SingleOrDefault();
            var function = "";
            if (detail.TypeId == 1)
            {
                function = "addNewData";
            }
            else
            {
                function = "saveData";
            }
            var setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            var FoodDataJson = JsonConvert.SerializeObject(foodData, setting);
            var transactionFoodData = _service.DecodeData(_service.GetTransactionInputByHashAsync(detail.TransactionHash).Result, function);
            var isMatch = FoodDataJson.CompareTo(transactionFoodData) == 0 ? true : false;
            if (!isMatch)
            {
                //_service.SetInvalidData(foodIdl);
            }
            return true;
        }
    }
}
