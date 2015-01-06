using Oas.Infrastructure.Domain;
using Oas.Infrastructure.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public class BusinessPromotionService : IBusinessPromotionService
    {
        private readonly IRepository<BusinessPromotion> businessPromotionRepository;


        public BusinessPromotionService(IRepository<BusinessPromotion> businessPromotionRepository)
        {
            this.businessPromotionRepository = businessPromotionRepository;
        }
        public IList<Domain.BusinessPromotion> Get()
        {
            return businessPromotionRepository.Get.ToList();
        }

        public Domain.BusinessPromotion Get(Guid Id)
        {
            return businessPromotionRepository.Find(Id);
        }

        public Domain.BusinessPromotion Add(Domain.BusinessPromotion businesspromotion)
        {
            businesspromotion.Id = Guid.NewGuid();
            businessPromotionRepository.Add(businesspromotion);
            return businesspromotion;
        }

        public Domain.BusinessPromotion Update(Domain.BusinessPromotion businesspromotion)
        {
            businessPromotionRepository.Update(businesspromotion);
            return businesspromotion;
        }

        public bool Remove(Guid Id)
        {
            var businesspromotion = Get(Id);
            if (businesspromotion == null) return false;
            businessPromotionRepository.Remove(businesspromotion);
            return true;
        }
        public IList<Domain.BusinessPromotion> GetInclude()
        {
            return businessPromotionRepository.Get
                .Include(c => c.Business)
                .ToList();
        }

        public bool Approve(Guid guid)
        {
            var obj = Get(guid);
            if (obj != null)
            {
                obj.Status = Status.Approved;
                businessPromotionRepository.Update(obj);
                return true;
            }

            return false;
        }

        public bool Reject(Guid guid)
        {
            var obj = Get(guid);
            if (obj != null)
            {
                obj.Status = Status.Rejected;
                businessPromotionRepository.Update(obj);
                return true;
            }

            return false;
        }


        public int GetTotalRequest()
        {
            var total = businessPromotionRepository.Get.Where(t => t.Status == Status.Pending).Count();
            return (int)total;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IList<BusinessPromotion> Search(PromotionCriteria criteria)
        {
            Func<BusinessPromotion, bool> exp = null;

            exp = t => (Math.Pow((Math.Pow(t.Business.Latitude.Value - criteria.Latitude.Value, 2) + Math.Pow(t.Business.Longtitude.Value - criteria.Longtitude.Value, 2)), 0.5) <= criteria.Radius.Value);

            var result = businessPromotionRepository.Get
                        .Include(t => t.Business)
                        .Where(exp)
                        .Skip(criteria.Skip ?? 0)
                        .Take(criteria.Take ?? 1000)
                        .OrderByDescending(t => t.StartDate)
                       .ToList();

            return result;
        }
    }
}
