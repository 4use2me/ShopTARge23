using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShopTARge23.ApplicationServices.Services;
using ShopTARge23.Core.Domain;
using Moq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
        public async Task Should_GetByIdRealestate_WhenReturnsEqual() //vastupidine test vıreldes eelmisega
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
            //m‰ngult sisestan andmed ja seej‰rel kustutan ‰ra
            RealEstateDto realEstate = MockRealEstateData();

            var addRealEstate = await Svc<IRealEstatesServices>().Create(realEstate);
            var result = await Svc<IRealEstatesServices>().Delete((Guid)addRealEstate.Id);

            Assert.Equal(result, addRealEstate);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            RealEstateDto realEstate = MockRealEstateData(); //kutsume Mock andmed esile

            var realEstate1 = await Svc<IRealEstatesServices>().Create(realEstate); //loome kıigepealt realestate
            var realEstate2 = await Svc<IRealEstatesServices>().Create(realEstate);

            var result = await Svc<IRealEstatesServices>().Delete((Guid)realEstate2.Id); //2 kustutab ‰ra, sama id-ga esimest kustutada ei saa

            Assert.NotEqual(result.Id, realEstate1.Id);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            var guid = Guid.Parse("47d296b4-fac8-4834-9e0a-5ebc1bdbcc16");

            //uued andmed
            RealEstateDto dto = MockRealEstateData();

            //andmebaasis olevad andmed
            RealEstate domain = new();

            domain.Id = Guid.Parse("47d296b4-fac8-4834-9e0a-5ebc1bdbcc16");
            domain.Location = "asdasd";
            domain.Size = 34;
            domain.RoomNumber = 1234;
            domain.BuildingType = "asd";
            domain.CreatedAt = DateTime.UtcNow;
            domain.ModifiedAt = DateTime.UtcNow;

            await Svc<IRealEstatesServices>().Update(dto);

            Assert.Equal(domain.Id, guid);
            Assert.DoesNotMatch(domain.Location, dto.Location);
            Assert.DoesNotMatch(domain.RoomNumber.ToString(), dto.RoomNumber.ToString()); //kasutades notequal ei pea teisendama stringiks
            Assert.NotEqual(domain.Size, dto.Size);
            Assert.Matches(domain.BuildingType, dto.BuildingType);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateDataVersion2()
        {
            //kasutame kahte mock andmebaasi ja siis vırdleme omavahel

            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstatesServices>().Create(dto);

            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstatesServices>().Update(update);

            Assert.DoesNotMatch(result.Location, createRealEstate.Location);
            Assert.NotEqual(result.ModifiedAt, createRealEstate.ModifiedAt);
        }

        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenDidNotUpdateData()
        {
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstatesServices>().Create(dto);

            RealEstateDto nullUpdate = MockNullRealEstateData();
            var result = await Svc<IRealEstatesServices>().Update(nullUpdate);

            // Assert
            Assert.NotEqual(createRealEstate.Id, result.Id);
        }

        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenSizeIsZero()
        {
            // Arrange
            RealEstateDto invalidDto = MockInvalidRealEstateData();

            // Act
            var result = await Svc<IRealEstatesServices>().Update(invalidDto);

            // Assert
            Assert.Null(result); // Kontrollime, et tulemus oleks null, kuna v‰rskendus ebaınnestus
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

        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Location = "vbn",
                Size = 44,
                RoomNumber = 6,
                BuildingType = "vbn",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1),
            };
            return realEstate;
        }
        private RealEstateDto MockNullRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Id = null,
                Location = "vbn",
                Size = 44,
                RoomNumber = 6,
                BuildingType = "vbn",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1),
            };
            return realEstate;
        }
        private RealEstateDto MockInvalidRealEstateData()
        {
            return new RealEstateDto
            {
                Size = 0, // Kehtetu v‰‰rtus
                Location = "Invalid Location",
                RoomNumber = 1,
                BuildingType = "Invalid Building",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
        }

    }
}