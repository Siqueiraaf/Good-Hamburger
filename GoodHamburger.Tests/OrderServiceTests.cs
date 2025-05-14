using FluentAssertions;
using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Services;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Enums.Sandwich;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using GoodHamburger.WebAPI;
using Moq;
using System;
using System.Collections.Generic;
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
        var order = new OrderDto
        {
            Sandwich = SandwichType.XBurguer,
            Extras = new List<ExtrasType>()
        };

        var mockRepo = new Mock<IOrderRepository>();
        var service = new OrderService(mockRepo.Object);
        var total = service.CalculateTotal(order);
        total.Should().Be(5.00m);
    }

    [Fact]
    public void Should_Apply_20Percent_Discount_WithFriesAndSoda()
    {
        var order = new OrderDto
        {
            Sandwich = SandwichType.XEgg,
            Extras = new List<ExtrasType> { ExtrasType.Fries, ExtrasType.SoftDrink }
        };

        var mockRepo = new Mock<IOrderRepository>();
        var service = new OrderService(mockRepo.Object);
        var total = service.CalculateTotal(order);
        total.Should().Be(7.20m);
    }

    [Fact]
    public void Should_Apply_15Percent_Discount_WithSodaOnly()
    {
        var order = new OrderDto
        {
            Sandwich = SandwichType.XBacon,
            Extras = new List<ExtrasType> { ExtrasType.SoftDrink }
        };

        var mockRepo = new Mock<IOrderRepository>();
        var service = new OrderService(mockRepo.Object);
        var total = service.CalculateTotal(order);
        total.Should().BeApproximately(8.08m, 0.01m);
    }

    [Fact]
    public void Should_Apply_10Percent_Discount_WithFriesOnly()
    {
        var order = new OrderDto
        {
            Sandwich = SandwichType.XBurguer,
            Extras = new List<ExtrasType> { ExtrasType.Fries }
        };

        var mockRepo = new Mock<IOrderRepository>();
        var service = new OrderService(mockRepo.Object);
        var total = service.CalculateTotal(order);
        total.Should().BeApproximately(6.30m, 0.01m);
    }

    [Fact]
    public void Should_Throw_DuplicateExtrasException_WhenExtrasAreDuplicated()
    {
        var order = new OrderDto
        {
            Sandwich = SandwichType.XEgg,
            Extras = new List<ExtrasType> { ExtrasType.Fries, ExtrasType.Fries }
        };

        var mockRepo = new Mock<IOrderRepository>();
        var service = new OrderService(mockRepo.Object);
        Action act = () => service.CalculateTotal(order);
        act.Should().Throw<DuplicateExtrasException>()
           .WithMessage("*Fries*");
    }

    [Fact]
    public void Should_Throw_OrderNullException_WhenOrderIsNull()
    {
        var mockRepo = new Mock<IOrderRepository>();
        var service = new OrderService(mockRepo.Object);
        Action act = () => service.CalculateTotal(null!);
        act.Should().Throw<OrderNullException>();
    }

    [Fact]
    public void Should_Throw_InvalidSandwichException_WhenSandwichIsInvalid()
    {
        var order = new OrderDto
        {
            Sandwich = (SandwichType)999,
            Extras = new List<ExtrasType>()
        };

        var mockRepo = new Mock<IOrderRepository>();
        var service = new OrderService(mockRepo.Object);
        Action act = () => service.CalculateTotal(order);
        act.Should().Throw<InvalidSandwichException>();
    }
}
