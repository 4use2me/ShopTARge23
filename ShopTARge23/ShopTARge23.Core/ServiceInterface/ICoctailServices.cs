using ShopTARge23.Core.Dto.CoctailsDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopTARge23.Core.ServiceInterface
{
    public interface ICoctailServices
    {
        // Kokteilide otsing
        Task<List<CoctailSearchDto>> GetCocktailsAsync(string searchTerm);

        // Kokteili detailide päring
        Task<CoctailDetailDto> GetCoctailDetailsAsync(string idDrink);
    }
}

