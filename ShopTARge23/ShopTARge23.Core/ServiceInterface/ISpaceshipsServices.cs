using ShopTARge23.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge23.Core.ServiceInterface
{
    public interface ISpaceshipsServices 
    {
        Task<Spaceship> DetailAsync(Guid id);
    }
}
