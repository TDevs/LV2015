using Oas.Infrastructure.Domain;
using Oas.Infrastructure.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Oas.Infrastructure.Services
{
    public class BusinessCategoryService : IBusinessCategoryService
    {
        private readonly IRepository<BusinessCategory> businessCategoryRepository;

        public BusinessCategoryService()
        {
            this.businessCategoryRepository = new Repository<BusinessCategory>();
        }

        public BusinessCategoryService(IRepository<BusinessCategory> businessCategoryRepository)
        {
            this.businessCategoryRepository = businessCategoryRepository;
        }
        public IList<Domain.BusinessCategory> Get()
        {
            return businessCategoryRepository
                .Get
                .Include(t => t.BusinessCategories)
                .ToList();
        }

        public Domain.BusinessCategory Get(Guid Id)
        {
            return businessCategoryRepository.Get
                .Include(t => t.BusinessCategories)
                .FirstOrDefault(t => t.Id.Equals(Id));
        }

        public Domain.BusinessCategory Add(Domain.BusinessCategory businessCategory)
        {
            businessCategoryRepository.Add(businessCategory);
            return businessCategory;
        }

        public Domain.BusinessCategory Update(Domain.BusinessCategory businessCategory)
        {
            businessCategoryRepository.Update(businessCategory);
            return businessCategory;
        }

        public bool Remove(Guid Id)
        {
            var businessCategory = Get(Id);
            if (businessCategory == null) return false;
            businessCategoryRepository.Remove(businessCategory);
            return true;
        }


        public List<Domain.ViewModel.BusinessCategoryGroup> GetGroup()
        {
            List<BusinessCategoryGroup> listreturn = new List<BusinessCategoryGroup>();
            var listbs = Get().Where(c => c.ParentId == null);
            if (listbs != null && listbs.Count() > 0)
            {
                foreach (BusinessCategory parent in listbs)
                {
                    BusinessCategoryGroup p = new BusinessCategoryGroup()
                    {
                        Id = parent.Id,
                        Name = parent.Name,
                        ParentId = null,
                        Children = new List<BusinessCategoryGroup>()
                    };
                    var all = Get();
                    //get all children of this parent
                    var children = (from a in all
                                    where a.ParentId != null && a.ParentId == p.Id
                                    select new BusinessCategoryGroup()
                                    {
                                        Id = a.Id,
                                        Name = a.Name,
                                        ParentId = a.ParentId,
                                    }).ToList();

                    //set children
                    if (children != null && children.Count() > 0)
                    {
                        p.Children = children;

                        foreach (BusinessCategoryGroup child in children)
                        {
                            var childrenlvl = (from a in all
                                               where a.ParentId != null && a.ParentId == child.Id
                                               select new BusinessCategoryGroup()
                                               {
                                                   Id = a.Id,
                                                   Name = a.Name,
                                                   ParentId = a.ParentId,
                                               }).ToList();

                            if (childrenlvl != null && childrenlvl.Count > 0)
                            {
                                child.Children = childrenlvl;
                            }
                        }

                    }

                    //add to list 
                    listreturn.Add(p);
                }

            }
            return listreturn;
        }


        public BusinessCategory GetByFactualCategoryId(int id)
        {
            var obj = businessCategoryRepository.Get.FirstOrDefault(t => t.CategoryId == id);
            return obj;
        }
    }
}
