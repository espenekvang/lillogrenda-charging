using FluentAssertions;
using Lillogrenda.Charging.Domain.Entities;

namespace Lillogrenda.Charging.Domain.UnitTests;

public class ChargingSessionTests
{
    [Fact]
    public void OneHourSessionEnergyReturned()
    {
        //arrage
        var session = new ChargingSession
        {
            Start = new DateTimeOffset(2023,04,24, 19,00,00,new TimeSpan()),
            End = new DateTimeOffset(2023,04,24, 20,00,00,new TimeSpan()),
            EnergyInkWh = 10
        };

        //act
        var energyPerHour = session.GetEnergyPerHour();

        //assert
        energyPerHour.Should().Be(10);
    }
    
    [Fact]
    public void TwoHourSessionEnergySplitReturned()
    {
        //arrage
        var session = new ChargingSession
        {
            Start = new DateTimeOffset(2023,04,24, 19,00,00,new TimeSpan()),
            End = new DateTimeOffset(2023,04,24, 21,00,00,new TimeSpan()),
            EnergyInkWh = 10
        };

        //act
        var energyPerHour = session.GetEnergyPerHour();

        //assert
        energyPerHour.Should().Be(5);
    }
    
    [Fact]
    public void OneAndAHalfHourSessionEnergySplitReturned()
    {
        //arrage
        var session = new ChargingSession
        {
            Start = new DateTimeOffset(2023,04,24, 19,00,00,new TimeSpan()),
            End = new DateTimeOffset(2023,04,24, 20,30,00,new TimeSpan()),
            EnergyInkWh = 15
        };

        //act
        var energyPerHour = session.GetEnergyPerHour();

        //assert
        energyPerHour.Should().Be(10);
    }
    
    [Fact]
    public void OneAndAHalfHourTwoChargingHoursReturned()
    {
        //arrage
        var session = new ChargingSession
        {
            Start = new DateTimeOffset(2023,04,24, 19,00,00,new TimeSpan()),
            End = new DateTimeOffset(2023,04,24, 20,30,00,new TimeSpan()),
            EnergyInkWh = 15
        };

        //act
        var chargingHours = session.GetChargingHours().ToList();

        //assert
        chargingHours.Should().HaveCount(2);
        chargingHours[0].Hour.Should().Be(19);
        chargingHours[1].Hour.Should().Be(20);
    }
}