using DTO.Models.FoodData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
   public interface IFoodDataBL
    {

        Task<string> SaveFoodData(FoodData Food);

        Task<string> CreateFood(FoodData Food);

        Task<FoodData> GetFoodDataByID(long id);
    }
}
