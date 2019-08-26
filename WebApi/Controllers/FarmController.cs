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
using Microsoft.AspNetCore.Authorization;
using Common.Constant;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Farm")]
    public class FarmController : ControllerBase
    {

        private readonly IFoodBL _foodBL;
        private readonly IFoodDataBL _foodDataBL;
        private readonly IPremisesBL _premisesBL;
        private readonly ITransactionBL _transactionBL;
        private readonly IFoodDetailBL _foodDetailBL;
        private readonly IFeedingBL _feedingBL;
        private readonly IVaccineBL _vaccineBL;
        private readonly IMapper _mapper;
        public FarmController(
            IFoodBL foodBL,
            IFoodDataBL foodDataBL,
            IPremisesBL premisesBL,
            ITransactionBL transactionBL,
            IFoodDetailBL foodDetailBL,
            IFeedingBL feedingBL,
            IVaccineBL vaccineBL,
            IMapper mapper)
        {
            _foodBL = foodBL;
            _foodDataBL = foodDataBL;
            _premisesBL = premisesBL;
            _transactionBL = transactionBL;
            _foodDetailBL = foodDetailBL;
            _feedingBL = feedingBL;
            _vaccineBL = vaccineBL;
            _mapper = mapper;
        }

        [HttpGet("foods")]
        public async Task<IActionResult> GetAllFood()
        {
            try
            {
                var farmId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { data = _mapper.Map<IList<Models.FoodFarm>>(await _foodBL.FindAllProductByFarmerAsync(farmId)) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }

        }

        [HttpPost("food")]
        public async Task<IActionResult> CreateFood([FromBody]Models.CreateFoodRequest foodRequest)
        {
            try
            {
                Entities.Food food = _mapper.Map<Entities.Food>(foodRequest);
                food.FarmId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                food.CreateById = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                await _foodBL.CreateProductAsync(food);
                var transactionHash = await _foodDataBL.CreateFood(food, food.FarmId);
                await _foodBL.AddDetail(food.FoodId, EFoodDetailType.CREATE, transactionHash, food.CreateById);
                return Ok(new { message = MessageConstant.INSERT_SUCCESS });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }

        }

        [HttpGet("food/feedings/{foodId}")]
        public async Task<IActionResult> GetFeedingsById(int foodId)
        {
            try
            {
                return Ok(new { data = await _foodDataBL.GetFeedingsById(foodId) });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpPut("food/feedings/{foodId}")]
        public async Task<IActionResult> AddFeedings(int foodId, [FromBody]List<Models.AddFeedingInfoToFoodDataRequest> feedingModelRequest)
        {
            try
            {
                var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                var transactionHash = await _foodDataBL.AddFeedings(foodId, feedingModelRequest);
                await _foodBL.InsertFeedingFood(foodId, feedingModelRequest);
                await _foodBL.AddDetail(foodId, EFoodDetailType.FEEDING, transactionHash, userId);
                return Ok(new { message = MessageConstant.INSERT_SUCCESS });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }


        [HttpGet("food/vaccinations/{foodId}")]
        public async Task<IActionResult> GetVaccinsById(int foodId)
        {
            try
            {
                return Ok(new { data = await _foodDataBL.GetVaccinsById(foodId) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpPut("food/vaccinations/{foodId}")]
        public async Task<IActionResult> AddVaccination(int foodId, [FromBody]List<Models.AddVaccineInfoToFoodDataRequest> vaccineModelRequest)
        {
            try
            {
                var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                var transactionHash = await _foodDataBL.AddVaccination(foodId, vaccineModelRequest);
                await _foodBL.InsertVaccineFood(foodId, vaccineModelRequest);
                await _foodBL.AddDetail(foodId, EFoodDetailType.VACCINATION, transactionHash, userId);
                return Ok(new { message = MessageConstant.INSERT_SUCCESS });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }

        }
      
        [HttpGet("category")]
        public async Task<IList<Entities.Category>> GetAllCategory()
        {
            return await _foodBL.getAllCategory();
        }

        [HttpGet("productdetailtype")]
        public async Task<IActionResult> GetProductDetailType()
        {
            try
            {
                return Ok(new { results = _mapper.Map<IList<Models.Option>>(await _foodDetailBL.GetFoodDetailTypeByPremises(PremisesTypeDataConstant.FARM)) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }


        [HttpPost("createTransaction")]
        public async Task<Models.TransactionReponse.CreateTransactionReponse> CreateTransaction([FromBody]Models.TransactionRequest transactionRequest)
        {
            Entities.Transaction transaction = _mapper.Map<Entities.Transaction>(transactionRequest);
            transaction.SenderId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            transaction.CreateById = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
            await _transactionBL.CreateSellFoodTransactionAsync(transaction);
            var transactionHash = await _foodDataBL.AddCertification(transactionRequest.FoodId, transactionRequest.CertificationNumber);
            await _foodBL.AddDetail(transactionRequest.FoodId, EFoodDetailType.VERIFY, transactionHash, transaction.CreateById);
            var reponseModel = new Models.TransactionReponse.CreateTransactionReponse()
            {
                TransactionId = transaction.TransactionId
            };
            return reponseModel;
        }

        [HttpGet("getAllProvider")]
        public async Task<IActionResult> GetAllProvider(string search, int foodId)
        {
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { results = _mapper.Map<IList<Models.Option>>(await _premisesBL.getAllProviderAsync(search, foodId, premisesId)) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpGet("food/foodDetail/{foodId}")]
        public async Task<Models.FoodData.FoodData> GetFoodDetail(long foodId)
        {
            return await _foodDataBL.GetFoodDataByID(foodId);
        }

        [HttpGet("countFarmTransaction")]
        public async Task<int> CountTransaction()
        {
            int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            return await _transactionBL.CountFarmTransaction(premisesId);
        }

        [HttpGet("getAllFarmTransaction")]
        public async Task<IActionResult> getAllTransaction()
        {
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { data = _mapper.Map<IList<Models.TransactionReponse.FarmGetTransaction>>(await _transactionBL.getAllFarmTransaction(premisesId)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPost("feedings")]
        public IActionResult AddFeedingList([FromBody]List<Models.Feedingm> feedings)
        {
            try
            {
                var feedingList = _mapper.Map<IList<Entities.Feeding>>(feedings);


                var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                var premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                _feedingBL.AddNewFeedingList(feedingList, premisesId, userId);
                return Ok(new { message = MessageConstant.INSERT_SUCCESS });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("premisesFeedings")]
        public async Task<IActionResult> GetFeedingListByPremisesId()
        {
            try
            {
                var feedingList = await _feedingBL.GetFeedingListByPremisesId(int.Parse(User.Claims.First(c => c.Type == "premisesID").Value));
                return Ok(new { data = _mapper.Map<IList<Models.Feedingm>>(feedingList) });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("feedings/{foodId}")]
        public async Task<IActionResult> GetFeedingList(int foodId)
        {
            try
            {
                var feedingList = await _feedingBL.GetFeedingListByFoodId(foodId);
                return Ok(new { results = _mapper.Map<IList<Models.Option>>(feedingList) });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPut("feeding/{id}")]
        public async Task<IActionResult> RemoveFeeding(int id)
        {
            try
            {
                var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                await _feedingBL.RemoveFeedingById(id, userId);
                return Ok(new { message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message, error = e.StackTrace });
            }
        }

        [HttpPost("vaccines")]
        public IActionResult AddVaccineList([FromBody]List<Models.Vaccinem> vaccines)
        {
            try
            {
                var vaccineList = _mapper.Map<IList<Entities.Vaccine>>(vaccines);
                var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                var premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                _vaccineBL.AddNewVaccineList(vaccineList, premisesId, userId);
                return Ok(new { message = MessageConstant.INSERT_SUCCESS });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("premisesVaccines")]
        public async Task<IActionResult> GetVaccineListByPremisesId()
        {
            try
            {
                var vaccineList = await _vaccineBL.GetVaccineListByPremisesId(int.Parse(User.Claims.First(c => c.Type == "premisesID").Value));
                return Ok(new { data = _mapper.Map<IList<Models.Vaccinem>>(vaccineList) });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("vaccines")]
        public async Task<IActionResult> GetFVaccineList()
        {
            try
            {
                var vaccineList = await _vaccineBL.GetVaccineList();
                return Ok(new { results = _mapper.Map<IList<Models.Option>>(vaccineList) });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpDelete("vaccine/{id}")]
        public async Task<IActionResult> RemoveVaccine(int id)
        {
            try
            {
                var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                await _vaccineBL.RemoveVaccineById(id, userId);
                return Ok(new { message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message, error = e.StackTrace });
            }
        }

        [HttpPost("farmReport")]
        public async Task<IActionResult> downloadReport()
        {
            try
            {
                int month = DateTime.Now.Month;
                var row = 3;
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                byte[] fileContents;
                var foodCreate = _mapper.Map<IList<Models.FoodRespone.ReportFood>>(await _foodBL.FarmReportFoodIn(premisesId));
                var foodSell = _mapper.Map<IList<Models.FoodRespone.ReportFoodOut>>(await _foodBL.FarmReportFoodOut(premisesId));
                var foodReject = _mapper.Map<IList<Models.FoodRespone.ReportFoodReject>>(await _foodBL.FarmReportFoodReject(premisesId));
                using (var package = new ExcelPackage())
                {
                    //sheet 1
                    var worksheet = package.Workbook.Worksheets.Add("Thêm mới");

                    //Row 1

                    worksheet.Cells[1, 1].Value = "Báo cáo chăn nuôi tháng " + month;
                    worksheet.Cells[1, 1].Style.Font.Size = 12;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:D1"].Merge = true;

                    //Row 2
                    worksheet.Cells[2, 1].Value = "Loại";
                    worksheet.Cells[2, 2].Value = "Giống";
                    worksheet.Cells[2, 3].Value = "Ngày tạo";
                    worksheet.Cells[2, 4].Value = "Hết hàng";
                    worksheet.Cells["A2:D2"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells["A2:D2"].AutoFitColumns();

                    for (var i = 0; i < foodCreate.Count; i++)
                    {
                        worksheet.Cells[row + i, 1].Value = foodCreate[i].CategoryName;
                        worksheet.Cells[row + i, 2].Value = foodCreate[i].Breed;
                        worksheet.Cells[row + i, 3].Value = foodCreate[i].CreateDate;
                        worksheet.Cells[row + i, 3].Style.Numberformat.Format = "dd-mm-yyyy";
                        if (foodCreate[i].IsSoldOut)
                        {
                            worksheet.Cells[row + i, 4].Value = "X";
                        }
                        else
                        {
                            worksheet.Cells[row + i, 4].Value = "";
                        }
                        worksheet.Cells["A" + (row + i) + ":D" + (row + i)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        worksheet.Cells["A" + (row + i) + ":D" + (row + i)].AutoFitColumns();
                    }
                    worksheet.Cells[(row + foodCreate.Count), 1].Value = "Tổng: " + foodCreate.Count;
                    worksheet.Cells[(row + foodCreate.Count), 1].Style.Font.Bold = true;
                    worksheet.Cells["A" + (row + foodCreate.Count) + ":D" + (row + foodCreate.Count)].Merge = true;

                    //Sheet 2
                    var worksheet2 = package.Workbook.Worksheets.Add("Bán hàng");
                    //Row 1

                    worksheet2.Cells[1, 1].Value = "Báo cáo bán hàng tháng " + month;
                    worksheet2.Cells[1, 1].Style.Font.Size = 12;
                    worksheet2.Cells[1, 1].Style.Font.Bold = true;
                    worksheet2.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet2.Cells["A1:E1"].Merge = true;

                    //Row 2
                    worksheet2.Cells[2, 1].Value = "Loại";
                    worksheet2.Cells[2, 2].Value = "Giống";
                    worksheet2.Cells[2, 3].Value = "Nhà cung cấp";
                    worksheet2.Cells[2, 4].Value = "Ngày bán";
                    worksheet2.Cells[2, 5].Value = "Ghi chú";
                    worksheet2.Cells["A2:E5"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["A2:E5"].AutoFitColumns();

                    for (var i = 0; i < foodSell.Count; i++)
                    {
                        worksheet2.Cells[row + i, 1].Value = foodSell[i].CategoryName;
                        worksheet2.Cells[row + i, 2].Value = foodSell[i].Breed;
                        worksheet2.Cells[row + i, 3].Value = foodSell[i].ReceiverName;
                        worksheet2.Cells[row + i, 4].Value = foodSell[i].CreateDate;
                        worksheet2.Cells[row + i, 4].Style.Numberformat.Format = "dd-mm-yyyy";
                        worksheet2.Cells[row + i, 5].Value = foodSell[i].ReceiverCommnent;
                        worksheet2.Cells["A" + (row + i) + ":E" + (row + i)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        worksheet2.Cells["A" + (row + i) + ":E" + (row + i)].AutoFitColumns();
                    }
                    worksheet2.Cells[(row + foodSell.Count), 1].Value = "Tổng: " + foodSell.Count;
                    worksheet2.Cells[(row + foodSell.Count), 1].Style.Font.Bold = true;
                    worksheet2.Cells["A" + (row + foodSell.Count) + ":E" + (row + foodSell.Count)].Merge = true;

                    //sheet 3
                    var worksheet3 = package.Workbook.Worksheets.Add("Từ chối");
                    //Row 1

                    worksheet3.Cells[1, 1].Value = "Báo cáo hàng bị từ chối tháng " + month;
                    worksheet3.Cells[1, 1].Style.Font.Size = 12;
                    worksheet3.Cells[1, 1].Style.Font.Bold = true;
                    worksheet3.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet3.Cells["A1:E1"].Merge = true;

                    //Row 2
                    worksheet3.Cells[2, 1].Value = "Loại";
                    worksheet3.Cells[2, 2].Value = "Giống";
                    worksheet3.Cells[2, 3].Value = "Nhà cung cấp";
                    worksheet3.Cells[2, 4].Value = "Ngày bán";
                    worksheet3.Cells[2, 5].Value = "Lý do từ chối";
                    worksheet3.Cells["A2:E5"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet3.Cells["A2:E5"].AutoFitColumns();

                    for (var i = 0; i < foodReject.Count; i++)
                    {
                        worksheet3.Cells[row + i, 1].Value = foodReject[i].CategoryName;
                        worksheet3.Cells[row + i, 2].Value = foodReject[i].Breed;
                        worksheet3.Cells[row + i, 3].Value = foodReject[i].ReceiverName;
                        worksheet3.Cells[row + i, 4].Value = foodReject[i].CreateDate;
                        worksheet3.Cells[row + i, 4].Style.Numberformat.Format = "dd-mm-yyyy";
                        worksheet3.Cells[row + i, 5].Value = foodReject[i].RejectReason;
                        worksheet3.Cells["A" + (row + i) + ":E" + (row + i)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        worksheet3.Cells["A" + (row + i) + ":E" + (row + i)].AutoFitColumns();
                    }
                    worksheet3.Cells[(row + foodReject.Count), 1].Value = "Tổng: " + foodReject.Count;
                    worksheet3.Cells[(row + foodReject.Count), 1].Style.Font.Bold = true;
                    worksheet3.Cells["A" + (row + foodReject.Count) + ":D" + (row + foodReject.Count)].Merge = true;

                    fileContents = package.GetAsByteArray();
                }

                if (fileContents == null || fileContents.Length == 0)
                {
                    return NotFound();
                }

                return File(
                    fileContents: fileContents,
                    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileDownloadName: "test.xlsx"
                );
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPut("soldOut/{foodId}")]
        public async Task<IActionResult> checkFoodSoldOut(int foodId)
        {
            try
            {
                await _foodBL.UpdateFoodSoldOut(foodId);
                return Ok(new { data = MessageConstant.UPDATE_SUCCESS });
            }catch(Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
