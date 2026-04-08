using Assessment.Api.Features.BusinessFactors.Models;
using Assessment.Api.Features.BusinessFactors.Services;
using Assessment.UnitTests.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace Assessment.UnitTests.Features.BusinessFactors.Services;

[TestFixture]
public class BusinessFactorServiceTests : DatabaseTestFixture
{
    private BusinessFactorService _service;

    [SetUp]
    public void SetUp()
    {
        _service = new BusinessFactorService(DbFactory);
    }

    [Test]
    public async Task GetAllAsync_WhenEmpty_ShouldReturnEmptyList()
    {
        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetAllAsync_WhenHasData_ShouldReturnAllBusinessFactors()
    {
        // Arrange
        await _service.CreateAsync(new BusinessFactor { Business = "Tech", Factor = 1.2m });
        await _service.CreateAsync(new BusinessFactor { Business = "Healthcare", Factor = 1.5m });

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(f => f.Business == "Tech" && f.Factor == 1.2m);
        result.Should().Contain(f => f.Business == "Healthcare" && f.Factor == 1.5m);
    }

    [Test]
    public async Task GetByBusinessAsync_WhenExists_ShouldReturnBusinessFactor()
    {
        // Arrange
        var created = await _service.CreateAsync(new BusinessFactor { Business = "Tech", Factor = 1.2m });

        // Act
        var result = await _service.GetByBusinessAsync("Tech");

        // Assert
        result.Should().NotBeNull();
        result!.Business.Should().Be("Tech");
        result.Factor.Should().Be(1.2m);
    }

    [Test]
    public async Task GetByBusinessAsync_WhenNotExists_ShouldReturnNull()
    {
        // Act
        var result = await _service.GetByBusinessAsync("NonExistent");

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public async Task CreateAsync_ShouldCreateAndReturnBusinessFactor()
    {
        // Arrange
        var factor = new BusinessFactor { Business = "Tech", Factor = 1.2m };

        // Act
        var result = await _service.CreateAsync(factor);

        // Assert
        result.Should().BeEquivalentTo(factor);

        var retrieved = await _service.GetByBusinessAsync("Tech");
        retrieved.Should().NotBeNull();
        retrieved!.Factor.Should().Be(1.2m);
    }

    [Test]
    public async Task UpdateAsync_WhenBusinessMatches_ShouldUpdateAndReturnTrue()
    {
        // Arrange
        await _service.CreateAsync(new BusinessFactor { Business = "Tech", Factor = 1.2m });
        var updatedFactor = new BusinessFactor { Business = "Tech", Factor = 1.5m };

        // Act
        var result = await _service.UpdateAsync("Tech", updatedFactor);

        // Assert
        result.Should().BeTrue();

        var retrieved = await _service.GetByBusinessAsync("Tech");
        retrieved!.Factor.Should().Be(1.5m);
    }

    [Test]
    public async Task UpdateAsync_WhenBusinessDoesNotMatch_ShouldReturnFalse()
    {
        // Arrange
        var factor = new BusinessFactor { Business = "Tech", Factor = 1.5m };

        // Act
        var result = await _service.UpdateAsync("Healthcare", factor);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task UpdateAsync_WhenNotExists_ShouldReturnFalse()
    {
        // Arrange
        var factor = new BusinessFactor { Business = "Tech", Factor = 1.5m };

        // Act
        var result = await _service.UpdateAsync("Tech", factor);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task DeleteAsync_WhenExists_ShouldDeleteAndReturnTrue()
    {
        // Arrange
        await _service.CreateAsync(new BusinessFactor { Business = "Tech", Factor = 1.2m });

        // Act
        var result = await _service.DeleteAsync("Tech");

        // Assert
        result.Should().BeTrue();

        var retrieved = await _service.GetByBusinessAsync("Tech");
        retrieved.Should().BeNull();
    }

    [Test]
    public async Task DeleteAsync_WhenNotExists_ShouldReturnFalse()
    {
        // Act
        var result = await _service.DeleteAsync("NonExistent");

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task ExistsAsync_WhenExists_ShouldReturnTrue()
    {
        // Arrange
        await _service.CreateAsync(new BusinessFactor { Business = "Tech", Factor = 1.2m });

        // Act
        var result = await _service.ExistsAsync("Tech");

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public async Task ExistsAsync_WhenNotExists_ShouldReturnFalse()
    {
        // Act
        var result = await _service.ExistsAsync("NonExistent");

        // Assert
        result.Should().BeFalse();
    }
}
