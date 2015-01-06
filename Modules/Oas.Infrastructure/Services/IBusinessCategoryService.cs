using Oas.Infrastructure.Domain;
using Oas.Infrastructure.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IBusinessCategoryService
    {
        IList<BusinessCategory> Get();

        BusinessCategory Get(Guid Id);

        BusinessCategory GetByFactualCategoryId(int id);

        BusinessCategory Add(BusinessCategory businessCategory);

        BusinessCategory Update(BusinessCategory businessCategory);

        bool Remove(Guid Id);

        List<BusinessCategoryGroup> GetGroup();
    }
}
