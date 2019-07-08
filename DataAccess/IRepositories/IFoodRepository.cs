using DTO.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
   public interface IFoodRepository : IGenericRepository<Food>
    {
        Task<IList<Food>> GetAllProductAsync();

        Task<IList<Food>> FindAllProductByProviderAsync(int providerID);

        Task<int> CreateProductAsync(Food newProduct);

        Task<IList<Food>> GetMatchedWithNumber(int distributorId);

        Task<IList<Food>> FindAllProductByFarmerAsync(int farmerID);
    }
}