using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Oas.Infrastructure.Domain;
using Oas.Infrastructure.Domain.Account; 

namespace Oas.Infrastructure
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext()
            : base(Constant.AppConnection)
        {
        }

        public IDbSet<MembershipPackage> MembershipPackages { get; set; }
        public IDbSet<PackageItem> PackageItems { get; set; }

        public IDbSet<BusinessCategory> BusinessCategories { get; set; }

        public IDbSet<Business> Businesses { get; set; }

        public IDbSet<BusinessPromotion> BusinessPromotions { get; set; }

        public IDbSet<Image> Images { get; set; }
        public IDbSet<Setting> Settings { get; set; }
        
        public IDbSet<Advertisment> Advertisments { get; set; }
        public IDbSet<EmailTemplate> EmailTemplates { get; set; }

        public IDbSet<BusinessLike> BusinessLikes { get; set; }
        public IDbSet<MessageHistory> MessageHitories { get; set; }

        public IDbSet<CarCategory> CarCategories { get; set; }

        public IDbSet<CarModel> CarModels { get; set; }

        public IDbSet<Car> Cars { get; set; }

        public IDbSet<UserApplication> UserApplications { get; set; }

        public IDbSet<Application> Applications { get; set; }

        public IDbSet<CarItem> CarItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove unused conventions
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");
            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<IdentityRole>()
            .ToTable("Roles");
            modelBuilder.Entity<Role>()
                .ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRoles");

        }


    }
}
