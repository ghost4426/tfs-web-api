using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IPremisesRepository: IGenericRepository<Premises>
    {
        Task<IList<Premises>> getAllProviderAsync(string keyword);

        Task<IList<Premises>> getAllDistributorAsync(string keyword);
    }
}
