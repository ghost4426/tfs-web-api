using DTO.Entities;
using Models = DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
    public interface IPremisesTypeBL
    {
        Task<PremisesType> GetById(int premisesId);
    }
}
