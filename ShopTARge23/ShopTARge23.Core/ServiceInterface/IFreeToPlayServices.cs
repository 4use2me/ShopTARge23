using ShopTARge23.Core.Dto.FreeToPlayDtos;

namespace ShopTARge23.Core.ServiceInterface
{
	public interface IFreeToPlayServices
	{
		Task<FreeToPlayResultDto> FreeToPlayResult(FreeToPlayResultDto dto);
	}
}
