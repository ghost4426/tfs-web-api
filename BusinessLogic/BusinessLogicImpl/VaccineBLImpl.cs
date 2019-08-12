using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class VaccineBLImpl : IVaccineBL
    {
        private IVaccineRepository _vaccineRepository;

        public VaccineBLImpl(IVaccineRepository vaccineRepository)
        {
            _vaccineRepository = vaccineRepository;
        }

        public async Task<IList<Vaccine>> GetVaccineListByPremisesId(int premisesId)
        {
            return await _vaccineRepository.FindAllAsync(t => t.PremisesId == premisesId && t.IsDelete == false);
        }

        public void AddNewVaccineList(IList<Vaccine> vaccines, int premisesId, int userId)
        {
            foreach (var vaccine in vaccines)
            {
                if (vaccine.VaccineId == 0)
                {
                    vaccine.CreateById = userId;
                    vaccine.UpdateById = userId;
                    vaccine.PremisesId = premisesId;
                    vaccine.UpdateDate = DateTime.Now;
                    _vaccineRepository.Insert(vaccine);
                }
            }
        }

        public async Task RemoveVaccineById(int vaccineId, int userId)
        {
            var feeding = _vaccineRepository.GetById(vaccineId);
            feeding.IsDelete = true;
            feeding.UpdateById = userId;
            feeding.UpdateDate = DateTime.Now;
            await _vaccineRepository.UpdateAsync(feeding);
        }

        public async  Task<IList<Vaccine>> GetVaccineList()
        {
            return await _vaccineRepository.FindAllAsync(t => t.IsDelete == false);
        }
    }
}
