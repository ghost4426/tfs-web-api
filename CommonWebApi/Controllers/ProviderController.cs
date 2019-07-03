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
    public class ProviderController : ControllerBase
    {
        private IProductBL _productBL;

        public ProviderController(IProductBL productBL)
        {
            _productBL = productBL;
        }

        [HttpGet("testgetByProvider")]
        public async Task<IList<Food>> TestFindAllProductByProviderAsync()
        {
            return await _productBL.FindAllProductByProviderAsync(3);
        }

        [HttpGet("getByProvider")]
        public async Task<IList<Food>> FindAllProductByProviderAsync()
        {
            int userId = Int32.Parse(User.Claims.First(c => c.Type == "UserID").Value);

            return await _productBL.FindAllProductByProviderAsync(userId);
        }
    }
}