namespace ShopTARge23.Models.Coctails
{
    public class SearchCoctailViewModel
    {
        public string? SearchCoctail { get; set; }
        public string? SearchIngredient { get; set; }
        public List<CoctailViewModel> Results { get; set; } = new();
        public List<CoctailViewModel> IngredientResults { get; set; } = new();
    }
}
