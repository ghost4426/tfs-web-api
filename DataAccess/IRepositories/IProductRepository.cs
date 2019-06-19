using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
   public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IList<Product>> GetAllProductAsync();

        Task<IList<Product>> FindAllProductByProviderAsync(int providerID);

        Task<int> CreateProductAsync(Product newProduct);
    }
}
