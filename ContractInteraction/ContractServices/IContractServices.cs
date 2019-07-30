using ContractInteraction.FoodDataStorage;
using DTO.Models.FoodData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContractInteraction.ContractServices
{
    public interface IContractServices
    {

        Task<string> SaveFoodData(FoodData Food);

        Task<FoodData> GetFoodDataByID(long id);
    }
}
