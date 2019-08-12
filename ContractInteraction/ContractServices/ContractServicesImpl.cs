using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ContractInteraction.FoodDataStorage;
using ContractInteraction.FoodDataStorage.ContractDefinition;
using DTO.Models.FoodData;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;

namespace ContractInteraction.ContractServices
{
    public class ContractServicesImpl : IContractServices
    {

        private FoodDataStorageService GetService()
        {
            var url = "http://202.78.227.98:8545";
            var privateKey = "4FEE435CDB6F678C03FD5C154631ADE103A265C1C1109847FFD0A6A5A9EF2CF6";
            var account = new Account(privateKey);
            var web3 = new Web3(account, url);
            var contractAddress = "0xe87272211fb5a2edc9c5cf279ee322cb13b82f2c";
            var service = new FoodDataStorageService(web3, contractAddress);
            return service;
        }

        public async Task<FoodData> GetFoodDataByID(long Id)
        {
            var service = GetService();
            var result = await service.GetDataByIdQueryAsync(new GetDataByIdFunction { Id = Id, Gas = 300000 });
            var setting = new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore };
            return JsonConvert.DeserializeObject<FoodData>(result, setting);
        }

        public async Task<string> SaveFoodData(FoodData FoodData)
        {
            var service = GetService();
            var setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string Data = JsonConvert.SerializeObject(FoodData, setting);
            var result = await service.SaveDataRequestAndWaitForReceiptAsync(
                    new SaveDataFunction { Data = Data, Id = FoodData.FoodId, Gas = 1000000 });
            return result.TransactionHash;

        }
       
    }
}
