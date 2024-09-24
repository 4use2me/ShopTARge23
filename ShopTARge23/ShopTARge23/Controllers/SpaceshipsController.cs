using Microsoft.AspNetCore.Mvc;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;
using ShopTARge23.Models.Spaceships;

namespace ShopTARge23.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopTARge23Context _context;
        private readonly ISpaceshipsServices _spaceshipServices;

        public SpaceshipsController
            (
            ShopTARge23Context context,
            ISpaceshipsServices spaceshipsServices
            )
        {
            _context = context;
            _spaceshipServices = spaceshipsServices;
        }


        public IActionResult Index()
        {
            var result = _context.Spaceships
                .Select(x => new SpaceshipsIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Typename = x.Typename,
                    BuiltDate = x.BuiltDate,
                    Crew = x.Crew,
                });

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var spaceship = await _spaceshipServices.DetailAsync(id);

            if (spaceship == null)
            {
                return View("Error");
            }

            var vm = new SpaceshipDetailsViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name; 
            vm.Typename = spaceship.Typename;
            vm.SpaceshipModel = spaceship.SpaceshipModel;
            vm.BuiltDate = spaceship.BuiltDate;
            vm.Crew = spaceship.Crew;   
            vm.EnginePower = spaceship.EnginePower; 
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;


            return View(vm);
        }
    }
}
