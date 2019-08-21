using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class FoodRespone
    {

        public class FoodsRespone
        {
            public IList<Food> Foods { get; set; }
        }

        public class TreatmentReponse
        {
            public int TreatmentId { get; set; }
            public string Name { get; set; }
            public int TreatmentParentId { get; set; }
        }

        public class TreatmentPremises
        {
            public int TreatmentId { get; set; }
            public string Name { get; set; }
        }

        public class ReportFood
        {
            public string Breed { get; set; }
            public string CategoryName { get; set; }
            public DateTime CreateDate { get; set; }
            public bool IsSoldOut { get; set; }
        }

        public class ReportFoodOut
        {
            public string Breed { get; set; }
            public string CategoryName { get; set; }
            public string ReceiverName { get; set; }
            public DateTime CreateDate { get; set; }
            public string ReceiverCommnent { get; set; }
        }

        public class ReportFoodReject
        {
            public string Breed { get; set; }
            public string CategoryName { get; set; }
            public string ReceiverName { get; set; }
            public DateTime CreateDate { get; set; }
            public string RejectReason { get; set; }
        }

        public class ProviderReportFoodIn
        {
            public string Breed { get; set; }
            public string CategoryName { get; set; }
            public string SenderName { get; set; }
            public DateTime CreateDate { get; set; }
        }
    }
}
