using ShopTARge23.Core.Domain;
using ShopTARge23.Core.Dto;

namespace ShopTARge23.Core.ServiceInterface
{
    public interface IRealEstatesServices
    {
        Task<RealEstate> DetailAsync(Guid id);
        Task<RealEstate> Update(RealEstateDto dto);
        Task<RealEstate> Delete(Guid id);
        Task<RealEstate> Create(RealEstateDto dto);
    }
}
