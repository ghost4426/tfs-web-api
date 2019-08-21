using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using DTO.Models.Exception;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class PremisesBLImpl : IPremisesBL
    {
        private IPremisesRepository _premisesRepository;
        private IPremisesTypeRepository _premisesTypeRepository;

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

        public async Task<IList<Premises>> getAllPremisesAsync()
        {
            var result = await _premisesRepository.GetAllIncluding(p => p.PremisesType).OrderBy(p => p.PremisesId).ToListAsync();
            //foreach(var t in result)
            //{
            //    var type = _premisesTypeRepository.GetById(t.TypeId);
            //    t.PremisesType = type;
            //}
            return result;
        }

        public Task<IList<Premises>> getAllProviderAsync(string keyword)
        {
            if(keyword == null)
            {
                keyword = "";
            }
            return this._premisesRepository.getAllProviderAsync(keyword.ToLower());
        }
        public async Task<Premises> GetById(int premisesId)
        {
            return await this._premisesRepository.GetByIdAsync(premisesId);
        }

        public async Task updatePremisesStatus(int premisesId)
        {
            var premises = await _premisesRepository.GetByIdAsync(premisesId);
            if (premises == null)
            {
                throw new NotFoundException("Không tìm thấy cơ sở");
            }
            if (premises.IsActive)
            {
                premises.IsActive = false;
            }
            else { premises.IsActive = true; }
            await _premisesRepository.UpdateAsync(premises, true);
        }
    }
}
