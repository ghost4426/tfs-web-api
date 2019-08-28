using ContractInteraction.FoodDataStorage;
using DTO.Models.FoodData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nethereum.RPC.Eth.DTOs;

namespace ContractInteraction.ContractServices
{
    public interface IContractServices
    {

        Task<string> AddNewFoodData(FoodData Food, string sender);

        Task<string> SaveFoodData(FoodData Food, string newData, string sender);

        Task<FoodData> GetFoodDataByID(long id);

        Task<string> GetTransactionInputByHashAsync(string transactionHash);

        Task SetInvalidData(long id);

        Task<List<Transaction>> GetContractTransaction();

        string DecodeData(string data, string function);
    }
}
