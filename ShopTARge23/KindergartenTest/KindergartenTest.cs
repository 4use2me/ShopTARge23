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
using System.Xml;

namespace ShopTARge23.KindergartenTest
{
    public class KindergartenTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_DeleteByIdKindergarten_WhenDidNotDeleteKindergarten()
        {
            KindergartenDto kindergarten = MockKindergartenData(); //kutsume Mock andmed esile

            var kindergarten1 = await Svc<IKindergartensServices>().Create(kindergarten); //loome kıigepealt kindergarteni
            var kindergarten2 = await Svc<IKindergartensServices>().Create(kindergarten);

            var result = await Svc<IKindergartensServices>().Delete((Guid)kindergarten2.Id); //2 kustutab ‰ra, sama id-ga esimest kustutada ei saa

            Assert.NotEqual(result.Id, kindergarten1.Id);
        }

        [Fact]
        public async Task Should_UpdateKindergarten_WhenUpdateData()
        {
            var guid = Guid.Parse("f8245993-1783-4224-b3ec-e927af82b3cb");

            //uued andmed
            KindergartenDto dto = MockKindergartenData();

            //andmebaasis olevad andmed
            Kindergarten domain = new();

            domain.Id = Guid.Parse("f8245993-1783-4224-b3ec-e927af82b3cb");
            domain.GroupName = "Ruudukad";
            domain.ChildrenCount = 34;
            domain.KindergartenName = "P‰ikeseratas";
            domain.Teacher = "Kai";
            domain.CreatedAt = DateTime.UtcNow;
            domain.UpdatedAt = DateTime.UtcNow;

            await Svc<IKindergartensServices>().Update(dto);

            Assert.Equal(domain.Id, guid);
            Assert.DoesNotMatch(domain.GroupName, dto.GroupName);
            Assert.DoesNotMatch(domain.ChildrenCount.ToString(), dto.ChildrenCount.ToString()); //kasutades notequal ei pea teisendama stringiks
            Assert.NotEqual(domain.ChildrenCount, dto.ChildrenCount);
            Assert.Matches(domain.KindergartenName, dto.KindergartenName);
            Assert.DoesNotMatch(domain.Teacher, dto.Teacher);
        }

        [Fact]
        public async Task ShouldNot_UpdateKindergarten_WhenDidNotUpdateData()
        {
            KindergartenDto dto = MockKindergartenData();
            var createKindergarten = await Svc<IKindergartensServices>().Create(dto);

            KindergartenDto nullUpdate = MockNullKindergartenData();
            var result = await Svc<IKindergartensServices>().Update(nullUpdate);

            // Assert
            Assert.NotEqual(createKindergarten.Id, result.Id);
        }

        [Fact]
        public async Task ShouldNot_AddKindergarten_WhenChildrenCountIsZero()
        {
            // Arrange
            KindergartenDto invalidDto = MockInvalidKindergartenData();

            // Act
            var result = await Svc<IKindergartensServices>().Create(invalidDto);

            // Assert
            Assert.Null(result); // Kontrollime, et tulemus oleks null, kuna v‰rskendus ebaınnestus
        }


        private KindergartenDto MockKindergartenData()
        {
            KindergartenDto kindergarten = new()
            {
                GroupName = "Triibukad",
                ChildrenCount = 24,
                KindergartenName = "P‰ikeseratas",
                Teacher = "Tiina",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            return kindergarten;
        }

        private KindergartenDto MockNullKindergartenData()
        {
            KindergartenDto kindergarten = new()
            {
                Id = null,
                GroupName = "T‰psikud",
                ChildrenCount = 20,
                KindergartenName = "P‰ikeseratas",
                Teacher = "Mai",
                CreatedAt = DateTime.Now.AddYears(1),
                UpdatedAt = DateTime.Now.AddYears(1),
            };
            return kindergarten;
        }
        private KindergartenDto MockInvalidKindergartenData()
        {
            return new KindergartenDto
            {
                ChildrenCount = 0, // Kehtetu v‰‰rtus
                GroupName = "",
                Teacher = "Kai",
                KindergartenName = "P‰ikeseratas",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

    }
}