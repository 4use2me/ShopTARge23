using ShopTARge23.Core.Dto.CoctailsDtos;

namespace ShopTARge23.Core.ServiceInterface
{
    public interface ICoctailServices
    {
        // Search for cocktails
        Task<List<CoctailSearchDto>> GetCocktailsAsync(string searchTerm);
        Task<List<CoctailSearchDto>> GetCocktailsByIngredientAsync(string ingredient);

        // Request for cocktail details
        Task<CoctailDetailDto> GetCoctailDetailsAsync(string idDrink);
    }
}

