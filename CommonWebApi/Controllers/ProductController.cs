using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.IBusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using DTO.Entities;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace CommonWebApi.Controllers
{
    //[Authorize]
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
        [HttpGet("getByProvider")]
        public async Task<IList<Product>> FindAllProductByProviderAsync() 
        {
            int userId = Int32.Parse(User.Claims.First(c => c.Type == "UserID").Value);

            return await bl.FindAllProductByProviderAsync(userId);
        }

        [HttpGet("testgetByProvider")]
        public async Task<IList<Product>> TestFindAllProductByProviderAsync()
        {
            return await bl.FindAllProductByProviderAsync(3);
        }

        [HttpPost("createProduct")]
        public async Task<Models.ProductReponse.CreateProductReponse> CreateProduct([FromBody]Models.Products productModel)
        {
            Product product = new Product() { Name = productModel.Name, CategoriesId = productModel.CategoriesId, ProviderUserId = productModel.ProviderUserId };
            await bl.CreateProductAsync(product);
            var reponseModel = new Models.ProductReponse.CreateProductReponse()
            {
                ProductId = product.Id
            };
            return reponseModel;
        }

        [HttpGet("getAllCategory")]
        public async Task<IList<Categories>> getAllCategory()
        {
            return await bl.getAllCategory();
        }
        
        [HttpGet("getProductMatched/{distributorId}")]
        public async Task<IEnumerable<Product>> getMatchedWithNumber(int distributorId)
        {
            return await bl.getMatchedWithNumber(distributorId);
        }
    }
}