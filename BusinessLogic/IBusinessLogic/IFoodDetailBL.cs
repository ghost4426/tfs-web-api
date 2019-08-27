using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
   public interface IFoodDetailBL
    {
        Task<IList<FoodDetailType>> GetFoodDetailTypeByPremises(string premisesType);

        Task<IList<FoodDetail>> GetFoodDetail();
    }
}
