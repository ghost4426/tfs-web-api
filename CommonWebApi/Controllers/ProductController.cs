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
        private IFoodBL _productBL;

        public ProductController(IFoodBL productBL)
        {
            _productBL = productBL;
        }

        // GET api/values
        [HttpGet]
       public async Task<IList<Food>> GetAllProduct()
        {
            return await _productBL.GetAllProductAsync();
        }

        // GET api/values
        [HttpGet("getByProvider")]
        public async Task<IList<Food>> FindAllProductByProviderAsync() 
        {
            int userId = Int32.Parse(User.Claims.First(c => c.Type == "UserID").Value);

            return await _productBL.FindAllProductByProviderAsync(userId);
        }

        [HttpGet("testgetByProvider")]
        public async Task<IList<Food>> TestFindAllProductByProviderAsync()
        {
            return await _productBL.FindAllProductByProviderAsync(3);
        }

       

        [HttpGet("getAllCategory")]
        public async Task<IList<Categories>> getAllCategory()
        {
            return await _productBL.getAllCategory();
        }
        
        [HttpGet("getProductMatched/{distributorId}")]
        public async Task<IEnumerable<Food>> getMatchedWithNumber(int distributorId)
        {
            return await _productBL.getMatchedWithNumber(distributorId);
        }
    }
}