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
    public class ProductController : ControllerBase
    {
        private IProductBL bl;

        public ProductController(IProductBL productBL)
        {
            this.bl = productBL;
        }

        // GET api/values
        [HttpGet]
       public async Task<IList<Product>> GetAllProduct()
        {
            return await bl.GetAllProductAsync();
        }

        // GET api/values
        [HttpGet("/getByProvider")]
        public async Task<IList<Product>> FindAllProductByProviderAsync(int providerID) 
        {
            return await bl.FindAllProductByProviderAsync(providerID);
        }

        [HttpPost("/createProduct")]
        public async Task<Models.ProductReponse.CreateProductReponse> CreateProduct([FromBody] Product productModel)
        {
            Entities.Product product = new Entities.Product() { Name = productModel.Name, CategoriesId = productModel.CategoriesId, ProviderUserId = productModel.ProviderUserId };
            await bl.CreateProductAsync(product);
            var reponseModel = new Models.ProductReponse.CreateProductReponse()
            {
                ProductId = product.Id
            };
            return reponseModel;
        }
        [HttpGet("/getProductMatched")]
        public async Task<IEnumerable<Product>> getMatchedWithNumber(int distributorId)
        {
            return await bl.getMatchedWithNumber(distributorId);
        }
    }
}