using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IProviderFoodRepository:IGenericRepository<ProviderFood>
    {
        Task<IList<ProviderFood>> getAllFoodByProviderId(int providerId);
        Task<int> createProviderFood(ProviderFood providerFood);
    }
}
