using Common.Enum;
using DTO.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
    public interface IFoodBL
    {
        Task<IList<Food>> GetAllProductAsync();

        Task<IList<Food>> FindAllProductByProviderAsync(int providerID);

        Task<int> CreateProductAsync(Food newProduct);

        Task<IList<Food>> getMatchedWithNumber(int distributorId);

        Task<IList<Category>> getAllCategory();

        Task<IList<Food>> FindAllProductByFarmerAsync(int farmerID);

        Task AddDetail(long foodId, EFoodDetailType type);

        Task<IList<ProviderFood>> getAllFoodByProviderId(int providerId);

        Task<int> createProviderFood(ProviderFood newProviderFood);

        Task UpdateFoodTreatment(Food food, int foodId);

        Task<Food> getFoodById(int foodId);

        Task<IList<DistributorFood>> getAllFoodByDistributorId(int distributorId);

        Task<int> createDistributorFood(DistributorFood newDistributorFood);
    }
}
