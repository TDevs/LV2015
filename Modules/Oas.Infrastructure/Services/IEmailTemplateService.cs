using Oas.Infrastructure.Domain;
using Oas.Infrastructure.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IEmailTemplateService
    {
        IList<EmailTemplate> Get();

        EmailTemplate Get(Guid Id);

        EmailTemplate Add(EmailTemplate businessCategory);

        EmailTemplate Update(EmailTemplate businessCategory);

        bool Remove(Guid Id);

       
    }
}
