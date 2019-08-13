using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
    public interface IFeedingBL
    {
        void AddNewFeedingList(IList<Feeding> feedings, int premisesId, int userId);

        Task<IList<Feeding>> GetFeedingListByPremisesId(int premisesId);

        Task<IList<Feeding>> GetFeedingList();

        Task RemoveFeedingById(int feedingId, int userId);
    }
}
