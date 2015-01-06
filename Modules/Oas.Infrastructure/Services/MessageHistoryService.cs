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
    public class MessageHistoryService : IMessageHistoryService
    {
        private readonly IRepository<MessageHistory> advertismentRepository;


        public MessageHistoryService(IRepository<MessageHistory> advertismentRepository)
        {
            this.advertismentRepository = advertismentRepository;
        }
        public IList<MessageHistory> Get()
        {
            return advertismentRepository.Get.ToList();
        }

        public MessageHistory Get(Guid Id)
        {
            return advertismentRepository.Find(Id);
        }

        public MessageHistory Add(MessageHistory advertisment)
        {
            advertismentRepository.Add(advertisment);
            return advertisment;
        }

        public MessageHistory Update(MessageHistory advertisment)
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
