using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class TreatmentBLImpl : ITreatmentBL
    {
        private ITreatmentRepository _treatmentRepos;

        public TreatmentBLImpl(ITreatmentRepository treatmentRepos)
        {
            _treatmentRepos = treatmentRepos;
        }

        public async Task CreateTreatment(Treatment treatment, List<string> treatmentProcess)
        {
           await _treatmentRepos.InsertAsync(treatment);
            foreach (var process in treatmentProcess)
            {
             await _treatmentRepos.InsertAsync(new Treatment()
                {
                    Name = process,
                    ParentTreatmentId = treatment.TreatmentId,
                    PremisesId = treatment.PremisesId
                });
            }   
        }
    }
}
