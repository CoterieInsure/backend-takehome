using Assessment.Api.Features.BusinessFactors.Services;
using Assessment.Api.Features.CarrierEligibility.Models;
using Assessment.Api.Features.CarrierEligibility.Validators;
using Assessment.Api.Features.StateFactors.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Assessment.UnitTests.Features.CarrierEligibility.Validators;

[TestFixture]
public class CreateCarrierEligibilityValidatorTests
{
    private Mock<IBusinessFactorService> _mockBusinessFactorService;
    private Mock<IStateFactorService> _mockStateFactorService;
    private CreateCarrierEligibilityValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _mockBusinessFactorService = new Mock<IBusinessFactorService>();
        _mockStateFactorService = new Mock<IStateFactorService>();
        _validator = new CreateCarrierEligibilityValidator(
            _mockBusinessFactorService.Object,
            _mockStateFactorService.Object);
    }

    [Test]
    public async Task Validate_WhenValid_ShouldPass()
    {
        // Arrange
        var request = new CreateCarrierEligibilityRequest
        {
            Business = "Tech",
            State = "CA",
            IsEligible = true
        };

        _mockBusinessFactorService.Setup(s => s.ExistsAsync("Tech")).ReturnsAsync(true);
        _mockStateFactorService.Setup(s => s.ExistsAsync("CA")).ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Test]
    public async Task Validate_WhenBusinessIsEmpty_ShouldFail()
    {
        // Arrange
        var request = new CreateCarrierEligibilityRequest
        {
            Business = "",
            State = "CA",
            IsEligible = true
        };

        _mockStateFactorService.Setup(s => s.ExistsAsync("CA")).ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Business" && e.ErrorMessage == "Business is required");
    }

    [Test]
    public async Task Validate_WhenBusinessExceedsMaxLength_ShouldFail()
    {
        // Arrange
        var request = new CreateCarrierEligibilityRequest
        {
            Business = new string('A', 101),
            State = "CA",
            IsEligible = true
        };

        _mockStateFactorService.Setup(s => s.ExistsAsync("CA")).ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Business" && e.ErrorMessage == "Business name cannot exceed 100 characters");
    }

    [Test]
    public async Task Validate_WhenBusinessDoesNotExist_ShouldFail()
    {
        // Arrange
        var request = new CreateCarrierEligibilityRequest
        {
            Business = "NonExistent",
            State = "CA",
            IsEligible = true
        };

        _mockBusinessFactorService.Setup(s => s.ExistsAsync("NonExistent")).ReturnsAsync(false);
        _mockStateFactorService.Setup(s => s.ExistsAsync("CA")).ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Business" && e.ErrorMessage == "Business 'NonExistent' does not exist");
    }

    [Test]
    public async Task Validate_WhenStateIsEmpty_ShouldFail()
    {
        // Arrange
        var request = new CreateCarrierEligibilityRequest
        {
            Business = "Tech",
            State = "",
            IsEligible = true
        };

        _mockBusinessFactorService.Setup(s => s.ExistsAsync("Tech")).ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "State" && e.ErrorMessage == "State is required");
    }

    [Test]
    public async Task Validate_WhenStateExceedsMaxLength_ShouldFail()
    {
        // Arrange
        var request = new CreateCarrierEligibilityRequest
        {
            Business = "Tech",
            State = new string('A', 51),
            IsEligible = true
        };

        _mockBusinessFactorService.Setup(s => s.ExistsAsync("Tech")).ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "State" && e.ErrorMessage == "State name cannot exceed 50 characters");
    }

    [Test]
    public async Task Validate_WhenStateDoesNotExist_ShouldFail()
    {
        // Arrange
        var request = new CreateCarrierEligibilityRequest
        {
            Business = "Tech",
            State = "NonExistent",
            IsEligible = true
        };

        _mockBusinessFactorService.Setup(s => s.ExistsAsync("Tech")).ReturnsAsync(true);
        _mockStateFactorService.Setup(s => s.ExistsAsync("NonExistent")).ReturnsAsync(false);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "State" && e.ErrorMessage == "State 'NonExistent' does not exist");
    }
}

[TestFixture]
public class UpdateCarrierEligibilityValidatorTests
{
    private Mock<IBusinessFactorService> _mockBusinessFactorService;
    private Mock<IStateFactorService> _mockStateFactorService;
    private UpdateCarrierEligibilityValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _mockBusinessFactorService = new Mock<IBusinessFactorService>();
        _mockStateFactorService = new Mock<IStateFactorService>();
        _validator = new UpdateCarrierEligibilityValidator(
            _mockBusinessFactorService.Object,
            _mockStateFactorService.Object);
    }

    [Test]
    public async Task Validate_WhenValid_ShouldPass()
    {
        // Arrange
        var request = new UpdateCarrierEligibilityRequest
        {
            Business = "Tech",
            State = "CA",
            IsEligible = true
        };

        _mockBusinessFactorService.Setup(s => s.ExistsAsync("Tech")).ReturnsAsync(true);
        _mockStateFactorService.Setup(s => s.ExistsAsync("CA")).ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Test]
    public async Task Validate_WhenBusinessIsEmpty_ShouldFail()
    {
        // Arrange
        var request = new UpdateCarrierEligibilityRequest
        {
            Business = "",
            State = "CA",
            IsEligible = true
        };

        _mockStateFactorService.Setup(s => s.ExistsAsync("CA")).ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Business" && e.ErrorMessage == "Business is required");
    }

    [Test]
    public async Task Validate_WhenBusinessDoesNotExist_ShouldFail()
    {
        // Arrange
        var request = new UpdateCarrierEligibilityRequest
        {
            Business = "NonExistent",
            State = "CA",
            IsEligible = true
        };

        _mockBusinessFactorService.Setup(s => s.ExistsAsync("NonExistent")).ReturnsAsync(false);
        _mockStateFactorService.Setup(s => s.ExistsAsync("CA")).ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Business" && e.ErrorMessage == "Business 'NonExistent' does not exist");
    }

    [Test]
    public async Task Validate_WhenStateDoesNotExist_ShouldFail()
    {
        // Arrange
        var request = new UpdateCarrierEligibilityRequest
        {
            Business = "Tech",
            State = "NonExistent",
            IsEligible = true
        };

        _mockBusinessFactorService.Setup(s => s.ExistsAsync("Tech")).ReturnsAsync(true);
        _mockStateFactorService.Setup(s => s.ExistsAsync("NonExistent")).ReturnsAsync(false);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "State" && e.ErrorMessage == "State 'NonExistent' does not exist");
    }
}
