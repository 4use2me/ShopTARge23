using ShopTARge23.Core.Dto.CoctailsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge23.Core.ServiceInterface
{
    public interface ICoctailServices
    {
        Task<CoctailResultDto> GetCoctails(CoctailResultDto dto);

    }
}