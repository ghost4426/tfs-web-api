using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
   public interface ITreatmentBL
    {
        Task CreateTreatment(Treatment treatment, List<string> treatmentProcess);
       
    }
}
