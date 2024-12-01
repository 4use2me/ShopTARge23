using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Models.Coctails;

namespace ShopTARge23.Controllers
{
    public class CoctailsController : Controller
    {
        private readonly ICoctailServices _coctailServices;

        public CoctailsController(ICoctailServices coctailServices)
        {
            _coctailServices = coctailServices;
        }

        [HttpGet]
        public IActionResult Index(string searchQuery, bool clearSession = false)
        {
            if (clearSession)
            {
                HttpContext.Session.Remove("SearchCoctail");
                HttpContext.Session.Remove("Results");
                HttpContext.Session.Remove("SearchIngredient");
                HttpContext.Session.Remove("IngredientResults");
            }

            var model = new SearchCoctailViewModel
            {
                SearchCoctail = null,
                SearchIngredient = null,
                Results = new List<CoctailViewModel>(),
                IngredientResults = new List<CoctailViewModel>()
            };

            // If there is data in Session and clearSession is not true
            if (!clearSession)
            {
                // Download the cocktail name and ingredient search results from the session
                var searchCoctail = HttpContext.Session.GetString("SearchCoctail");
                var resultsJson = HttpContext.Session.GetString("Results");
                var searchIngredient = HttpContext.Session.GetString("SearchIngredient");
                var ingredientResultsJson = HttpContext.Session.GetString("IngredientResults");

                if (!string.IsNullOrEmpty(searchCoctail) && !string.IsNullOrEmpty(resultsJson))
                {
                    model.SearchCoctail = searchCoctail;
                    model.Results = JsonSerializer.Deserialize<List<CoctailViewModel>>(resultsJson);
                }
                if (!string.IsNullOrEmpty(searchIngredient) && !string.IsNullOrEmpty(ingredientResultsJson))
                {
                    model.SearchIngredient = searchIngredient;
                    model.IngredientResults = JsonSerializer.Deserialize<List<CoctailViewModel>>(ingredientResultsJson);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchCoctailViewModel model)
        {
            if (!string.IsNullOrEmpty(model.SearchCoctail))
            {
                var coctailsDto = await _coctailServices.GetCocktailsAsync(model.SearchCoctail);

                model.Results = coctailsDto?.Select(dto => new CoctailViewModel
                {
                    IdDrink = dto.IdDrink,
                    StrDrink = dto.StrDrink
                }).ToList() ?? new List<CoctailViewModel>();

                HttpContext.Session.SetString("SearchCoctail", model.SearchCoctail);
                HttpContext.Session.SetString("Results", JsonSerializer.Serialize(model.Results));
            }

            if (!string.IsNullOrEmpty(model.SearchIngredient))
            {
                var ingredientDto = await _coctailServices.GetCocktailsByIngredientAsync(model.SearchIngredient);

                model.IngredientResults = ingredientDto?.Select(dto => new CoctailViewModel
                {
                    IdDrink = dto.IdDrink,
                    StrDrink = dto.StrDrink
                }).ToList() ?? new List<CoctailViewModel>();

                HttpContext.Session.SetString("SearchIngredient", model.SearchIngredient);
                HttpContext.Session.SetString("IngredientResults", JsonSerializer.Serialize(model.IngredientResults));
            }

            return View(model);
        }

        // Displaying cocktail details
        [HttpGet]
        public async Task<IActionResult> Details(string idDrink, string searchQuery)
        {
            if (string.IsNullOrEmpty(idDrink))
            {
                return NotFound();
            }

            var coctailDetailDto = await _coctailServices.GetCoctailDetailsAsync(idDrink);

            if (coctailDetailDto == null)
            {
                return NotFound();
            }

            // Convert CoctailDetailDto to CoctailViewModel
            var coctailViewModel = new CoctailViewModel
            {
                IdDrink = coctailDetailDto.IdDrink,
                StrDrink = coctailDetailDto.StrDrink,
                StrGlass = coctailDetailDto.StrGlass,
                StrDrinkThumb = coctailDetailDto.StrDrinkThumb,
                StrInstructions = coctailDetailDto.StrInstructions,
                StrIngredient1 = coctailDetailDto.StrIngredient1,
                StrIngredient2 = coctailDetailDto.StrIngredient2,
                StrIngredient3 = coctailDetailDto.StrIngredient3,
                StrIngredient4 = coctailDetailDto.StrIngredient4,
                StrIngredient5 = coctailDetailDto.StrIngredient5,
                StrIngredient6 = coctailDetailDto.StrIngredient6,
                StrIngredient7 = coctailDetailDto.StrIngredient7,
                StrIngredient8 = coctailDetailDto.StrIngredient8,
                StrIngredient9 = coctailDetailDto.StrIngredient9,
                StrIngredient10 = coctailDetailDto.StrIngredient10,
                StrIngredient11 = coctailDetailDto.StrIngredient11,
                StrIngredient12 = coctailDetailDto.StrIngredient12,
                StrIngredient13 = coctailDetailDto.StrIngredient13,
                StrIngredient14 = coctailDetailDto.StrIngredient14,
                StrIngredient15 = coctailDetailDto.StrIngredient15
                // Fill in all the necessary attributes from CoctailDetailDto to CoctailViewModel
            };

            // Submit a search query as a link
            ViewData["SearchQuery"] = searchQuery; // Submit search value
            return View("Coctail", coctailViewModel);
        }
    }
}