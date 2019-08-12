using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class FeedingBLImpl : IFeedingBL
    {
        private IFeedingRepository _feedingRepository;

        public FeedingBLImpl(IFeedingRepository feedingRepository)
        {
            _feedingRepository = feedingRepository;
        }

        public async Task<IList<Feeding>> GetFeedingListByPremisesId(int premisesId)
        {
            return await _feedingRepository.FindAllAsync(t => t.PremisesId == premisesId && t.IsDelete == false);
        }

        public void AddNewFeedingList(IList<Feeding> feedings, int premisesId, int userId)
        {
            //List<Feeding> feedingUpdateList = null;
            foreach (var feeding in feedings)
            {
                if (feeding.FeedingId == 0)
                {
                    feeding.CreateById = userId;
                    feeding.UpdateById = userId;
                    feeding.PremisesId = premisesId;
                    feeding.UpdateDate = DateTime.Now;
                    _feedingRepository.Insert(feeding);
                }
                //else
                //{
                //    if (feedingUpdateList == null)
                //    {
                //        feedingUpdateList = new List<Feeding>();
                //    }
                //    feedingUpdateList.Add(feeding);
                //}
            }

            //if (feedingUpdateList != null)
            //{
            //    foreach (var feeding in feedingUpdateList)
            //    {
            //        var tmpFeeding = _feedingRepository.GetById(feeding.FeedingId);
            //        tmpFeeding.Name = feeding.Name;
            //        tmpFeeding.UpdateById = userId;
            //        tmpFeeding.UpdateDate = DateTime.Now;
            //    }
            //  await  _feedingRepository.UpdateRangeAsync(feedingUpdateList);
            //}
        }

        public async Task RemoveFeedingById(int feedingId, int userId)
        {
            var feeding = _feedingRepository.GetById(feedingId);
            feeding.IsDelete = true;
            feeding.UpdateById = userId;
            feeding.UpdateDate = DateTime.Now;
            await _feedingRepository.UpdateAsync(feeding);
        }

        public async  Task<IList<Feeding>> GetFeedingList()
        {
            return await _feedingRepository.FindAllAsync(t => t.IsDelete == false);
        }
    }
}
