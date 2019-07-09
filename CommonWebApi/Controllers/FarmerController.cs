using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using Entities = DTO.Entities;
using DTO.Entities;
using System.Linq.Expressions;
using BusinessLogic.IBusinessLogic;
using Common.Utils;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmerController : ControllerBase
    {
        private IProductBL _productBL;
        private ITransactionBL _transactionBL;
        private IPremisesBL _premisesBL;
        private IAutoMapConverter<Models.CreateFoodRequest, Entities.Food> _mapCreateFoodRequestModelToEntity;
        private IAutoMapConverter<Models.TransactionRequest, Entities.Transaction> _mapCreateTransactionRequestModelToEntity;

        public FarmerController(IProductBL productBL, ITransactionBL transactionBL,IPremisesBL premisesBL
            , IAutoMapConverter<Models.CreateFoodRequest, Entities.Food> mapCreateFoodRequestModelToEntity
            , IAutoMapConverter<Models.TransactionRequest, Entities.Transaction> mapCreateTransactionRequestModelToEntity)
        {
            _productBL = productBL;
            _transactionBL = transactionBL;
            _premisesBL = premisesBL;
            _mapCreateFoodRequestModelToEntity = mapCreateFoodRequestModelToEntity;
            _mapCreateTransactionRequestModelToEntity = mapCreateTransactionRequestModelToEntity;
        }

        [HttpGet("getAllCategory")]
        public async Task<IList<Categories>> getAllCategory()
        {
            return await _productBL.getAllCategory();
        }

        [HttpGet("getByFarmer")]
        public async Task<IList<Food>> FindAllProductByFarmerAsync()
        {
            return await _productBL.FindAllProductByFarmerAsync(2);
        }

        [HttpPost("createFood")]
        public async Task<Models.ProductReponse.CreateProductReponse> CreateFood([FromBody]Models.CreateFoodRequest foodRequest)
        {
            Entities.Food food = _mapCreateFoodRequestModelToEntity.ConvertObject(foodRequest);
            //new Entities.Food() { CategoriesId = foodRequest.CategoriesId, FarmerId = foodRequest.FamerId };
            await _productBL.CreateProductAsync(food);
            var reponseModel = new Models.ProductReponse.CreateProductReponse()
            {
                ProductId = food.FoodId
            };
            return reponseModel;
        }

        [HttpPost("createTransaction")]
        public async Task<Models.TransactionReponse.CreateTransactionReponse> CreateTransaction([FromBody]Models.TransactionRequest transactionRequest)
        {
            Entities.Transaction transaction = _mapCreateTransactionRequestModelToEntity.ConvertObject(transactionRequest);
            await _transactionBL.CreateSellFoodTransactionAsync(transaction);
            var reponseModel = new Models.TransactionReponse.CreateTransactionReponse()
            {
                TransactionId = transaction.TransactionId
            };
            return reponseModel;
        }

        [HttpGet("getAllProvider")]
        public async Task<IList<Premises>> GetAllProvider()
        {
            return await _premisesBL.getAllProviderAsync();
        }
    }
}