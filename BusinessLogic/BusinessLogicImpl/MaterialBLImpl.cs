using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class MaterialBLImpl : IMaterialBL
    {
        public IMaterialRepository _materialRepos;

        public MaterialBLImpl(IMaterialRepository materialRepos)
        {
                _materialRepos = materialRepos;
        }
        public async Task<IEnumerable<Material>> GetMaterialByFarmerId(int FarmerId)
        {
            return await this._materialRepos.GetMaterialByFarmerId(FarmerId);
        }
    }
}
