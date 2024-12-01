using System.Net;
using ShopTARge23.Core.Dto.CoctailsDtos;
using ShopTARge23.Core.ServiceInterface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ShopTARge23.ApplicationServices.Services
{
    public class CoctailServices : ICoctailServices
    {
        public async Task<List<CoctailSearchDto>> GetCocktailsAsync(string searchTerm)
        {
            string apiKey = "1"; // API key
            string apiCallUrl = $"https://www.thecocktaildb.com/api/json/v1/{apiKey}/search.php?s={searchTerm}";
            using (WebClient client = new())
            {
                // Download data from API
                string json = client.DownloadString(apiCallUrl);
                var coctailResult = JsonConvert.DeserializeObject<CoctailRootDto>(json);

                List<CoctailSearchDto> result = new List<CoctailSearchDto>();

                // We go through all the cocktails we find and add them to the List
                if (coctailResult.Drinks != null)
                {
                    foreach (var drink in coctailResult.Drinks)
                    {
                        CoctailSearchDto dto = new CoctailSearchDto
                        {
                            IdDrink = drink.IdDrink,
                            StrDrink = drink.StrDrink,
                            StrDrinkThumb = drink.StrDrinkThumb,
                        };

                        result.Add(dto);
                    }
                }

                return result;
            }
        }
        public async Task<List<CoctailSearchDto>> GetCocktailsByIngredientAsync(string ingredient)
        {
            string apiKey = "1";
            string apiCallUrl = $"https://www.thecocktaildb.com/api/json/v1/{apiKey}/filter.php?i={ingredient}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(apiCallUrl);

                // Deserialize to JObject for dynamic handling
                var coctailResult = JsonConvert.DeserializeObject<JObject>(json);
                var drinksToken = coctailResult["drinks"];

                List<CoctailSearchDto> result = new List<CoctailSearchDto>();

                // Handle different possible types
                if (drinksToken is JArray drinksArray)
                {
                    foreach (var drink in drinksArray)
                    {
                        CoctailSearchDto dto = new CoctailSearchDto
                        {
                            IdDrink = drink["idDrink"]?.ToString(),
                            StrDrink = drink["strDrink"]?.ToString(),
                            StrDrinkThumb = drink["strDrinkThumb"]?.ToString(),
                        };
                        result.Add(dto);
                    }
                }
                else if (drinksToken is JValue drinksValue && drinksValue.ToString() == "no data found")
                {
                    // Handle "no data found" case (empty result list)
                }

                return result;
            }
        }

        public async Task<CoctailDetailDto> GetCoctailDetailsAsync(string idDrink)
        {
            string apiKey = "1"; // API key
            string apiCallUrl = $"https://www.thecocktaildb.com/api/json/v1/{apiKey}/lookup.php?i={idDrink}";
            using (WebClient client = new WebClient())
            {
                // Download data from API
                string json = client.DownloadString(apiCallUrl);

                var coctailResult = JsonConvert.DeserializeObject<CoctailRootDto>(json);

                if (coctailResult.Drinks != null && coctailResult.Drinks.Count > 0)
                {
                    var drink = coctailResult.Drinks[0];

                    CoctailDetailDto dto = new CoctailDetailDto
                    {
                        IdDrink = drink.IdDrink,
                        StrDrink = drink.StrDrink,
                        StrDrinkThumb = drink.StrDrinkThumb,
                        StrTags = drink.StrTags,
                        StrVideo = drink.StrVideo,
                        StrCategory = drink.StrCategory,
                        StrIBA = drink.StrIBA,
                        StrAlcoholic = drink.StrAlcoholic,
                        StrGlass = drink.StrGlass,
                        StrInstructions = drink.StrInstructions,
                        StrIngredient1 = drink.StrIngredient1,
                        StrIngredient2 = drink.StrIngredient2,
                        StrIngredient3 = drink.StrIngredient3,
                        StrIngredient4 = drink.StrIngredient4,
                        StrIngredient5 = drink.StrIngredient5,
                        StrIngredient6 = drink.StrIngredient6,
                        StrIngredient7 = drink.StrIngredient7,
                        StrIngredient8 = drink.StrIngredient8,
                        StrIngredient9 = drink.StrIngredient9,
                        StrIngredient10 = drink.StrIngredient10,
                        StrIngredient11 = drink.StrIngredient11,
                        StrIngredient12 = drink.StrIngredient12,
                        StrIngredient13 = drink.StrIngredient13,
                        StrIngredient14 = drink.StrIngredient14,
                        StrIngredient15 = drink.StrIngredient15,
                        // Add other required fields...
                    };

                    return dto;
                }

                return null;
            }
        }
    }
}
