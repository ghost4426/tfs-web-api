using BusinessLogic.IBusinessLogic;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Utils;
using DataAccess.IRepositories;

namespace BusinessLogic.BusinessLogicImpl
{
    public class ProductBLmpl : IProductBL
    {
        private IProductRepository repo;

        public ProductBLmpl(IProductRepository productRepository)
        {
            if(productRepository != null)
            {
                this.repo = productRepository;
            }
        }
        public async Task<IList<Product>> GetAllProduct()
        {
            return await this.repo.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> getMatchedWithNumber(int distributorId)
        {
            return await this.repo.GetMatchedWithNumber(distributorId);
        }
    }
}
