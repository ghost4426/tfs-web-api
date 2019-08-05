using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class PremisesBLImpl : IPremisesBL
    {
        private IPremisesRepository _premisesRepository;

        public PremisesBLImpl(IPremisesRepository premisesRepository)
        {
            _premisesRepository = premisesRepository;
        }

        public Task<IList<Premises>> getAllDistriburtorAsync(string keyword)
        {
            if(keyword == null)
            {
                keyword = "";
            }
            return this._premisesRepository.getAllDistributorAsync(keyword.ToLower());
        }

        public Task<IList<Premises>> getAllProviderAsync(string keyword)
        {
            if(keyword == null)
            {
                keyword = "";
            }
            return this._premisesRepository.getAllProviderAsync(keyword.ToLower());
        }
    }
}
