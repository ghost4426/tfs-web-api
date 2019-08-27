using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
    public interface IPremisesBL
    {
        Task<IList<Premises>> getAllProviderAsync(string keyword, int foodId, int premisesId);
        Task<Premises> GetById(int premisesId);
        Task<IList<Premises>> getAllDistriburtorAsync(string keyword, int foodId, int premises);
        Task<IList<Premises>> getAllPremisesAsync();
        Task updatePremisesStatus(int premisesId);
    }
}
