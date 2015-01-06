using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oas.Infrastructure.Domain;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;

namespace Oas.Infrastructure.Services
{
    /// <summary>
    /// Business Service
    /// </summary>
    public class BusinessService : IBusinessService
    {
        private readonly IRepository<Business> businessRepository;
        private readonly IRepository<BusinessCategory> businessCategoryRepository;
        private readonly IRepository<BusinessLike> businessLikeRepository;


        public BusinessService()
        {
            this.businessRepository = new Repository<Business>();
            this.businessCategoryRepository = new Repository<BusinessCategory>();
        }

        public BusinessService(IRepository<Business> businessRepository, IRepository<BusinessLike> businessLikeRepository, IRepository<BusinessCategory> businessCategoryRepository)
        {
            this.businessRepository = businessRepository;
            this.businessLikeRepository = businessLikeRepository;
            this.businessCategoryRepository = businessCategoryRepository;
        }
        public IList<Domain.Business> Get()
        {
            return businessRepository.Get
                .Include(c => c.BusinessCategory)
                .Include(u => u.User)
                .Include(u => u.Images)
                .Include(u => u.Comments)
                .ToList();

        }

        public Domain.Business Get(Guid Id)
        {
            return businessRepository.Get
                .Include(t => t.BusinessCategory)
                .Include(t => t.User)
                .Include(t => t.Images)
                .Include(t => t.Comments)
                .FirstOrDefault(t => t.Id.Equals(Id));
        }

        public Domain.Business Create(Domain.Business business)
        {
            business.Id = Guid.NewGuid();
            business.CreateDate = DateTime.Now;
            var obj = businessRepository.Add(business);
            return obj;
        }

        public Domain.Business Update(Domain.Business business)
        {
            business.CreateDate = DateTime.Now;
            var obj = businessRepository.Update(business);
            return obj;
        }

        public bool Delete(Guid Id)
        {
            var obj = Get(Id);
            if (obj == null) return false;
            businessRepository.Remove(obj);
            return true;
        }

        public Domain.Business Find(object[] keyValues)
        {
            throw new NotImplementedException();
        }


        public void Seek(Guid businessId, int number)
        {
            var biz = businessRepository.Get.FirstOrDefault(t => t.Id.Equals(businessId));
            if (biz != null)
            {
                biz.TotalViewed += 1;
                businessRepository.Update(biz);
            }
        }


        public int GetTotalRequest()
        {
            var total = businessRepository.Get.Where(t => t.Status == Status.Pending).Count();
            return (int)total;
        }


        public bool Approve(Guid guid)
        {
            var obj = Get(guid);
            if (obj != null)
            {
                obj.Status = Status.Approved;
                businessRepository.Update(obj);
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
                businessRepository.Update(obj);
                return true;
            }

            return false;
        }


        public void Like(Guid businessId, string userId, bool likeOrDislike)
        {
            var blike = new BusinessLike()
            {
                Id = Guid.NewGuid(),
                BusinessId = businessId,
                UserId = userId.ToString(),
                Like = likeOrDislike
            };

            var bl = businessLikeRepository.Get.FirstOrDefault(t => t.BusinessId.Equals(businessId) && t.UserId.Equals(userId));
            if (bl == null)
            {
                var result = businessLikeRepository.Add(blike);

            }
            else
            {
                bl.Like = likeOrDislike;
                businessLikeRepository.Update(bl);
            }

        }


        public bool GetLike(Guid businessId, string userId)
        {
            var bl = businessLikeRepository.Get.FirstOrDefault(t => t.BusinessId.Equals(businessId) && t.UserId.Equals(userId));
            if (bl == null) return false;
            return bl.Like;

        }


        public IList<Business> GetAvailableBusinesses(Guid? businessCategoryId, double lat, double lng, double radius)
        {
            //var bizs = businessRepository.Get.Where(t => businessCategoryId == null || t.BusinessCategoryId.Equals(businessCategoryId.Value) || (t.BusinessCategory.BusinessCategories.Any(c => c.ParentId.Equals(businessCategoryId.Value)))).ToList();

            var result = businessRepository.Get
                .Include(t => t.BusinessCategory)
                .Where(t => (businessCategoryId == null || t.BusinessCategoryId.Equals(businessCategoryId.Value))
                    && t.Latitude.HasValue && t.Longtitude.HasValue
                    && (Math.Pow((Math.Pow(t.Latitude.Value - lat, 2) + Math.Pow(t.Longtitude.Value - lng, 2)), 0.5) <= radius) && t.Status == Status.Approved);

            return result.ToList();

        }


        public IList<Business> GetNewBusinesses(double lat, double lng, double radius)
        {
            var result = businessRepository.Get
                         .Where(t => t.Latitude.HasValue && t.Longtitude.HasValue && (Math.Pow((Math.Pow(t.Latitude.Value - lat, 2) + Math.Pow(t.Longtitude.Value - lng, 2)), 0.5) <= radius) && t.Status == Status.Approved)
                         .OrderByDescending(t => t.CreateDate).Take(20);
            return result.ToList();
        }

        public IList<Business> GetPorpularBusinesses(double lat, double lng, double radius)
        {
            var result = businessRepository.Get
                        .Where(t => t.Latitude.HasValue && t.Longtitude.HasValue && (Math.Pow((Math.Pow(t.Latitude.Value - lat, 2) + Math.Pow(t.Longtitude.Value - lng, 2)), 0.5) <= radius) && t.Status == Status.Approved)
                        .OrderByDescending(t => t.TotalViewed)
                        .Take(5);
            return result.ToList();
        }


        public IList<Business> GetBusinessByCatIds(string bName, List<Guid?> ids, double lat, double lng, double radius)
        {

            var result = businessRepository.Get
                        .Where(t => ids.Contains(t.BusinessCategoryId) && (string.IsNullOrEmpty(bName) || t.Name.ToUpper().Contains(bName.ToUpper()))
                                && t.Latitude.HasValue && t.Longtitude.HasValue
                                 && (Math.Pow((Math.Pow(t.Latitude.Value - lat, 2) + Math.Pow(t.Longtitude.Value - lng, 2)), 0.5) <= radius) && t.Status == Status.Approved);
            return result.ToList();
        }


        public bool NotExist(string name, string zipcode, string cat)
        {
            var biz = businessRepository.Get
                .Include(t => t.BusinessCategory)
                .FirstOrDefault(t => t.Name.Equals(name) && t.Zipcode.Equals(zipcode));
            return biz == null;
        }


        public IList<Business> Search(BusinessCriteria criteria)
        {
            Func<Business, bool> exp = null;

            if (criteria.IsListing == false)
            {
                exp = t => (string.IsNullOrEmpty(criteria.Name) || criteria.Name.Contains(t.Name))
                                             && (criteria.Status == null || t.Status.Equals(criteria.Status))
                                             && (criteria.CategoryId == null || t.BusinessCategoryId.Equals(criteria.CategoryId))
                                             && (criteria.CategoryIds == null || criteria.CategoryIds.Contains(t.BusinessCategoryId))
                                             && (criteria.UserId == null || t.UserId.Equals(criteria.UserId))
                                             && ((criteria.Latitude == null || criteria.Longtitude == null || criteria.Radius == null) ||
                                             ((criteria.Latitude.HasValue && criteria.Longtitude.HasValue && criteria.Radius.HasValue
                                             && (Math.Pow((Math.Pow(t.Latitude.Value - criteria.Latitude.Value, 2) + Math.Pow(t.Longtitude.Value - criteria.Longtitude.Value, 2)), 0.5) <= criteria.Radius.Value))));
            }
            else
            {
                // Get children category
                var catIds = GetChidren(criteria.CategoryId);
                exp = t => (string.IsNullOrEmpty(criteria.Name) || t.Status.Equals(criteria.Name))
                                             && (criteria.Status == null || t.Status.Equals(criteria.Status))
                                             && (catIds.Contains(t.BusinessCategoryId))
                                             && (criteria.Latitude.HasValue && criteria.Longtitude.HasValue && criteria.Radius.HasValue
                                             && (Math.Pow((Math.Pow(t.Latitude.Value - criteria.Latitude.Value, 2) + Math.Pow(t.Longtitude.Value - criteria.Longtitude.Value, 2)), 0.5) <= criteria.Radius.Value));
            }

            var result = businessRepository.Get
                         .Where(exp)
                         .Skip(criteria.Skip ?? 0)
                         .Take(criteria.Take ?? 1000)
                         .OrderByDescending(t => t.CreateDate)
                         .ToList();

            return result;

        }

        private List<Guid> GetChidren(Guid? category)
        {
            List<Guid> result = new List<Guid>();
            if (category.HasValue)
                _GetChildren(result, category.Value);
            return result;
        }

        private void _GetChildren(List<Guid> result, Guid guid)
        {
            var cat = businessCategoryRepository.Get.FirstOrDefault(t => t.Id.Equals(guid));
            if (cat != null)
            {

                if (cat.BusinessCategories != null && cat.BusinessCategories.Count > 0)
                {
                    cat.BusinessCategories.ForEach(t => _GetChildren(result, t.Id));
                }
                else
                    result.Add(cat.Id);
            }
        }
    }
}
