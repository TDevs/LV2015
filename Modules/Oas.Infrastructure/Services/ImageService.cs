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
    public class ImageService : IImageService
    {
        private readonly IRepository<Image> imageRepository;
        public ImageService(IRepository<Image> imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        public List<Domain.Image> Get()
        {
            return imageRepository.Get
                .Include(c => c.Business)
                .ToList();
        }

        public Domain.Image Get(Guid Id)
        {
            return imageRepository.Get
                .Include(t => t.Business)
                .FirstOrDefault(t => t.Id.Equals(Id));
        }

        public Domain.Image Create(Domain.Image business)
        {
            business.Id = Guid.NewGuid();
            var obj = imageRepository.Add(business);
            return obj;
        }

        public Domain.Image Update(Domain.Image business)
        {
            var obj = imageRepository.Update(business);
            return obj;
        }

        public bool Delete(Guid Id)
        {
            var obj = Get(Id);
            if (obj == null) return false;
            imageRepository.Remove(obj);
            return true;
        }

        public Domain.Image Find(object[] keyValues)
        {
            throw new NotImplementedException();
        }
    }
}
