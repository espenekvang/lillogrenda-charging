using FakeItEasy;
using FluentAssertions;
using Lillogrenda.Charging.Application.Invoices;
using Lillogrenda.Charging.Domain.Entities;
using Lillogrenda.Charging.Domain.Services;


namespace Lillogrenda.Charging.Application.UnitTests.Invoices;

public class ChargingHourExtractorTests
{
    [Fact]
    public void OneAndAHalfHourTwoChargingHoursReturned()
    {
        //arrange
        var session = new ChargingSession
        {
            Start = new DateTimeOffset(2023,04,24, 19,00,00,new TimeSpan()),
            End = new DateTimeOffset(2023,04,24, 20,30,00,new TimeSpan()),
            EnergyInkWh = 15
        };

        var priceService = A.Fake<IPriceService>();
        var extractor = new ChargingHourExtractor(priceService);

        //act
        var chargingHours = extractor.ExtractFrom(new[] { session }).ToList();

        //assert
        chargingHours.Should().HaveCount(2);
        chargingHours[0].Hour.Should().Be(19);
        chargingHours[1].Hour.Should().Be(20);
    }
}