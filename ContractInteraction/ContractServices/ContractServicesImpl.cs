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
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using DTO.Models.Exception;
using Common.Constant;

namespace ContractInteraction.ContractServices
{
    public class ContractServicesImpl : IContractServices
    {
        //private string url = "http://locahost:8545";
        private readonly string url = "http://202.78.227.98:8545";
        private readonly string privateKey = "4FEE435CDB6F678C03FD5C154631ADE103A265C1C1109847FFD0A6A5A9EF2CF6";
        private readonly string contractAddress = "0x724c2ac2e8d25da2664f1dc67f1d8ed3d67651bd";

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
            if (result == "Data Invalid")
            {
                throw new InvalidDataException(MessageConstant.IVALID_DATA);
            }
            var setting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            return JsonConvert.DeserializeObject<FoodData>(result, setting);
        }

        public async Task<string> SaveFoodData(FoodData FoodData, string newData, string sender)
        {
            var service = GetService();
            var setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string Data = JsonConvert.SerializeObject(FoodData, setting);
            var result = await service.SaveDataRequestAndWaitForReceiptAsync(
                    new SaveDataFunction { Data = Data, NewData = newData, Sender = sender, Id = FoodData.FoodId, Gas = 1000000 });
            if (result.HasErrors() ?? false)
            {
                throw new Exception("Lưu không thành công");
            }
            return result.TransactionHash;

        }

        public async Task<string> AddNewFoodData(FoodData FoodData, string sender)
        {
            var service = GetService();
            var setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string Data = JsonConvert.SerializeObject(FoodData, setting);
            var result = await service.AddNewDataRequestAndWaitForReceiptAsync(
                    new AddNewDataFunction { Data = Data, Id = FoodData.FoodId, Sender = sender, Gas = 1000000 });
            return result.TransactionHash;
        }



        public async Task<string> GetTransactionInputByHashAsync(string transactionHash)
        {
            var account = new Account(privateKey);
            var web3 = new Web3(account, url);
            var transaction = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(transactionHash);
            return transaction.Input;
        }


        public string DecodeData(string data, string function)
        {
            var abi =
                @"[{""constant"":false,""inputs"":[{""name"":""id"",""type"":""uint256""},{""name"":""data"",""type"":""string""},{""name"":""sender"",""type"":""string""}],""name"":""addNewData"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""id"",""type"":""uint256""},{""name"":""data"",""type"":""string""},{""name"":""sender"",""type"":""string""},{""name"":""newData"",""type"":""string""}],""name"":""saveData"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""id"",""type"":""uint256""}],""name"":""setIsValid"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""id"",""type"":""uint256""}],""name"":""getDataById"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""}]";
            var account = new Account(privateKey);
            var web3 = new Web3(account, url);
            var eth = web3.Eth;
            var contract = eth.GetContract(abi, contractAddress);
            var testFunction = contract.GetFunction(function);
            var decode = testFunction.DecodeInput(data);
            return (string)decode[1].Result;
        }

        public async Task<List<Transaction>> GetContractTransaction()
        {
            var account = new Account(privateKey);
            var web3 = new Web3(account, url);
            var currentBlockNumber = web3.Eth.Blocks.GetBlockNumber.SendRequestAsync().Result.Value;
            List<Transaction> transactions = new List<Transaction>();
            for (BigInteger i = 0; i < currentBlockNumber; i++)
            {
                var blockTransaction = await web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(new HexBigInteger(i));
                foreach (var tran in blockTransaction.Transactions)
                {
                    if (tran.To == contractAddress)
                    {
                        transactions.Add(tran);
                    }
                }
            }
            return transactions;
        }

        public async Task SetInvalidData(long id)
        {
            //var service = GetService();
            //var result = await service.SetIsValidRequestAndWaitForReceiptAsync(new SetIsValidFunction { Id = id, Gas = 1000000 });
            //if (result.HasErrors() ?? false)
            //{
            //    throw new Exception("Xử lí thất bại");
            //}
        }
    }
}
