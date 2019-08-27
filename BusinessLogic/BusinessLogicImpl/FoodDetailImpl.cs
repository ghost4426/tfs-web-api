using BusinessLogic.IBusinessLogic;
using Common.Constant;
using DataAccess.IRepositories;
using DTO.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class FoodDetailImpl : IFoodDetailBL
    {
        private IFoodDetailTypeRepository _foodDetailTypeRepos;
        private IFoodDetailRepository _foodDetailRepository;

        public FoodDetailImpl(
            IFoodDetailTypeRepository foodDetailTypeRepos
            ,IFoodDetailRepository foodDetailRepository)
        {
            _foodDetailTypeRepos = foodDetailTypeRepos;
            _foodDetailRepository = foodDetailRepository;
        }

        public async Task<IList<FoodDetailType>> GetFoodDetailTypeByPremises(string premisesType)
        {
            IList<FoodDetailType> foodDetails = null;
            switch (premisesType)
            {
                case PremisesTypeDataConstant.FARM:
                    foodDetails = await _foodDetailTypeRepos.FindAllAsync(f =>
                        f.TypeId == FoodDetailTypeDataConstant.ADD_FEEDING_ID ||
                        f.TypeId == FoodDetailTypeDataConstant.ADD_VACCINATION_ID); 
                    break;
                case PremisesTypeDataConstant.PROVIDER:
                    foodDetails = await _foodDetailTypeRepos.FindAllAsync(f =>
                    f.TypeId == FoodDetailTypeDataConstant.ADD_TREATMENT_ID ||
                    f.TypeId == FoodDetailTypeDataConstant.ADD_PACKAGING_ID);
                    break;
                case PremisesTypeDataConstant.DISTRIBUTOR:
                    ;
                    break;
                default:
                    break;
            }
            return foodDetails;
        }

        public async Task<IList<FoodDetail>> GetFoodDetail()
        {
            return await _foodDetailRepository.GetAllIncluding(f => f.CreateBy, f => f.Type).OrderByDescending(f => f.CreateDate).ToListAsync();
        }
    }
}
