using FakeItEasy;
using FluentAssertions;
using Lillogrenda.Charging.Application.Invoices;
using Lillogrenda.Charging.Domain.Entities;
using Lillogrenda.Charging.Domain.Services;


namespace Lillogrenda.Charging.Application.UnitTests.Invoices;

public class ChargingHourExtractorTests
{
    [Fact]
    public async Task OneAndAHalfHourTwoChargingHoursReturned()
    {
        //arrange
        var session = new ChargingSession
        {
            Start = new DateTimeOffset(2023,04,24, 19,00,00,new TimeSpan()),
            End = new DateTimeOffset(2023,04,24, 20,30,00,new TimeSpan()),
            EnergyInkWh = 15
        };

        var priceService = A.Fake<IPriceService>();
        A.CallTo(() => priceService.GetPricesAsync(A<DateOnly>.Ignored, A<CancellationToken>.Ignored))
            .Returns(new List<EnergyPrice>
            {
                new(session.Start, session.Start.AddMinutes(59), 10, Currency.Nok),
                new(session.End.AddMinutes(-30), session.End.AddMinutes(29), 20, Currency.Nok),
            });
        var extractor = new ChargingHourExtractor(priceService);

        //act
        var chargingHours = (await extractor.ExtractFromAsync( session, CancellationToken.None)).ToList();

        //assert
        chargingHours.Should().HaveCount(2);
        chargingHours[0].Hour.Should().Be(19);
        chargingHours[1].Hour.Should().Be(20);
    }
}