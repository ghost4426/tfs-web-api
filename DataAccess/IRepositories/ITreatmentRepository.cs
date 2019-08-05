using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
   public interface ITreatmentRepository: IGenericRepository<Treatment>
    {
        Task<IList<Treatment>> getAllTreatmentById(int treatmentId);

        Task<IList<Treatment>> getAllTreatmentByPremisesId(int premisesId);

        Task<IList<int>> getTreatmentIdByParent(int treatmentId);
    }
}
