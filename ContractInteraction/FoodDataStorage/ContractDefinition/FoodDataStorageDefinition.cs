using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace ContractInteraction.FoodDataStorage.ContractDefinition
{


    public partial class FoodDataStorageDeployment : FoodDataStorageDeploymentBase
    {
        public FoodDataStorageDeployment() : base(BYTECODE) { }
        public FoodDataStorageDeployment(string byteCode) : base(byteCode) { }
    }

    public class FoodDataStorageDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b506102ff806100206000396000f3fe608060405234801561001057600080fd5b50600436106100365760003560e01c8063545350b21461003b578063f2722a9f146100e5575b600080fd5b6100e36004803603604081101561005157600080fd5b81019060208101813564010000000081111561006c57600080fd5b82018360208201111561007e57600080fd5b803590602001918460018302840111640100000000831117156100a057600080fd5b91908080601f0160208091040260200160405190810160405280939291908181526020018383808284376000920191909152509295505091359250610177915050565b005b610102600480360360208110156100fb57600080fd5b5035610199565b6040805160208082528351818301528351919283929083019185019080838360005b8381101561013c578181015183820152602001610124565b50505050905090810190601f1680156101695780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b600081815260208181526040909120835161019492850190610238565b505050565b6000818152602081815260409182902080548351601f600260001961010060018616150201909316929092049182018490048402810184019094528084526060939283018282801561022c5780601f106102015761010080835404028352916020019161022c565b820191906000526020600020905b81548152906001019060200180831161020f57829003601f168201915b50505050509050919050565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061027957805160ff19168380011785556102a6565b828001600101855582156102a6579182015b828111156102a657825182559160200191906001019061028b565b506102b29291506102b6565b5090565b6102d091905b808211156102b257600081556001016102bc565b9056fea165627a7a72305820e5e18446d7b4233f5afc6d030613082af6c4cd612b8d0ed133671d186a365bd90029";
        public FoodDataStorageDeploymentBase() : base(BYTECODE) { }
        public FoodDataStorageDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class SaveDataFunction : SaveDataFunctionBase { }

    [Function("saveData")]
    public class SaveDataFunctionBase : FunctionMessage
    {
        [Parameter("string", "data", 1)]
        public virtual string Data { get; set; }
        [Parameter("uint256", "id", 2)]
        public virtual BigInteger Id { get; set; }
    }

    public partial class GetDataByIdFunction : GetDataByIdFunctionBase { }

    [Function("getDataById", "string")]
    public class GetDataByIdFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "id", 1)]
        public virtual BigInteger Id { get; set; }
    }



    public partial class GetDataByIdOutputDTO : GetDataByIdOutputDTOBase { }

    [FunctionOutput]
    public class GetDataByIdOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }
}
