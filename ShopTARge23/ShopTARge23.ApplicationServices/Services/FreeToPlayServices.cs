using Nancy.Json;
using ShopTARge23.Core.Dto.FreeToPlayDtos;
using ShopTARge23.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopTARge23.ApplicationServices.Services
{
	public class FreeToPlayServices : IFreeToPlayServices
	{
		public async Task<FreeToPlayResultDto> FreeToPlayResult(FreeToPlayResultDto dto)
		{
			var url = "https://www.freetogame.com/api/games?platform=pc";

			using (WebClient client = new WebClient())
			{
				string json = client.DownloadString(url);
				List<FreeToPlayRootDto> freeToPlayResult = JsonSerializer.Deserialize<List<FreeToPlayRootDto>>(json);

				if (freeToPlayResult != null)
				{
					dto.FreeToPlay = freeToPlayResult;
				}
			}

			return dto;
		}
	}
}