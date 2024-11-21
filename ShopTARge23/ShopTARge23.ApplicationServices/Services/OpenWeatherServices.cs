using Nancy.Json;
using ShopTARge23.Core.Dto.WeatherDtos;
using ShopTARge23.Core.Dto.WeatherDtos.OpenWeatherDtos;
using ShopTARge23.Core.ServiceInterface;
using System.Net;

namespace ShopTARge23.ApplicationServices.Services
{
    public class OpenWeatherServices : IOpenWeatherServices
    {
        public async Task<OpenWeatherResultDto> OpenWeatherResult(OpenWeatherResultDto dto)
        {
            string OpenWeatherApiKey = "20cba06d4e368dc5c4d4307cce23e7c4";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={dto.City}&appid={OpenWeatherApiKey}&units=metric";

            //api call puhul peab andmed deserialiseerima
            //andmed tulevad JSOn kujul siia ja need tuleb muuta c# arusaadavaks
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                OpenWeatherRootDto weatherResult = new JavaScriptSerializer()
                    .Deserialize<OpenWeatherRootDto>(json);

                dto.City = weatherResult.Name;
                dto.Temp = weatherResult.Main.Temp;
                dto.FeelsLike = weatherResult.Main.Feels_like;
                dto.Humidity = weatherResult.Main.Humidity;
                dto.Pressure = weatherResult.Main.Pressure;
                dto.WindSpeed = weatherResult.Wind.Speed;
                dto.Description = weatherResult.Weather[0].Description;
            }

            return dto;
        }
    }
}
