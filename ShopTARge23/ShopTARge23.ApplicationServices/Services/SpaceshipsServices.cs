using Microsoft.EntityFrameworkCore;
using ShopTARge23.Core.Domain;
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

    }
}
