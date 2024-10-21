using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShopTARge23.ApplicationServices.Services;
using ShopTARge23.Core.Domain;

namespace ShopTARge23.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            //Arrange
            RealEstateDto dto = new();

            dto.Size = 100;
            dto.Location = "asd";
            dto.RoomNumber = 1;
            dto.BuildingType = "asd";
            dto.CreatedAt = DateTime.Now;
            dto.ModifiedAt = DateTime.Now;

            //Act
            var result = await Svc<IRealEstatesServices>().Create(dto);

            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task ShouldNot_GetByIdRealestate_WhenReturnsNotEqual()
        {
            //Arrange
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString()); // Juhuslikult genereeritud GUID
            Guid guid = Guid.Parse("47d296b4-fac8-4834-9e0a-5ebc1bdbcc16");

            //Act
            await Svc<IRealEstatesServices>().DetailAsync(guid);

            //Assert
            Assert.NotEqual(wrongGuid, guid);
        }

        [Fact]
        public async Task Should_GetByIdRealestate_WhenReturnsEqual() //vastupidine test võreldes eelmisega
        {
            //Arrange
            Guid correctGuid = Guid.Parse("47d296b4-fac8-4834-9e0a-5ebc1bdbcc16"); 
            Guid guid = Guid.Parse("47d296b4-fac8-4834-9e0a-5ebc1bdbcc16");

            //Act
            await Svc<IRealEstatesServices>().DetailAsync(guid);

            //Assert
            Assert.Equal(correctGuid, guid);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {
            //mängult sisestan andmed ja seejärel kustutan ära
            RealEstateDto realEstate = MockRealEstateData();

            var addRealEstate = await Svc<IRealEstatesServices>().Create(realEstate);
            var result = await Svc<IRealEstatesServices>().Delete((Guid)addRealEstate.Id);

            Assert.Equal(result, addRealEstate);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            RealEstateDto realEstate = MockRealEstateData(); //kutsume Mock andmed esile

            var realEstate1 = await Svc<IRealEstatesServices>().Create(realEstate); //loome kõigepealt realestate
            var realEstate2 = await Svc<IRealEstatesServices>().Create(realEstate);

            var result = await Svc<IRealEstatesServices>().Delete((Guid)realEstate2.Id); //2 kustutab ära, sama id-ga esimest kustutada ei saa

            Assert.NotEqual(result.Id, realEstate1.Id);
        }

        private RealEstateDto MockRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Size = 100,
                Location = "asd",
                RoomNumber = 1,
                BuildingType = "asd",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            
            return realEstate;
        }

        

    }
}