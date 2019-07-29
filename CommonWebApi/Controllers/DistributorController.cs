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

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributorController : ControllerBase
    {
        private readonly IFoodBL _foodBL;
        private readonly IMapper     _mapper;
        private readonly ITransactionBL _transactionBL;
        private readonly IUserBL _userBL;

        public DistributorController (IFoodBL productBL, IMapper mapper, ITransactionBL transactionBL, IUserBL userBL)
        {
            _mapper = mapper;
            _foodBL = productBL;
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

        [HttpGet("getTransactionById/{transId}")]
        public async Task<IActionResult> getTransactionById(int transId)
        {
            //var TransID = 3;
            return Ok(new { data = _mapper.Map<Models.Transaction>(await _transactionBL.GetTransactionById(transId)) });
        }

        [HttpPut("updateTransaction/{transID}/{status}/{reason}/{foodId}/{distributorId}")]
        public async Task<IActionResult> UpdateTransactionStatus(int tranId, int status, string reason, int foodId, int distributorId)
        {
            //tranId = 3;
            //status = 3;
            //reason = "NO REASON";

            Entities.DistributorFood distributorFood = new Entities.DistributorFood();
            distributorFood.FoodId = foodId;
            distributorFood.PremisesId = distributorId;
            await _foodBL.createDistributorFood(distributorFood);

            return Ok(new { data = _mapper.Map<Models.Transaction>(await _transactionBL.UpdateTransaction(tranId, status, reason)) });
        }

        [HttpGet("getProfile/{userId}")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
            var user = await _userBL.GetById(userId);
            return Ok(user);
        }

        [HttpPut("updateProfile/{id}")]
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
                await _userBL.UpdateUser(user, 16);
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
    }
}