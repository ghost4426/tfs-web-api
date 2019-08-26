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
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.CQS;
using Nethereum.Util;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Contracts;
using Nethereum.Contracts.Extensions;
namespace ContractInteraction.ContractServices
{
    public class ContractServicesImpl : IContractServices
    {
        //private string url = "http://locahost:8545";
        private readonly string url = "http://202.78.227.98:8545";
        private readonly string privateKey = "4FEE435CDB6F678C03FD5C154631ADE103A265C1C1109847FFD0A6A5A9EF2CF6";
        private readonly string contractAddress = "0x962a91275630d3fe036ed04f653fa430ee56cc30";

        private FoodDataStorageService GetService()
        {
            var account = new Account(privateKey);
            var web3 = new Web3(account, url);
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

        public async Task<string> AddNewFoodData(FoodData FoodData)
        {
            var service = GetService();
            var setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string Data = JsonConvert.SerializeObject(FoodData, setting);
            var result = await service.AddNewDataRequestAndWaitForReceiptAsync(
                    new AddNewDataFunction { Data = Data, Id = FoodData.FoodId, Gas = 1000000 });
     
            var transaction = GetTransactionByHashAsync(result.TransactionHash);
            var error = result.HasErrors();
            return result.TransactionHash;
        }

        public async Task<string> GetTransactionByHashAsync(string transactionHash)
        {
            var account = new Account(privateKey);
            var web3 = new Web3(account, url);
            var transaction = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(transactionHash);
            return transaction.Input;
        }


        public string DecodeData(string data)
        {
            var abi =
                @"[{""constant"":false,""inputs"":[{""name"":""id"",""type"":""uint256""},{""name"":""data"",""type"":""string""}],""name"":""addNewData"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""id"",""type"":""uint256""},{""name"":""data"",""type"":""string""}],""name"":""saveData"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""id"",""type"":""uint256""}],""name"":""setIsValid"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""id"",""type"":""uint256""}],""name"":""getDataById"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""}]";

            var account = new Account(privateKey);
            var web3 = new Web3(account, url);
            var eth = web3.Eth;
            var contract = eth.GetContract(abi, contractAddress);
            var testFunction = contract.GetFunction("saveData");
            var decode = testFunction.DecodeInput(data);
            return (string)decode[0].Result;
        }
    }
}
