using Microsoft.AspNetCore.Mvc;
using ShopTARge23.Data;
using ShopTARge23.Models.Spaceships;

namespace ShopTARge23.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopTARge23Context _context;

        public SpaceshipsController
            (
            ShopTARge23Context context
            )
        {
            _context = context;
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
    }
}
