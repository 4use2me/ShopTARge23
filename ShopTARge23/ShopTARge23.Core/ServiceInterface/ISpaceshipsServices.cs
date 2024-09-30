using ShopTARge23.Core.Domain;
using ShopTARge23.Core.Dto;
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
        Task<Spaceship> Update(SpaceshipDto dto);
        Task<Spaceship> Delete(Guid id);
        Task<Spaceship> Create(SpaceshipDto dto);
    }
}
