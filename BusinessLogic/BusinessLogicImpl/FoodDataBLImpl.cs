using BusinessLogic.IBusinessLogic;
using ContractInteraction.Services;
using DTO.Models.FoodData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class FoodDataBLImpl : IFoodDataBL
    {
        private IService _service;

        public FoodDataBLImpl(IService service)
        {
            _service = service;
        }

        public async Task<string> CreateFood(FoodData Food)
        {
            return await _service.SaveFoodData(Food);
        }

        public Task<FoodData> GetFoodDataByID(long Id)
        {
            return _service.GetFoodDataByID(Id);
        }

        public async Task<string> SaveFoodData(FoodData Food)
        {
            return await _service.SaveFoodData(Food);
        }
    }
}
