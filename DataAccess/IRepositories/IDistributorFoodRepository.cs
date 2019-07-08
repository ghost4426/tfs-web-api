using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IDistributorFoodRepository : IGenericRepository<DistributorFood>
    {
        Task<IList<DistributorFood>> GetDistributorFoods(int premisesId);
    }
}
