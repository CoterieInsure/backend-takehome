using Assessment.Api.Features.StateFactors.Controllers;
using Assessment.Api.Features.StateFactors.Models;
using Assessment.Api.Features.StateFactors.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Assessment.UnitTests.Features.StateFactors.Controllers;

[TestFixture]
public class AdminStateFactorsControllerTests
{
    private Mock<IStateFactorService> _mockService;
    private AdminStateFactorsController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockService = new Mock<IStateFactorService>();
        _controller = new AdminStateFactorsController(_mockService.Object);
    }

    [Test]
    public async Task GetAll_ShouldReturnOkWithAllFactors()
    {
        // Arrange
        var factors = new List<StateFactor>
        {
            new() { State = "CA", Factor = 1.2m },
            new() { State = "TX", Factor = 1.5m }
        };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(factors);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var response = okResult.Value.Should().BeAssignableTo<IEnumerable<StateFactorResponse>>().Subject;
        response.Should().HaveCount(2);
        response.Should().Contain(r => r.State == "CA" && r.Factor == 1.2m);
        response.Should().Contain(r => r.State == "TX" && r.Factor == 1.5m);
    }

    [Test]
    public async Task Get_WhenExists_ShouldReturnFactor()
    {
        // Arrange
        var factor = new StateFactor { State = "CA", Factor = 1.2m };
        _mockService.Setup(s => s.GetByStateAsync("CA")).ReturnsAsync(factor);

        // Act
        var result = await _controller.Get("CA");

        // Assert
        var response = result.Value.Should().BeOfType<StateFactorResponse>().Subject;
        response.State.Should().Be("CA");
        response.Factor.Should().Be(1.2m);
    }

    [Test]
    public async Task Get_WhenNotExists_ShouldReturnNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.GetByStateAsync("NonExistent")).ReturnsAsync((StateFactor?)null);

        // Act
        var result = await _controller.Get("NonExistent");

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task Create_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var request = new CreateStateFactorRequest { State = "CA", Factor = 1.2m };
        var created = new StateFactor { State = "CA", Factor = 1.2m };
        _mockService.Setup(s => s.CreateAsync(It.IsAny<StateFactor>())).ReturnsAsync(created);

        // Act
        var result = await _controller.Create(request);

        // Assert
        var createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.ActionName.Should().Be(nameof(_controller.Get));
        createdResult.RouteValues!["state"].Should().Be("CA");

        var response = createdResult.Value.Should().BeOfType<StateFactorResponse>().Subject;
        response.State.Should().Be("CA");
        response.Factor.Should().Be(1.2m);
    }

    [Test]
    public async Task Update_WhenSuccessful_ShouldReturnNoContent()
    {
        // Arrange
        var request = new UpdateStateFactorRequest { Factor = 1.5m };
        _mockService.Setup(s => s.UpdateAsync("CA", It.IsAny<StateFactor>())).ReturnsAsync(true);

        // Act
        var result = await _controller.Update("CA", request);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Test]
    public async Task Update_WhenNotFound_ShouldReturnNotFound()
    {
        // Arrange
        var request = new UpdateStateFactorRequest { Factor = 1.5m };
        _mockService.Setup(s => s.UpdateAsync("NonExistent", It.IsAny<StateFactor>())).ReturnsAsync(false);

        // Act
        var result = await _controller.Update("NonExistent", request);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task Delete_WhenSuccessful_ShouldReturnNoContent()
    {
        // Arrange
        _mockService.Setup(s => s.DeleteAsync("CA")).ReturnsAsync(true);

        // Act
        var result = await _controller.Delete("CA");

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Test]
    public async Task Delete_WhenNotFound_ShouldReturnNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.DeleteAsync("NonExistent")).ReturnsAsync(false);

        // Act
        var result = await _controller.Delete("NonExistent");

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}
