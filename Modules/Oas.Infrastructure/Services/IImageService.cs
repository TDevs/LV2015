using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oas.Infrastructure.Domain;

namespace Oas.Infrastructure.Services
{
    public interface IImageService
    {

        List<Image> Get();
        Image Get(Guid Id);

        Image Create(Image image);

        Image Update(Image image);

        bool Delete(Guid Id);

        Image Find(object[] keyValues);
        

    }
}
