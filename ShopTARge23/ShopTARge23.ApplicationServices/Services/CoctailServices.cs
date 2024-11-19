using Nancy.Json;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ShopTARge23.Core.Dto.CoctailsDtos;
using ShopTARge23.Core.ServiceInterface;

namespace ShopTARge23.ApplicationServices.Services
{
    public class CoctailServices : ICoctailServices
    {
        public async Task<CoctailResultDto> GetCoctails(CoctailResultDto dto)

        {
            string apiKey = "1";
            string apiCallUrl = $"https://www.thecocktaildb.com/api/json/v1/{apiKey}/search.php?s={dto.StrDrink}";
            using (WebClient client = new())
            {
                string json = client.DownloadString(apiCallUrl);
                CoctailRootDto coctailResult = new JavaScriptSerializer().Deserialize<CoctailRootDto>(json);

                dto.IdDrink = coctailResult.Drinks[0].IdDrink;
                dto.StrDrink = coctailResult.Drinks[0].StrDrink;
                dto.StrDrinkAlternate = coctailResult.Drinks[0].StrDrinkAlternate;
                dto.StrTags = coctailResult.Drinks[0].StrTags;
                dto.StrVideo = coctailResult.Drinks[0].StrVideo;
                dto.StrCategory = coctailResult.Drinks[0].StrCategory;
                dto.StrIBA = coctailResult.Drinks[0].StrGlass;
                dto.StrAlcoholic = coctailResult.Drinks[0].StrAlcoholic;
                dto.StrGlass = coctailResult.Drinks[0].StrGlass;
                dto.StrInstructions = coctailResult.Drinks[0].StrInstructions;
                dto.StrInstructionsES = coctailResult.Drinks[0].StrInstructionsES;
                dto.StrInstructionsDE = coctailResult.Drinks[0].StrInstructionsDE;
                dto.StrInstructionsFR = coctailResult.Drinks[0].StrInstructionsFR;
                dto.StrInstructionsIT = coctailResult.Drinks[0].StrInstructionsIT;
                dto.StrInstructionsZHHANS = coctailResult.Drinks[0].StrInstructionsZHHANS;
                dto.StrInstructionsZHHANT = coctailResult.Drinks[0].StrInstructionsZHHANT;
                dto.StrDrinkThumb = coctailResult.Drinks[0].StrDrinkThumb;
                dto.StrIngredient1 = coctailResult.Drinks[0].StrIngredient1;
                dto.StrIngredient2 = coctailResult.Drinks[0].StrIngredient2;
                dto.StrIngredient3 = coctailResult.Drinks[0].StrIngredient3;
                dto.StrIngredient4 = coctailResult.Drinks[0].StrIngredient4;
                dto.StrIngredient5 = coctailResult.Drinks[0].StrIngredient5;
                dto.StrIngredient6 = coctailResult.Drinks[0].StrIngredient6;
                dto.StrIngredient7 = coctailResult.Drinks[0].StrIngredient7;
                dto.StrIngredient8 = coctailResult.Drinks[0].StrIngredient8;
                dto.StrIngredient9 = coctailResult.Drinks[0].StrIngredient9;
                dto.StrIngredient10 = coctailResult.Drinks[0].StrIngredient10;
                dto.StrIngredient11 = coctailResult.Drinks[0].StrIngredient11;
                dto.StrIngredient12 = coctailResult.Drinks[0].StrIngredient12;
                dto.StrIngredient13 = coctailResult.Drinks[0].StrIngredient13;
                dto.StrIngredient14 = coctailResult.Drinks[0].StrIngredient14;
                dto.StrIngredient15 = coctailResult.Drinks[0].StrIngredient15;
                dto.StrMeasure1 = coctailResult.Drinks[0].StrMeasure1;
                dto.StrMeasure2 = coctailResult.Drinks[0].StrMeasure2;
                dto.StrMeasure3 = coctailResult.Drinks[0].StrMeasure3;
                dto.StrMeasure4 = coctailResult.Drinks[0].StrMeasure4;
                dto.StrMeasure5 = coctailResult.Drinks[0].StrMeasure5;
                dto.StrMeasure6 = coctailResult.Drinks[0].StrMeasure6;
                dto.StrMeasure7 = coctailResult.Drinks[0].StrMeasure7;
                dto.StrMeasure8 = coctailResult.Drinks[0].StrMeasure8;
                dto.StrMeasure9 = coctailResult.Drinks[0].StrMeasure9;
                dto.StrMeasure10 = coctailResult.Drinks[0].StrMeasure10;
                dto.StrMeasure11 = coctailResult.Drinks[0].StrMeasure11;
                dto.StrMeasure12 = coctailResult.Drinks[0].StrMeasure12;
                dto.StrMeasure13 = coctailResult.Drinks[0].StrMeasure13;
                dto.StrMeasure14 = coctailResult.Drinks[0].StrMeasure14;
                dto.StrMeasure15 = coctailResult.Drinks[0].StrMeasure15;
                dto.StrImageSource = coctailResult.Drinks[0].StrImageSource;
                dto.StrImageAttribution = coctailResult.Drinks[0].StrImageAttribution;
                dto.StrCreativeCommonsConfirmed = coctailResult.Drinks[0].StrCreativeCommonsConfirmed;
                dto.DateModified = coctailResult.Drinks[0].DateModified;

            }
            return dto;
        }
    }
}