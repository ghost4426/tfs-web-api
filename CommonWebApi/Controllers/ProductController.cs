﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.IBusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using Entities = DTO.Entities;
using DTO.Entities;
using System.Linq.Expressions;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductBL _productBL;

        public ProductController(IProductBL productBL)
        {
            _productBL = productBL;
        }

        // GET api/values
        [HttpGet]
       public async Task<IList<Product>> GetAllProduct()
        {
            return await _productBL.GetAllProductAsync();
        }

        // GET api/values
        [HttpGet("getByProvider")]
        public async Task<IList<Product>> FindAllProductByProviderAsync() 
        {
            int userId = Int32.Parse(User.Claims.First(c => c.Type == "UserID").Value);

            return await _productBL.FindAllProductByProviderAsync(userId);
        }

        [HttpGet("testgetByProvider")]
        public async Task<IList<Product>> TestFindAllProductByProviderAsync()
        {
            return await _productBL.FindAllProductByProviderAsync(3);
        }

        [HttpPost("createProduct")]
        public async Task<Models.ProductReponse.CreateProductReponse> CreateProduct([FromBody]Models.Products productModel)
        {
            Entities.Product product = new Entities.Product() { Name = productModel.Name, CategoriesId = productModel.CategoriesId, ProviderUserId = productModel.ProviderUserId };
            await _productBL.CreateProductAsync(product);
            var reponseModel = new Models.ProductReponse.CreateProductReponse()
            {
                ProductId = product.Id
            };
            return reponseModel;
        }

        [HttpGet("getAllCategory")]
        public async Task<IList<Categories>> getAllCategory()
        {
            return await _productBL.getAllCategory();
        }
        
        [HttpGet("getProductMatched/{distributorId}")]
        public async Task<IEnumerable<Product>> getMatchedWithNumber(int distributorId)
        {
            return await _productBL.getMatchedWithNumber(distributorId);
        }
    }
}