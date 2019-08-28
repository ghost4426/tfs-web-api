using DTO.Entities;
using DTO.Models.FoodData;
using Models = DTO.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
   public interface IFoodDataBL
    {

        Task<string> CreateFood(Food newFood, int farmId, int createById);

        Task<string> AddFeedings(long foodId, List<Models.AddFeedingInfoToFoodDataRequest> addFeedingInfos, int createById);

        Task<string> AddVaccination(long foodId, List<Models.AddVaccineInfoToFoodDataRequest> addVaccineInfos, int createById);

        Task<string> AddCertification(long foodId, string certificationNumber, int createById);

        Task<string> ProviderAddCertification(long foodId, int providerId, string certificationNumber, int createById);

        Task<string> AddProvider(long foodId, int providerId, int createById);

        Task<string> AddDistributor(long foodId, int distributorId, int createById);

        Task<string> AddTreatment(long foodId, int treamentId, int providerId, int createById);

        Task<string> Packaging(long foodId, Packaging packaging, int providerId, int createById);

        Task<FoodData> GetFoodDataByID(long id);

        Task<IList<string>> GetFeedingsById(int foodId);

        Task<IList<Models.FoodData.VaccineData>> GetVaccinsById(int foodId);

        Task<FoodData> GetFoodDataByIDAndProviderID(long foodId, int providerId);

        Task<FoodData> GetFoodDataByIDAndProviderIDAndDistributorID(long foodId, int providerId,int distributorId);

    }
}
