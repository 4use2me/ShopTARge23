using Nancy.Json;
using ShopTARge23.Core.Dto.WeatherDtos;
using ShopTARge23.Core.Dto.WeatherDtos.OpenWeatherDtos;
using ShopTARge23.Core.ServiceInterface;
using System.Net;
using System.Web;

namespace ShopTARge23.ApplicationServices.Services
{
    public class OpenWeatherServices : IOpenWeatherServices
    {
        public async Task<OpenWeatherResultDto> OpenWeatherResult(OpenWeatherResultDto dto)
        {
            string OpenWeatherApiKey = "20cba06d4e368dc5c4d4307cce23e7c4";
			string pixabayApiKey = "47208038-bd9e1972a245230a0cad8c8af";
			
            string weatherurl = $"https://api.openweathermap.org/data/2.5/weather?q={dto.City}&appid={OpenWeatherApiKey}&units=metric";

            //api call puhul peab andmed deserialiseerima
            //andmed tulevad JSOn kujul siia ja need tuleb muuta c# arusaadavaks
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(weatherurl);
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

            // Pixabay API URL linna pildi otsimiseks
            string city = HttpUtility.UrlEncode(dto.City); // Kodeeri linnanimi
            string pixabayUrl = $"https://pixabay.com/api/?key={pixabayApiKey}&q={city}&image_type=photo&category=places";

			// Teeme Pixabay päringu
			using (WebClient client = new WebClient())
			{
                string pixabayJson = client.DownloadString(pixabayUrl);
                Console.WriteLine(pixabayJson); // Kontrolli vastust

                PixabayRootDto pixabayResult = new JavaScriptSerializer()
                    .Deserialize<PixabayRootDto>(pixabayJson);

                // Kasutame esimest saadud pilti
                if (pixabayResult.Hits != null && pixabayResult.Hits.Count > 0)
                {
                    dto.CityImageUrl = pixabayResult.Hits[0].WebformatURL; // Esimene pilt
                    System.Diagnostics.Debug.WriteLine($"Pixabay Image URL: {dto.CityImageUrl}");

                }
                else
                {
                    dto.CityImageUrl = dto.CityImageUrl = "/images/weather1.jpg";
                }

            }

            return dto;
		}
	}
}
