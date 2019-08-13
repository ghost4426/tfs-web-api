using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using Entities = DTO.Entities;
using BusinessLogic.IBusinessLogic;
using Common.Utils;
using AutoMapper;
using Common.Enum;
using DTO.Models.FoodData;
using Microsoft.AspNetCore.Authorization;
using Common.Constant;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize] 
    [ApiController]
    public class DistributorController : ControllerBase
    {
        private readonly IFoodBL _foodBL;
        private readonly IMapper     _mapper;
        private readonly IUserBL _userBL;
        private IFoodDataBL _foodDataBL;
        private readonly ITransactionBL _transactionBL;

        public DistributorController (IFoodBL productBL, IMapper mapper, IUserBL userBL, IFoodDataBL foodDataBL, ITransactionBL transactionBL)
        {
            _mapper = mapper;
            _foodBL = productBL;
            _foodDataBL = foodDataBL;
            _transactionBL = transactionBL;
            _userBL = userBL;
        }
        [HttpGet("getProductMatched")]
        public async Task<IActionResult> getMatchedWithNumber()
        {
            //string disID = User.Claims.First(c => c.Type == "PremisesId").Value;
            var disID = 16; 
            return Ok(new { data = _mapper.Map<IList<Models.Food>>(await _foodBL.getMatchedWithNumber(disID))});
        }


        [HttpGet("getProfile/{userId}")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
            var user = await _userBL.GetById(userId);
            return Ok(user);
        }

        [HttpPut("account/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] Models.UpdateUserRequest userInfo)
        {
            Entities.User user = null;
            try
            {
                user = _mapper.Map<Entities.User>(userInfo);
                user.UserId = id;
                user.Fullname = userInfo.Fullname;
                user.Email = userInfo.Email;
                user.PhoneNo = userInfo.PhoneNo;
                await _userBL.UpdateUser(user, id);
                return Ok("success!!");

            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

        }

        [HttpPut("changePassword/{userId}")]
        public async Task<IActionResult> ChangePassword(int userId, [FromBody] Models.ChangePasswordUserRequest userInfo)
        {
            try
            {
                await _userBL.ChangePassword(userId, userInfo.newPass, userInfo.oldPass);
                return Ok("success!");
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

        }

        [HttpGet("Food/{foodID}")]
        public async Task<DTO.Entities.Food> FindProductByID(int foodID)
        {
            return await _foodBL.getFoodById(foodID);
        }

        [HttpGet("transaction/{id}")]
        public async Task<IActionResult> getTransactionById(int id)
        {
            //var TransID = 3;
            return Ok(new { data = _mapper.Map<Models.Transaction>(await _transactionBL.GetTransactionById(id)) });
        }

        [HttpPut("transaction/{transID}")]
        public async Task<IActionResult> UpdateTransactionStatus(int tranId, [FromBody] Models.TransactionDistributorUpdateRequest trans)
        {
            return Ok(new { data = _mapper.Map<Models.Transaction>(await _transactionBL.UpdateDistributorTransaction(tranId, trans.StatusId, trans.RejectedReason, trans.RejectById)) });
        }
        [HttpPost("distributorFood/{id}")]
        public async Task<IActionResult> CreateDistributorFood(int id, [FromBody]Models.CreateDistributorFoodRequest foodRequest)
        {
            try
            {
                Entities.DistributorFood distributorFood = new Entities.DistributorFood();
                distributorFood.FoodId = foodRequest.FoodId;
                distributorFood.PremisesId = id;

                await _foodDataBL.AddDistributor(distributorFood.FoodId, id);
                await _foodBL.createDistributorFood(distributorFood);
                return Ok(new { message = MessageConstant.INSERT_SUCCESS });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }

        }
    }
}