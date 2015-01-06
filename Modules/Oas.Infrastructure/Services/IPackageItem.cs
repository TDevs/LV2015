using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IPackageItemService
    {
        IList<PackageItem> Get();

        PackageItem Get(Guid Id);

        PackageItem Add(PackageItem packageItem);

        PackageItem Update(PackageItem packageItem);

        bool Remove(Guid Id);
    }
}
