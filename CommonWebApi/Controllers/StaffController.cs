using System;
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
    public class StaffController : ControllerBase
    {
        private IMaterialBL _materialBL;
        private IProductBL _productBL;


        public StaffController(IProductBL ProductBL, IMaterialBL MaterialBL)
        {
           _materialBL = MaterialBL;
            _productBL = ProductBL;
        }


        // GET api/values
        [HttpGet("getByProvider")]
        public async Task<IList<Product>> FindAllProductByProviderAsync(int providerID)
        {
            return await _productBL.FindAllProductByProviderAsync(providerID);
        }

        [HttpPost("createProduct")]
        public async Task<Models.ProductReponse.CreateProductReponse> CreateProduct([FromBody] Product productModel)
        {
            Entities.Product product = new Entities.Product() { Name = productModel.Name, CategoriesId = productModel.CategoriesId, ProviderUserId = productModel.ProviderUserId };
            await _productBL.CreateProductAsync(product);
            var reponseModel = new Models.ProductReponse.CreateProductReponse()
            {
                ProductId = product.Id
            };
            return reponseModel;
        }

        [HttpGet("getProductMatched/{distributorId}")]
        public async Task<IEnumerable<Product>> getMatchedWithNumber(int distributorId)
        {
            return await _productBL.getMatchedWithNumber(distributorId);
        }

        [HttpGet("getMaterialMatched/{FarmerId}")]
        public async Task<IEnumerable<Material>> GetMaterialByFarmerId(int FarmerId)
        {
            return await MaterialBL.GetMaterialByFarmerId(FarmerId);
        }

    }
}