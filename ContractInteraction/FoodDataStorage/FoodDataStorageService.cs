using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using ContractInteraction.FoodDataStorage.ContractDefinition;

namespace ContractInteraction.FoodDataStorage
{
    public partial class FoodDataStorageService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, FoodDataStorageDeployment foodDataStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<FoodDataStorageDeployment>().SendRequestAndWaitForReceiptAsync(foodDataStorageDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, FoodDataStorageDeployment foodDataStorageDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<FoodDataStorageDeployment>().SendRequestAsync(foodDataStorageDeployment);
        }

        public static async Task<FoodDataStorageService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, FoodDataStorageDeployment foodDataStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, foodDataStorageDeployment, cancellationTokenSource);
            return new FoodDataStorageService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public FoodDataStorageService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> SaveDataRequestAsync(SaveDataFunction saveDataFunction)
        {
             return ContractHandler.SendRequestAsync(saveDataFunction);
        }

        public Task<TransactionReceipt> SaveDataRequestAndWaitForReceiptAsync(SaveDataFunction saveDataFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(saveDataFunction, cancellationToken);
        }

        public Task<string> SaveDataRequestAsync(string data, BigInteger id)
        {
            var saveDataFunction = new SaveDataFunction();
                saveDataFunction.Data = data;
                saveDataFunction.Id = id;
            
             return ContractHandler.SendRequestAsync(saveDataFunction);
        }

        public Task<TransactionReceipt> SaveDataRequestAndWaitForReceiptAsync(string data, BigInteger id, CancellationTokenSource cancellationToken = null)
        {
            var saveDataFunction = new SaveDataFunction();
                saveDataFunction.Data = data;
                saveDataFunction.Id = id;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(saveDataFunction, cancellationToken);
        }

        public Task<string> GetDataByIdQueryAsync(GetDataByIdFunction getDataByIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetDataByIdFunction, string>(getDataByIdFunction, blockParameter);
        }

        
        public Task<string> GetDataByIdQueryAsync(BigInteger id, BlockParameter blockParameter = null)
        {
            var getDataByIdFunction = new GetDataByIdFunction();
                getDataByIdFunction.Id = id;
            
            return ContractHandler.QueryAsync<GetDataByIdFunction, string>(getDataByIdFunction, blockParameter);
        }
    }
}
