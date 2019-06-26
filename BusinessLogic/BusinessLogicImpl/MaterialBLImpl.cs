using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class MaterialBLImpl : IMaterialBL
    {
        public IMaterialRepository repos;

        public MaterialBLImpl(IMaterialRepository materialRepository)
        {
            if (materialRepository != null)
                this.repos = materialRepository;
        }
        public async Task<IEnumerable<Material>> GetMaterialByFarmerId(int FarmerId)
        {
            return await this.repos.GetMaterialByFarmerId(FarmerId);
        }
    }
}
