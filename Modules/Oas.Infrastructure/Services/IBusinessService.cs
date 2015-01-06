using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oas.Infrastructure.Domain;

namespace Oas.Infrastructure.Services
{
    public interface IBusinessService
    {

        IList<Business> Get();
        Business Get(Guid Id);

        Business Create(Business business);

        Business Update(Business business);

        bool Delete(Guid Id);

        Business Find(object[] keyValues);

        void Seek(Guid businessId, int number);

        int GetTotalRequest();

        bool Approve(Guid guid);

        bool Reject(Guid guid);

        void Like(Guid businessId, string userId, bool likeOrDislike);
        bool GetLike(Guid guid, string userId);

        IList<Business> GetAvailableBusinesses(Guid? businessCategoryId, double lat, double lng, double radius);

        IList<Business> GetNewBusinesses(double p1, double p2, double p3);

        IList<Business> GetPorpularBusinesses(double p1, double p2, double p3);

        IList<Business> GetBusinessByCatIds(string bName, List<Guid?> ids, double p1, double p2, double p3);

        bool NotExist(string name, string zipcode,string cat);

        IList<Business> Search(BusinessCriteria criteria);
    }
}
