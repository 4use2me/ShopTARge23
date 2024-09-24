using Microsoft.EntityFrameworkCore;
using ShopTARge23.Core.Domain;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge23.ApplicationServices.Services
{

    public class SpaceshipsServices : ISpaceshipsServices
    {
        private readonly ShopTARge23Context _context;

        public SpaceshipsServices
            (
            ShopTARge23Context context
            )
        {
            _context = context;
        }

        public async Task<Spaceship> DetailAsync(Guid id)
        {
            var result = await _context.Spaceships
                .FirstOrDefaultAsync( x => x.Id == id );
            return result;
        }

        public async Task<Spaceship> Update(SpaceshipDto dto)
        {
            Spaceship domain = new();

            domain.Id = dto.Id;
            domain.Name = dto.Name;
            domain.Typename = dto.Typename;
            domain.SpaceshipModel = dto.SpaceshipModel;
            domain.Crew = dto.Crew;
            domain.EnginePower = dto.EnginePower;
            domain.BuiltDate = dto.BuiltDate;
            domain.CreatedAt = dto.CreatedAt;
            domain.ModifiedAt = DateTime.Now;

            _context.Spaceships.Update(domain);
            await _context.SaveChangesAsync();
            
            return domain;
        }

        public async Task<Spaceship> Delete(Guid id)
        {
            var spaceship = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Spaceships.Remove(spaceship);
            await _context.SaveChangesAsync();

            return spaceship;
        }

    }
}
