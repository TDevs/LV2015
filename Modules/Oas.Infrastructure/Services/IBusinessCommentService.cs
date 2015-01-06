using Oas.Infrastructure.Domain;
using Oas.Infrastructure.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IBusinessCommentService 
    {
        IList<BusinessComment> Get();

        BusinessComment Get(Guid Id);

        BusinessComment Add(BusinessComment businessComment);

        BusinessComment Update(BusinessComment businessComment);

        bool Remove(Guid Id);

        IList<BusinessComment> GetInclude();
        
    }
}
