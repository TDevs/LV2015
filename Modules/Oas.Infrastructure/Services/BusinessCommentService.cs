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
    public class BusinessCommentService : IBusinessCommentService
    {
        private readonly IRepository<BusinessComment> businesscommentRepository;


        public BusinessCommentService(IRepository<BusinessComment> businesscommentRepository)
        {
            this.businesscommentRepository = businesscommentRepository;
        }
        public IList<Domain.BusinessComment> Get()
        {
            return businesscommentRepository.Get
                .Include(c => c.Business)
                .Include(c => c.User)
                .ToList();
        }

        public Domain.BusinessComment Get(Guid Id)
        {
            return businesscommentRepository.Find(Id);
        }

        public Domain.BusinessComment Add(Domain.BusinessComment businesscomment)
        {
            businesscomment.Id = Guid.NewGuid();
            businesscommentRepository.Add(businesscomment);
            return businesscomment;
        }

        public Domain.BusinessComment Update(Domain.BusinessComment businesscomment)
        {
            businesscommentRepository.Update(businesscomment);
            return businesscomment;
        }

        public bool Remove(Guid Id)
        {
            var businesscomment = Get(Id);
            if (businesscomment == null) return false;
            businesscommentRepository.Remove(businesscomment);
            return true;
        }
        public IList<Domain.BusinessComment> GetInclude()
        {
            return businesscommentRepository.Get
                .Include(c => c.Business)
                .Include(c => c.User)
                .ToList();
        }
    }
}
