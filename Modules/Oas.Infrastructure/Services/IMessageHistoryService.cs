using Oas.Infrastructure.Domain;
using Oas.Infrastructure.Domain.Account;
using Oas.Infrastructure.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IMessageHistoryService
    {
        IList<MessageHistory> Get();

        MessageHistory Get(Guid Id);

        MessageHistory Add(MessageHistory advertisment);

        MessageHistory Update(MessageHistory advertisment);

        bool Remove(Guid Id);

        
    }
}
