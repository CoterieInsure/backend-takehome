using Assessment.Api.Features.StateFactors.Models;
using Assessment.Api.Features.StateFactors.Validators;
using FluentAssertions;
using NUnit.Framework;

namespace Assessment.UnitTests.Features.StateFactors.Validators;

[TestFixture]
public class CreateStateFactorValidatorTests
{
    private CreateStateFactorValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new CreateStateFactorValidator();
    }

    [Test]
    public async Task Validate_WhenValid_ShouldPass()
    {
        // Arrange
        var request = new CreateStateFactorRequest
        {
            State = "CA",
            Factor = 1.2m
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Test]
    public async Task Validate_WhenStateIsEmpty_ShouldFail()
    {
        // Arrange
        var request = new CreateStateFactorRequest
        {
            State = "",
            Factor = 1.2m
        };

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
        var request = new CreateStateFactorRequest
        {
            State = new string('A', 51),
            Factor = 1.2m
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "State" && e.ErrorMessage == "State name cannot exceed 50 characters");
    }

    [Test]
    public async Task Validate_WhenFactorIsZero_ShouldFail()
    {
        // Arrange
        var request = new CreateStateFactorRequest
        {
            State = "CA",
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
        var request = new CreateStateFactorRequest
        {
            State = "CA",
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
        var request = new CreateStateFactorRequest
        {
            State = "CA",
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
public class UpdateStateFactorValidatorTests
{
    private UpdateStateFactorValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new UpdateStateFactorValidator();
    }

    [Test]
    public async Task Validate_WhenValid_ShouldPass()
    {
        // Arrange
        var request = new UpdateStateFactorRequest
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
        var request = new UpdateStateFactorRequest
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
        var request = new UpdateStateFactorRequest
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
        var request = new UpdateStateFactorRequest
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
