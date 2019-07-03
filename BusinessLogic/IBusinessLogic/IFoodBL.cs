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

        Task<IEnumerable<Food>> getMatchedWithNumber(int distributorId);

        Task<IList<Categories>> getAllCategory();

        Task<IList<Food>> FindAllProductByFarmerAsync(int farmerID);

    }
}
