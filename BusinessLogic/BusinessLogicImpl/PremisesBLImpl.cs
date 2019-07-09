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
        public Task<IList<Premises>> getAllProviderAsync()
        {
            return this._premisesRepository.getAllProviderAsync();
        }
    }
}
