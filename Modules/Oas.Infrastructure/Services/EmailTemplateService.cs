using Oas.Infrastructure.Domain;
using Oas.Infrastructure.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IRepository<EmailTemplate> emailTemplateRepository;


        public EmailTemplateService(IRepository<EmailTemplate> emailTemplateRepository)
        {
            this.emailTemplateRepository = emailTemplateRepository;
        }
        public IList<Domain.EmailTemplate> Get()
        {
            return emailTemplateRepository.Get.ToList();
        }

        public Domain.EmailTemplate Get(Guid Id)
        {
            return emailTemplateRepository.Find(Id);
        }

        public Domain.EmailTemplate Add(Domain.EmailTemplate emailTemplate)
        {
            emailTemplateRepository.Add(emailTemplate);
            return emailTemplate;
        }

        public Domain.EmailTemplate Update(Domain.EmailTemplate emailTemplate)
        {
            emailTemplateRepository.Update(emailTemplate);
            return emailTemplate;
        }

        public bool Remove(Guid Id)
        {
            var emailTemplate = Get(Id);
            if (emailTemplate == null) return false;
            emailTemplateRepository.Remove(emailTemplate);
            return true;
        }
         
    }
}
