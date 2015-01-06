using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oas.Infrastructure.Domain;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;

namespace Oas.Infrastructure.Services
{
    /// <summary>
    /// Business Service
    /// </summary>
    public class SettingService : ISettingService
    {
        private readonly IRepository<Setting> settingRepository;
        public SettingService(IRepository<Setting> settingRepository)
        {
            this.settingRepository = settingRepository;
        }

        public IList<Setting> Get()
        {
            return settingRepository.Get.ToList();
        }

        public Setting Get(Guid Id)
        {
            return settingRepository.Get.FirstOrDefault(t => t.Id.Equals(Id));
        }

        public Setting Add(Setting packageItem)
        {
            packageItem.Id = Guid.NewGuid();
            var newItem = settingRepository.Add(packageItem);
            return newItem;
        }

        public Setting Update(Setting packageItem)
        {
            var updateItem = settingRepository.Update(packageItem);
            return updateItem;
        }

        public bool Remove(Guid Id)
        {
            var removeItem = Get(Id);
            settingRepository.Remove(removeItem);
            return true;
        }


        public Setting GetDefault()
        {
            return settingRepository.Get.FirstOrDefault();
        }
    }
}
