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

        Task<string> SaveFoodData(FoodData food);

        Task<string> CreateFood(Food newFood, int farmId);

        Task<string> AddFeedings(long foodId, List<string> feedings);

        Task<string> AddVaccination(long foodId, List<Models.AddVaccineInfoToFoodDataRequest> addVaccineInfos);

        Task<string> AddCertification(long foodId, string certificationNumber);

        Task<string> ProviderAddCertification(long foodId, int providerId, string certificationNumber);

        Task<string> AddProvider(long foodId, int providerId);

        Task<string> AddDistributor(long foodId, int distributorId);

        Task<string> AddTreatment(long foodId, int treamentId, int providerId);

        Task<string> Packaging(long foodId, Packaging packaging, int providerId);

        Task<FoodData> GetFoodDataByID(long id);

        Task<IList<string>> GetFeedingsById(int foodId);

        Task<IList<Models.FoodData.VaccineData>> GetVaccinsById(int foodId);

        Task<FoodData> GetFoodDataByIDAndProviderID(long foodId, int providerId);
        Task<FoodData> GetFoodDataByIDAndProviderIDAndDistributorID(long foodId, int providerId,int distributorId);

    }
}
