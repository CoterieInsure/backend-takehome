using Assessment.Api.Features.BusinessFactors.Models;
using Assessment.Api.Features.BusinessFactors.Validators;
using FluentAssertions;
using NUnit.Framework;

namespace Assessment.UnitTests.Features.BusinessFactors.Validators;

[TestFixture]
public class CreateBusinessFactorValidatorTests
{
    private CreateBusinessFactorValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new CreateBusinessFactorValidator();
    }

    [Test]
    public async Task Validate_WhenValid_ShouldPass()
    {
        // Arrange
        var request = new CreateBusinessFactorRequest
        {
            Business = "Tech",
            Factor = 1.2m
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Test]
    public async Task Validate_WhenBusinessIsEmpty_ShouldFail()
    {
        // Arrange
        var request = new CreateBusinessFactorRequest
        {
            Business = "",
            Factor = 1.2m
        };

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
        var request = new CreateBusinessFactorRequest
        {
            Business = new string('A', 101),
            Factor = 1.2m
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Business" && e.ErrorMessage == "Business name cannot exceed 100 characters");
    }

    [Test]
    public async Task Validate_WhenFactorIsZero_ShouldFail()
    {
        // Arrange
        var request = new CreateBusinessFactorRequest
        {
            Business = "Tech",
            Factor = 0
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Factor" && e.ErrorMessage == "Factor must be greater than 0");
    }

    [Test]
    public async Task Validate_WhenFactorIsNegative_ShouldFail()
    {
        // Arrange
        var request = new CreateBusinessFactorRequest
        {
            Business = "Tech",
            Factor = -1
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Factor" && e.ErrorMessage == "Factor must be greater than 0");
    }

    [Test]
    public async Task Validate_WhenFactorExceedsMaximum_ShouldFail()
    {
        // Arrange
        var request = new CreateBusinessFactorRequest
        {
            Business = "Tech",
            Factor = 101
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Factor" && e.ErrorMessage == "Factor cannot exceed 100");
    }
}

[TestFixture]
public class UpdateBusinessFactorValidatorTests
{
    private UpdateBusinessFactorValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new UpdateBusinessFactorValidator();
    }

    [Test]
    public async Task Validate_WhenValid_ShouldPass()
    {
        // Arrange
        var request = new UpdateBusinessFactorRequest
        {
            Factor = 1.2m
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Test]
    public async Task Validate_WhenFactorIsZero_ShouldFail()
    {
        // Arrange
        var request = new UpdateBusinessFactorRequest
        {
            Factor = 0
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Factor" && e.ErrorMessage == "Factor must be greater than 0");
    }

    [Test]
    public async Task Validate_WhenFactorIsNegative_ShouldFail()
    {
        // Arrange
        var request = new UpdateBusinessFactorRequest
        {
            Factor = -1
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Factor" && e.ErrorMessage == "Factor must be greater than 0");
    }

    [Test]
    public async Task Validate_WhenFactorExceedsMaximum_ShouldFail()
    {
        // Arrange
        var request = new UpdateBusinessFactorRequest
        {
            Factor = 101
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Factor" && e.ErrorMessage == "Factor cannot exceed 100");
    }
}
