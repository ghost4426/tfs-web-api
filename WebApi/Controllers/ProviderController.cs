using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.IBusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using Entities = DTO.Entities;
using BusinessLogic.IBusinessLogic;
using Common.Utils;
using Common.Constant;
using AutoMapper;
using Common.Enum;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Provider")]
    public class ProviderController : ControllerBase
    {
        private readonly IFoodBL _foodBL;
        private readonly IFoodDataBL _foodDataBL;
        private readonly ITransactionBL _transactionBL;
        private readonly ITreatmentBL _treatmentBL;
        private readonly IPremisesBL _premisesBL;
        private readonly IMapper _mapper;

        public ProviderController(
            IFoodBL foodBL,
            IFoodDataBL foodDataBL,
            ITransactionBL transactionBL,
            ITreatmentBL treatmentBL,
            IPremisesBL premisesBL,
            IMapper mapper)
        {
            _foodBL = foodBL;
            _foodDataBL = foodDataBL;
            _transactionBL = transactionBL;
            _treatmentBL = treatmentBL;
            _premisesBL = premisesBL;
            _mapper = mapper;
        }

        [HttpPost("treatment")]
        public async Task<IActionResult> CreateTreatment([FromBody]Models.CreateTreatmentRequest treatmentRequest)
        {
            try
            {
                var Treatment = _mapper.Map<Entities.Treatment>(treatmentRequest);
                var TreatmentProcess = treatmentRequest.TreatmentProcess;
                Treatment.PremisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                Treatment.CreateById = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                Treatment.UpdateById = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                Treatment.CreateDate = DateTime.Now;
                await _treatmentBL.CreateTreatment(Treatment, TreatmentProcess);
                return Ok(new { message = MessageConstant.INSERT_SUCCESS });
            }catch(Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }           
        }

        //More treatmentDetail
        [HttpPost("moreTreatment/{treatmentId}")]
        public async Task<IActionResult> CreateMoreTreatment(int treatmentId, [FromBody]Models.CreateMoreTreatmentRequest treatmentRequest)
        {
            var Treatment = _mapper.Map<Entities.Treatment>(treatmentRequest);
            var TreatmentProcess = treatmentRequest.TreatmentProcess;
            IList<int> treatment = await _treatmentBL.getTreatmentIdByParent(treatmentId);
            foreach (var id in treatment)
            {
                await _treatmentBL.deleteTreatment(id);
            }
            Treatment.PremisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            Treatment.CreateById = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
            Treatment.UpdateById = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
            Treatment.CreateDate = DateTime.Now;
            await _treatmentBL.CreateMoreTreatmentDetail(treatmentId, Treatment, TreatmentProcess);
            return Ok(new { message = MessageConstant.INSERT_SUCCESS });
        }

        [HttpPut("food/treatment/{foodId}")]
        public async Task<IActionResult> AddTreatment(int foodId, [FromBody]string treatmentId)
        {
            try
            {
                var providerId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                var transactionHash = await _foodDataBL.AddTreatment(foodId, int.Parse(treatmentId), providerId);

                await _foodBL.AddDetail(foodId, EFoodDetailType.TREATMENT, transactionHash, userId);
                Entities.ProviderFood food = await _foodBL.getFoodById(foodId, providerId);
                await _foodBL.UpdateFoodTreatment(food, foodId, int.Parse(treatmentId), providerId);
                return Ok(new { message = MessageConstant.INSERT_SUCCESS});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpPut("food/packaging/{foodId}")]
        public async Task<IActionResult> Packaging(int foodId, [FromBody]Models.PackagingRequest packagingRequest)
        {
            try
            {
                var providerId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                var Packaging = _mapper.Map<Models.FoodData.Packaging>(packagingRequest);
                var transactionHash = await _foodDataBL.Packaging(foodId, Packaging, providerId);

                await _foodBL.AddDetail(foodId, EFoodDetailType.PACKAGING, transactionHash, userId);
                await _foodBL.UpdatePackagingFood(foodId, providerId);
                return Ok(new { message = MessageConstant.INSERT_SUCCESS });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpGet("getFoodByProvider")]
        public async Task<IActionResult> FindAllProviderFoodAsync()
        {
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { data = _mapper.Map<IList<Models.FoodProvider>>(await _foodBL.getAllFoodByProviderId(premisesId)) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpGet("countProviderTransaction")]
        public async Task<int> CountTransaction()
        {
            int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            return await _transactionBL.CountProviderTransaction(premisesId);
        }

        [HttpGet("getAllProviderReceiveTransaction")]
        public async Task<IActionResult> getAllReceiveTransaction()
        {
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { data = _mapper.Map<IList<Models.TransactionReponse.ProviderGetTransaction>>(await _transactionBL.getAllProviderReceiveTransaction(premisesId)) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpGet("getAllProviderSendTransaction")]
        public async Task<IActionResult> getAllSendTransaction()
        {
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { data = _mapper.Map<IList<Models.TransactionReponse.ProviderGetSendTransaction>>(await _transactionBL.getAllProviderSendTransaction(premisesId)) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpPut("UpdateTransaction/{transactionId}")]
        public async Task<IActionResult> UpdateTransaction(int transactionId, [FromBody] Models.TransactionUpdateRequest trans)
        {
            try
            {
                Entities.Transaction transaction = new Entities.Transaction()
                {
                    TransactionId = transactionId,
                    StatusId = trans.StatusId,
                    RejectReason = trans.RejectedReason,
                    ReceiverComment = trans.ProviderComment,
                };
                await _transactionBL.UpdateTransaction(transaction, transactionId);
                return Ok(new { message = MessageConstant.UPDATE_SUCCESS });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpPost("providerFood")]
        public async Task<IActionResult> CreateProviderFood([FromBody]Models.CreateProviderFoodRequest foodRequest)
        {
            try
            {
                Entities.ProviderFood food = _mapper.Map<Entities.ProviderFood>(foodRequest);
                food.PremisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                await _foodDataBL.AddProvider(food.FoodId, int.Parse(User.Claims.First(c => c.Type == "premisesID").Value));
                await _foodBL.createProviderFood(food);
                return Ok(new { message = MessageConstant.INSERT_SUCCESS });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }

        }

        [HttpGet("foodTreatment/{treatmentId}")]
        public async Task<IActionResult> getAllTreatmentById(int treatmentId)
        {
            try
            {
                return Ok(new { data = _mapper.Map<IList<Models.FoodRespone.TreatmentReponse>>(await _treatmentBL.getAllTreatmentById(treatmentId)) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpGet("treatment")]
        public async Task<IActionResult> getAllTreatmentByPremisesId()
        {
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { results = _mapper.Map<IList<Models.Option>>(await _treatmentBL.getAllTreatmentByPremisesId(premisesId)) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpDelete("deleteTreatment/{treatmentId}")]
        public async Task<IActionResult> deleteTreatment(int treatmentId)
        {
            try
            {
                await _treatmentBL.deleteTreatment(treatmentId);
                return Ok(new { message = "Xóa thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpGet("getAllDistributor")]
        public async Task<IActionResult> getAllDistributorAsync(string search, int foodId)
        {
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { results = _mapper.Map<IList<Models.Option>>(await _premisesBL.getAllDistriburtorAsync(search, foodId,premisesId)) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpGet("getFoodDataByProvider")]
        public async Task<IActionResult> GetFoodDataByIDAndProviderID(long id)
        {
            try
            {
                int providerId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { data = await _foodDataBL.GetFoodDataByIDAndProviderID(id, providerId) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpPost("createTransaction")]
        public async Task<IActionResult> CreateTransaction([FromBody]Models.TransactionRequest transactionRequest)
        {
            try
            {
                Entities.Transaction transaction = _mapper.Map<Entities.Transaction>(transactionRequest);
                transaction.SenderId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                transaction.CreateById = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                var transactionHash = await _foodDataBL.ProviderAddCertification(transactionRequest.FoodId, transaction.SenderId, transactionRequest.CertificationNumber);
                await _transactionBL.CreateSellFoodTransactionAsync(transaction);
                var reponseModel = new Models.TransactionReponse.CreateTransactionReponse()
                {
                    TransactionId = transaction.TransactionId
                };
                return Ok(new { message = MessageConstant.INSERT_SUCCESS});
            }catch(Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }            
        }

        [HttpPost("downloadReport")]
        public async Task<IActionResult> downloadReport()
        {
            try
            {
                int month = DateTime.Now.Month;
                var row = 3;
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                byte[] fileContents;
                var foodCreate = _mapper.Map<IList<Models.FoodRespone.ProviderReportFoodIn>>(await _foodBL.ProviderReportFoodIn(premisesId));
                var foodSell = _mapper.Map<IList<Models.FoodRespone.ReportFoodOut>>(await _transactionBL.ProviderFoodOutReport(premisesId));
                var foodReject = _mapper.Map<IList<Models.FoodRespone.ReportFoodReject>>(await _transactionBL.ProviderFoodRejectReport(premisesId));
                using (var package = new ExcelPackage())
                {
                    //sheet 1
                    var worksheet = package.Workbook.Worksheets.Add("Nhập hàng");

                    //Row 1

                    worksheet.Cells[1, 1].Value = "Báo cáo nhập thực phẩm tháng " + month;
                    worksheet.Cells[1, 1].Style.Font.Size = 12;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:D1"].Merge = true;

                    //Row 2
                    worksheet.Cells[2, 1].Value = "Loại";
                    worksheet.Cells[2, 2].Value = "Giống";
                    worksheet.Cells[2, 3].Value = "Nông trại";
                    worksheet.Cells[2, 4].Value = "Ngày nhập";
                    worksheet.Cells["A2:D2"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells["A2:D2"].AutoFitColumns();

                    for (var i = 0; i < foodCreate.Count; i++)
                    {
                        worksheet.Cells[row + i, 1].Value = foodCreate[i].CategoryName;
                        worksheet.Cells[row + i, 2].Value = foodCreate[i].Breed;
                        worksheet.Cells[row + i, 3].Value = foodCreate[i].SenderName;
                        worksheet.Cells[row + i, 4].Value = foodCreate[i].CreateDate;
                        worksheet.Cells[row + i, 4].Style.Numberformat.Format = "dd-mm-yyyy";                        
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
                    worksheet2.Cells[2, 3].Value = "Nhà phân phối";
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
                    worksheet3.Cells[2, 3].Value = "Nhà phân phối";
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
                    worksheet3.Cells["A" + (row + foodReject.Count) + ":E" + (row + foodReject.Count)].Merge = true;

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
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }
    }
}