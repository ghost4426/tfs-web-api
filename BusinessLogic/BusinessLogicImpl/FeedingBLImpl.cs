using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class FeedingBLImpl : IFeedingBL
    {
        private readonly IFeedingRepository _feedingRepository;
        private readonly IFeedingFoodRepository _feedingFoodRepository;

        public FeedingBLImpl(
            IFeedingRepository feedingRepository,
            IFeedingFoodRepository feedingFoodRepository
            )
        {
            _feedingRepository = feedingRepository;
            _feedingFoodRepository = feedingFoodRepository;
        }

        public async Task<IList<Feeding>> GetFeedingListByPremisesId(int premisesId)
        {
            //return await _feedingRepository.GetAllIncluding(f => f.Premises).Where(t => t.PremisesId == premisesId && t.IsDelete == false).ToListAsync();
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
            }
        }

        public async Task RemoveFeedingById(int feedingId, int userId)
        {
            var feeding = _feedingRepository.GetById(feedingId);
            feeding.IsDelete = true;
            feeding.UpdateById = userId;
            feeding.UpdateDate = DateTime.Now;
            await _feedingRepository.UpdateAsync(feeding);
        }

        public async Task<IList<Feeding>> GetFeedingListByFoodId(int foodId)
        {
            var feedingFood = await _feedingFoodRepository.FindAllAsync(f => f.FoodId == foodId);
            var query = _feedingRepository.GetIQueryable();
            return await query.Include(f => f.Premises).Where(f => !feedingFood.Any(ff => ff.FeedingId == f.FeedingId) && f.Premises.IsActive && !f.IsDelete).ToListAsync();
        }
    }
}
