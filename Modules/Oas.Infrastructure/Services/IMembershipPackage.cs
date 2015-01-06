using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IMembershipPackageService
    {
        IList<MembershipPackage> Get();

        MembershipPackage Get(Guid Id);

        MembershipPackage Add(MembershipPackage membershipPackage);

        MembershipPackage Update(MembershipPackage membershipPackage);

        bool Remove(Guid Id);
    }
}
