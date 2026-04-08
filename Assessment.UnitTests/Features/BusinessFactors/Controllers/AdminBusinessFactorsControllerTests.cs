using Assessment.Api.Features.BusinessFactors.Controllers;
using Assessment.Api.Features.BusinessFactors.Models;
using Assessment.Api.Features.BusinessFactors.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Assessment.UnitTests.Features.BusinessFactors.Controllers;

[TestFixture]
public class AdminBusinessFactorsControllerTests
{
    private Mock<IBusinessFactorService> _mockService;
    private AdminBusinessFactorsController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockService = new Mock<IBusinessFactorService>();
        _controller = new AdminBusinessFactorsController(_mockService.Object);
    }

    [Test]
    public async Task GetAll_ShouldReturnOkWithAllFactors()
    {
        // Arrange
        var factors = new List<BusinessFactor>
        {
            new() { Business = "Tech", Factor = 1.2m },
            new() { Business = "Healthcare", Factor = 1.5m }
        };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(factors);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var response = okResult.Value.Should().BeAssignableTo<IEnumerable<BusinessFactorResponse>>().Subject;
        response.Should().HaveCount(2);
        response.Should().Contain(r => r.Business == "Tech" && r.Factor == 1.2m);
        response.Should().Contain(r => r.Business == "Healthcare" && r.Factor == 1.5m);
    }

    [Test]
    public async Task Get_WhenExists_ShouldReturnFactor()
    {
        // Arrange
        var factor = new BusinessFactor { Business = "Tech", Factor = 1.2m };
        _mockService.Setup(s => s.GetByBusinessAsync("Tech")).ReturnsAsync(factor);

        // Act
        var result = await _controller.Get("Tech");

        // Assert
        var response = result.Value.Should().BeOfType<BusinessFactorResponse>().Subject;
        response.Business.Should().Be("Tech");
        response.Factor.Should().Be(1.2m);
    }

    [Test]
    public async Task Get_WhenNotExists_ShouldReturnNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.GetByBusinessAsync("NonExistent")).ReturnsAsync((BusinessFactor?)null);

        // Act
        var result = await _controller.Get("NonExistent");

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task Create_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var request = new CreateBusinessFactorRequest { Business = "Tech", Factor = 1.2m };
        var created = new BusinessFactor { Business = "Tech", Factor = 1.2m };
        _mockService.Setup(s => s.CreateAsync(It.IsAny<BusinessFactor>())).ReturnsAsync(created);

        // Act
        var result = await _controller.Create(request);

        // Assert
        var createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.ActionName.Should().Be(nameof(_controller.Get));
        createdResult.RouteValues!["business"].Should().Be("Tech");

        var response = createdResult.Value.Should().BeOfType<BusinessFactorResponse>().Subject;
        response.Business.Should().Be("Tech");
        response.Factor.Should().Be(1.2m);
    }

    [Test]
    public async Task Update_WhenSuccessful_ShouldReturnNoContent()
    {
        // Arrange
        var request = new UpdateBusinessFactorRequest { Factor = 1.5m };
        _mockService.Setup(s => s.UpdateAsync("Tech", It.IsAny<BusinessFactor>())).ReturnsAsync(true);

        // Act
        var result = await _controller.Update("Tech", request);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Test]
    public async Task Update_WhenNotFound_ShouldReturnNotFound()
    {
        // Arrange
        var request = new UpdateBusinessFactorRequest { Factor = 1.5m };
        _mockService.Setup(s => s.UpdateAsync("NonExistent", It.IsAny<BusinessFactor>())).ReturnsAsync(false);

        // Act
        var result = await _controller.Update("NonExistent", request);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task Delete_WhenSuccessful_ShouldReturnNoContent()
    {
        // Arrange
        _mockService.Setup(s => s.DeleteAsync("Tech")).ReturnsAsync(true);

        // Act
        var result = await _controller.Delete("Tech");

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
