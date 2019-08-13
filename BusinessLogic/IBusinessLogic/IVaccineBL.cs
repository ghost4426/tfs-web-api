using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
    public interface IVaccineBL
    {
        void AddNewVaccineList(IList<Vaccine> vaccines, int premisesId, int userId);

        Task<IList<Vaccine>> GetVaccineListByPremisesId(int premisesId);

        Task<IList<Vaccine>> GetVaccineList();

        Task RemoveVaccineById(int feedingId, int userId);
    }
}
