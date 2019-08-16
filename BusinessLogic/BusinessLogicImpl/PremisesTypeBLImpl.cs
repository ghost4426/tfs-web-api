using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using Models = DTO.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class PremisesTypeBLImpl: IPremisesTypeBL
    {
        private IPremisesTypeRepository _premisesTypeRepository;
        public PremisesTypeBLImpl(
            IPremisesTypeRepository premisesTypeRepository
            )
        {
            _premisesTypeRepository = premisesTypeRepository;
        }
        public async Task<PremisesType> GetById(int premisesId)
        {
            return await _premisesTypeRepository.GetByIdAsync(premisesId);
        }
    }
}
