using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public class MembershipPackageService : IMembershipPackageService
    {
        private readonly IRepository<MembershipPackage> membershipPackageRepository;



        public MembershipPackageService(IRepository<MembershipPackage> membershipPackageRepository)
        {
            this.membershipPackageRepository = membershipPackageRepository;
        }
        public IList<Domain.MembershipPackage> Get()
        {
            return membershipPackageRepository.Get.ToList();
        }

        public Domain.MembershipPackage Get(Guid Id)
        {
            return membershipPackageRepository.Find(Id);
        }

        public Domain.MembershipPackage Add(Domain.MembershipPackage membershipPackage)
        {
            membershipPackageRepository.Add(membershipPackage);
            return membershipPackage;
        }

        public Domain.MembershipPackage Update(Domain.MembershipPackage membershipPackage)
        {
            membershipPackageRepository.Update(membershipPackage);
            return membershipPackage;
        }

        public bool Remove(Guid Id)
        {
            var membershipPackage = Get(Id);
            if (membershipPackage == null) return false;
            membershipPackageRepository.Remove(membershipPackage);
            return true;
        }
    }
}
