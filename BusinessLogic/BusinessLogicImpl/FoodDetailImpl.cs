using BusinessLogic.IBusinessLogic;
using Common.Constant;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class FoodDetailImpl : IFoodDetailBL
    {
        private IFoodDetailTypeRepository _foodDetailTypeRepos;

        public FoodDetailImpl(IFoodDetailTypeRepository foodDetailTypeRepos)
        {
            _foodDetailTypeRepos = foodDetailTypeRepos;
        }

        public async Task<IList<FoodDetailType>> GetFoodDetailTypeByPremises(string premisesType)
        {
            IList<FoodDetailType> foodDetails = null;
            switch (premisesType)
            {
                case PremisesTypeDataConstant.FARM:
                    foodDetails = await _foodDetailTypeRepos.FindAllAsync(f => 
                        f.TypeId == FoodDetailTypeDataConstant.ADD_FEEDING_ID ||
                        f.TypeId == FoodDetailTypeDataConstant.ADD_VACCINATION_ID ||
                        f.TypeId == FoodDetailTypeDataConstant.ADD_CERTIFICATION_ID);
                    break;
                case PremisesTypeDataConstant.PROVIDER:
                    ;
                    break;
                case PremisesTypeDataConstant.DISTRIBUTOR:
                    ;
                    break;
                default:
                    break;
            }
            return foodDetails;
        }
    }
}
