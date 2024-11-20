using System.Threading.Tasks;
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
        public IActionResult Index(string searchQuery)
        {
            var model = new SearchCoctailViewModel
            {
                SearchCoctail = searchQuery,
                Results = new List<CoctailViewModel>()
            };

            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Kutsu teenust, et otsida kokteile (vajadusel lisa teenuskutse loogika)
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(SearchCoctailViewModel model)
        {
            // Kui mudel ei ole kehtiv, tagastame vaate
            if (!ModelState.IsValid)
            {
                model.Results = model.Results ?? new List<CoctailViewModel>();  // Veendume, et Results on algväärtustatud
                return View(model);
            }

            if (string.IsNullOrEmpty(model.SearchCoctail))
            {
                ModelState.AddModelError("", "Please enter a cocktail name to search.");
                model.Results = model.Results ?? new List<CoctailViewModel>();  // Veendume, et Results on algväärtustatud
                return View(model);
            }

            // Otsime kokteile teenusest
            var coctailsDto = await _coctailServices.GetCocktailsAsync(model.SearchCoctail);

            // Kui ei leita kokteile, kuvame veateate
            if (coctailsDto == null || coctailsDto.Count == 0)
            {
                ModelState.AddModelError("", $"No results found for \"{model.SearchCoctail}\".");
            }

            // Veenduge, et Results on initsialiseeritud tühi list
            model.Results = model.Results ?? new List<CoctailViewModel>();

            // Kui coctailsDto ei ole null, täiendame Results listi
            if (coctailsDto != null)
            {
                foreach (var coctailDto in coctailsDto)
                {
                    var viewModel = new CoctailViewModel
                    {
                        IdDrink = coctailDto.IdDrink,
                        StrDrink = coctailDto.StrDrink
                        // Täiendage vastavalt vajadusele
                    };

                    model.Results.Add(viewModel);  // Lisame Resultsi listi
                }
            }

            return View(model);
        }

        // Kokteili detailide kuvamine
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

            // Konverteeri CoctailDetailDto CoctailViewModel-iks
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
                // Täida kõik vajalikud atribuudid CoctailDetailDto-st CoctailViewModel-isse
            };

            // Edasta otsingupäring lingina
            ViewData["SearchQuery"] = searchQuery; // Edasta otsinguväärtus
            return View("Coctail", coctailViewModel);
        }


    }
}
