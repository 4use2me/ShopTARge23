namespace ShopTARge23.Core.Dto.WeatherDtos.OpenWeatherDtos
{
    public class PixabayRootDto
    {
        public int Total { get; set; }
        public int TotalHits { get; set; }
        public List<PixabayHitDto> Hits { get; set; }
    }

    public class PixabayHitDto
    {
        public string WebformatURL { get; set; }
        public string PreviewURL { get; set; }
    }

}
