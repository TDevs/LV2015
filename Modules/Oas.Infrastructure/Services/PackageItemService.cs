using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public class PackageItemService:IPackageItemService
    {
         private readonly IRepository<PackageItem> packageItemRepository;



         public PackageItemService(IRepository<PackageItem> packageItemRepository)
        {
            this.packageItemRepository = packageItemRepository;
        }
         public IList<Domain.PackageItem> Get()
        {
            return packageItemRepository.Get.ToList();
        }

         public Domain.PackageItem Get(Guid Id)
        {
            return packageItemRepository.Find(Id);
        }

         public Domain.PackageItem Add(Domain.PackageItem packageItem)
        {
            packageItemRepository.Add(packageItem);
            return packageItem;
        }

         public Domain.PackageItem Update(Domain.PackageItem packageItem)
        {
            packageItemRepository.Update(packageItem);
            return packageItem;
        }

        public bool Remove(Guid Id)
        {
            var packageItem = Get(Id);
            if (packageItem == null) return false;
            packageItemRepository.Remove(packageItem);
            return true;
        }
    }
}
