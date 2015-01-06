using Oas.Infrastructure.Domain;
using Oas.Infrastructure.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public class AdvertismentsService : IAdvertismentsService
    {
        private readonly IRepository<Advertisment> advertismentRepository;


        public AdvertismentsService(IRepository<Advertisment> advertismentRepository)
        {
            this.advertismentRepository = advertismentRepository;
        }
        public IList<Domain.Advertisment> Get()
        {
            return advertismentRepository.Get.ToList();
        }

        public Domain.Advertisment Get(Guid Id)
        {
            return advertismentRepository.Find(Id);
        }

        public Domain.Advertisment Add(Domain.Advertisment advertisment)
        {
            advertismentRepository.Add(advertisment);
            return advertisment;
        }

        public Domain.Advertisment Update(Domain.Advertisment advertisment)
        {
            advertismentRepository.Update(advertisment);
            return advertisment;
        }

        public bool Remove(Guid Id)
        {
            var advertisment = Get(Id);
            if (advertisment == null) return false;
            advertismentRepository.Remove(advertisment);
            return true;
        }
         
    }
}
