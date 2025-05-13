using FluentAssertions;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using GoodHamburger.Application.DTOs;
using GoodHamburger.WebAPI;
using System.Net.Http.Json;
using System.Net;
using Xunit;

namespace GoodHamburger.Tests.Services;

public class OrderServiceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public OrderServiceTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Should_CalculateTotal_WithOnlySandwich()
    {
        var order = new Order
        {
            Sandwich = SandwichType.XBurguer,
            Extras = new List<ExtrasType>()
        };

        var total = OrderService.CalculateTotal(order);
        total.Should().Be(5.00m);
    }

    [Fact]
    public void Should_Apply_20Percent_Discount_WithFriesAndSoda()
    {
        var order = new Order
        {
            Sandwich = SandwichType.XEgg,
            Extras = new List<ExtrasType> { ExtrasType.Fries, ExtrasType.SoftDrink }
        };

        var total = OrderService.CalculateTotal(order);
        // 4.5 + 2 + 2.5 = 9.00 => 20% discount = 7.20
        total.Should().Be(7.20m);
    }

    [Fact]
    public void Should_Apply_15Percent_Discount_WithSodaOnly()
    {
        var order = new Order
        {
            Sandwich = SandwichType.XBacon,
            Extras = new List<ExtrasType> { ExtrasType.SoftDrink }
        };

        var total = OrderService.CalculateTotal(order);
        // 7 + 2.5 = 9.5 * 0.85 = 8.075
        total.Should().BeApproximately(8.08m, 0.01m);
    }

    [Fact]
    public void Should_Apply_10Percent_Discount_WithFriesOnly()
    {
        var order = new Order
        {
            Sandwich = SandwichType.XBurguer,
            Extras = new List<ExtrasType> { ExtrasType.Fries }
        };

        var total = OrderService.CalculateTotal(order);
        // 5 + 2 = 7 * 0.90 = 6.30
        total.Should().BeApproximately(6.30m, 0.01m);
    }

    [Fact]
    public void Should_Throw_DuplicateExtrasException_WhenExtrasAreDuplicated()
    {
        var order = new Order
        {
            Sandwich = SandwichType.XEgg,
            Extras = new List<ExtrasType> { ExtrasType.Fries, ExtrasType.Fries }
        };

        Action act = () => OrderService.CalculateTotal(order);
        act.Should().Throw<DuplicateExtrasException>()
           .WithMessage("*Fries*");
    }

    [Fact]
    public void Should_Throw_OrderNullException_WhenOrderIsNull()
    {
        Action act = () => OrderService.CalculateTotal(null!);
        act.Should().Throw<OrderNullException>();
    }

    [Fact]
    public void Should_Throw_InvalidSandwichException_WhenSandwichIsInvalid()
    {
        var order = new Order
        {
            Sandwich = (SandwichType)999,
            Extras = []
        };

        Action act = () => OrderService.CalculateTotal(order);
        act.Should().Throw<InvalidSandwichException>();
    }
}