using Assessment.Api.Features.CarrierEligibility.Controllers;
using Assessment.Api.Features.CarrierEligibility.Models;
using Assessment.Api.Features.CarrierEligibility.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using CarrierEligibilityModel = Assessment.Api.Features.CarrierEligibility.Models.CarrierEligibility;

namespace Assessment.UnitTests.Features.CarrierEligibility.Controllers;

[TestFixture]
public class AdminEligibilityControllerTests
{
    private Mock<ICarrierEligibilityService> _mockService;
    private AdminEligibilityController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockService = new Mock<ICarrierEligibilityService>();
        _controller = new AdminEligibilityController(_mockService.Object);
    }

    [Test]
    public async Task GetAll_ShouldReturnOkWithAllEligibilities()
    {
        // Arrange
        var eligibilities = new List<CarrierEligibilityModel>
        {
            new() { Id = 1, Business = "Tech", State = "CA", IsEligible = true },
            new() { Id = 2, Business = "Healthcare", State = "TX", IsEligible = false }
        };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(eligibilities);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var response = okResult.Value.Should().BeAssignableTo<IEnumerable<CarrierEligibilityResponse>>().Subject;
        response.Should().HaveCount(2);
        response.Should().Contain(r => r.Id == 1 && r.Business == "Tech" && r.State == "CA" && r.IsEligible);
        response.Should().Contain(r => r.Id == 2 && r.Business == "Healthcare" && r.State == "TX" && !r.IsEligible);
    }

    [Test]
    public async Task Get_WhenExists_ShouldReturnEligibility()
    {
        // Arrange
        var eligibility = new CarrierEligibilityModel
        {
            Id = 1,
            Business = "Tech",
            State = "CA",
            IsEligible = true
        };
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(eligibility);

        // Act
        var result = await _controller.Get(1);

        // Assert
        var response = result.Value.Should().BeOfType<CarrierEligibilityResponse>().Subject;
        response.Id.Should().Be(1);
        response.Business.Should().Be("Tech");
        response.State.Should().Be("CA");
        response.IsEligible.Should().BeTrue();
    }

    [Test]
    public async Task Get_WhenNotExists_ShouldReturnNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((CarrierEligibilityModel?)null);

        // Act
        var result = await _controller.Get(999);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task Create_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var request = new CreateCarrierEligibilityRequest
        {
            Business = "Tech",
            State = "CA",
            IsEligible = true
        };
        var created = new CarrierEligibilityModel
        {
            Id = 1,
            Business = "Tech",
            State = "CA",
            IsEligible = true
        };
        _mockService.Setup(s => s.CreateAsync(It.IsAny<CarrierEligibilityModel>())).ReturnsAsync(created);

        // Act
        var result = await _controller.Create(request);

        // Assert
        var createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.ActionName.Should().Be(nameof(_controller.Get));
        createdResult.RouteValues!["id"].Should().Be(1);

        var response = createdResult.Value.Should().BeOfType<CarrierEligibilityResponse>().Subject;
        response.Id.Should().Be(1);
        response.Business.Should().Be("Tech");
        response.State.Should().Be("CA");
        response.IsEligible.Should().BeTrue();
    }

    [Test]
    public async Task Update_WhenSuccessful_ShouldReturnNoContent()
    {
        // Arrange
        var request = new UpdateCarrierEligibilityRequest
        {
            Business = "Tech",
            State = "CA",
            IsEligible = false
        };
        _mockService.Setup(s => s.UpdateAsync(1, It.IsAny<CarrierEligibilityModel>())).ReturnsAsync(true);

        // Act
        var result = await _controller.Update(1, request);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Test]
    public async Task Update_WhenNotFound_ShouldReturnNotFound()
    {
        // Arrange
        var request = new UpdateCarrierEligibilityRequest
        {
            Business = "Tech",
            State = "CA",
            IsEligible = false
        };
        _mockService.Setup(s => s.UpdateAsync(999, It.IsAny<CarrierEligibilityModel>())).ReturnsAsync(false);

        // Act
        var result = await _controller.Update(999, request);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task Delete_WhenSuccessful_ShouldReturnNoContent()
    {
        // Arrange
        _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Test]
    public async Task Delete_WhenNotFound_ShouldReturnNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.DeleteAsync(999)).ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(999);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}
