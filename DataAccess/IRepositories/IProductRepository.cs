using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
   public interface IProductRepository : IGenericRepository<Food>
    {
        Task<IList<Food>> GetAllProductAsync();

        Task<IList<Food>> FindAllProductByProviderAsync(int providerID);

        Task<int> CreateProductAsync(Food newProduct);
        Task<IEnumerable<Food>> GetMatchedWithNumber(int distributorId);
    }
}