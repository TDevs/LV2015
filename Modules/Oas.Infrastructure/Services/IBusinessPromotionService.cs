using Oas.Infrastructure.Domain;
using Oas.Infrastructure.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IBusinessPromotionService
    {
        IList<BusinessPromotion> Get();

        BusinessPromotion Get(Guid Id);

        BusinessPromotion Add(BusinessPromotion businessPromotion);

        BusinessPromotion Update(BusinessPromotion businessPromotion);

        bool Remove(Guid Id);

        IList<BusinessPromotion> GetInclude();


        bool Approve(Guid guid);

        bool Reject(Guid guid);


        int GetTotalRequest();

        IList<BusinessPromotion> Search(PromotionCriteria criteria);
    }
}
