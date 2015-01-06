using Oas.Infrastructure.Domain;
using Oas.Infrastructure.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IAdvertismentsService
    {
        IList<Advertisment> Get();

        Advertisment Get(Guid Id);

        Advertisment Add(Advertisment advertisment);

        Advertisment Update(Advertisment advertisment);

        bool Remove(Guid Id);

        
    }
}
