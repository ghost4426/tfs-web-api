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
        private ITransactionRepository _transactionRepos;

        public PremisesBLImpl(IPremisesRepository premisesRepository, ITransactionRepository transactionRepository)
        {
            _premisesRepository = premisesRepository;
            _transactionRepos = transactionRepository;
        }

        public async Task<IList<Premises>> getAllDistriburtorAsync(string keyword, int foodId)
        {
            if(keyword == null)
            {
                keyword = "";
            }
            var result = await _premisesRepository.getAllProviderAsync(keyword.ToLower());
            var distributor = await _transactionRepos.FindAllAsync(x => x.FoodId == foodId);
            var fin = result.Where(x => !distributor.Any(x2 => x2.ReceiverId == x.PremisesId));
            return fin.ToList();
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

        public async Task<IList<Premises>> getAllProviderAsync(string keyword, int foodId)
        {
            if(keyword == null)
            {
                keyword = "";
            }
            var result = await _premisesRepository.getAllProviderAsync(keyword.ToLower());
            var provider = await _transactionRepos.FindAllAsync(x => x.FoodId == foodId);
            var fin = result.Where(x => !provider.Any(x2 => x2.ReceiverId == x.PremisesId));
            return fin.ToList();
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
