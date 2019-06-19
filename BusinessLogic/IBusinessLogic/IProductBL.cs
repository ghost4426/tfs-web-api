using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
    public interface IProductBL
    {
        Task<IList<Product>> GetAllProductAsync();

        Task<IList<Product>> FindAllProductByProviderAsync(int providerID);

        Task<int> CreateProductAsync(Product newProduct);
    }
}
