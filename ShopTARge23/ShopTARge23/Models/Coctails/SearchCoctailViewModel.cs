using ShopTARge23.Core.Dto.CoctailsDtos;

namespace ShopTARge23.Models.Coctails
{
    public class SearchCoctailViewModel
    {
        public string SearchCoctail { get; set; }  // Otsingu sisend
        public List<CoctailViewModel> Results { get; set; } = new List<CoctailViewModel>();  // Tulemused
    }


}

