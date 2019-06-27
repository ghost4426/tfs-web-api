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
        private IMaterialBL MaterialBL;
        private IProductBL Bl;


        public StaffController(IProductBL ProductBL, IMaterialBL MaterialBL)
        {
            this.MaterialBL = MaterialBL;
            this.Bl = ProductBL;
        }


        // GET api/values
        [HttpGet("getByProvider")]
        public async Task<IList<Product>> FindAllProductByProviderAsync(int providerID)
        {
            return await Bl.FindAllProductByProviderAsync(providerID);
        }

        [HttpPost("createProduct")]
        public async Task<Models.ProductReponse.CreateProductReponse> CreateProduct([FromBody] Product productModel)
        {
            Entities.Product product = new Entities.Product() { Name = productModel.Name, CategoriesId = productModel.CategoriesId, ProviderUserId = productModel.ProviderUserId };
            await Bl.CreateProductAsync(product);
            var reponseModel = new Models.ProductReponse.CreateProductReponse()
            {
                ProductId = product.Id
            };
            return reponseModel;
        }

        [HttpGet("getProductMatched/{distributorId}")]
        public async Task<IEnumerable<Product>> getMatchedWithNumber(int distributorId)
        {
            return await Bl.getMatchedWithNumber(distributorId);
        }

        [HttpGet("getMaterialMatched/{FarmerId}")]
        public async Task<IEnumerable<Material>> GetMaterialByFarmerId(int FarmerId)
        {
            return await MaterialBL.GetMaterialByFarmerId(FarmerId);
        }

    }
}