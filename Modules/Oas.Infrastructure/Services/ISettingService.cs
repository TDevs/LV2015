using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface ISettingService
    {
        IList<Setting> Get();

        Setting Get(Guid Id);

        Setting GetDefault();

        Setting Add(Setting setting);

        Setting Update(Setting setting);

        bool Remove(Guid Id);
    }
}
