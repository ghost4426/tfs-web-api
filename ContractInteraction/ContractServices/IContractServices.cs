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

        Task<string> AddNewFoodData(FoodData Food);

        Task<string> SaveFoodData(FoodData Food);

        Task<FoodData> GetFoodDataByID(long id);

        Task<string> GetTransactionByHashAsync(string transactionHash);

        string DecodeData(string data);
    }
}
