using ShopTARge23.Core.Dto.WeatherDtos;

namespace ShopTARge23.Core.ServiceInterface
{
    public interface IOpenWeatherServices
    {
        Task<OpenWeatherResultDto> OpenWeatherResult(OpenWeatherResultDto dto);
    }
}
