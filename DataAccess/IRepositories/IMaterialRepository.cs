using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IMaterialRepository : IGenericRepository<Material>
    {
        Task<IEnumerable<Material>> GetMaterialByFarmerId(int FarmerId);
    }
}
