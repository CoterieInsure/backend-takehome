using Assessment.Api.Features.StateFactors.Models;
using Assessment.Api.Features.StateFactors.Services;
using Assessment.UnitTests.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace Assessment.UnitTests.Features.StateFactors.Services;

[TestFixture]
public class StateFactorServiceTests : DatabaseTestFixture
{
    private StateFactorService _service;

    [SetUp]
    public void SetUp()
    {
        _service = new StateFactorService(DbFactory);
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
    public async Task GetAllAsync_WhenHasData_ShouldReturnAllStateFactors()
    {
        // Arrange
        await _service.CreateAsync(new StateFactor { State = "CA", Factor = 1.2m });
        await _service.CreateAsync(new StateFactor { State = "TX", Factor = 1.5m });

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(f => f.State == "CA" && f.Factor == 1.2m);
        result.Should().Contain(f => f.State == "TX" && f.Factor == 1.5m);
    }

    [Test]
    public async Task GetByStateAsync_WhenExists_ShouldReturnStateFactor()
    {
        // Arrange
        await _service.CreateAsync(new StateFactor { State = "CA", Factor = 1.2m });

        // Act
        var result = await _service.GetByStateAsync("CA");

        // Assert
        result.Should().NotBeNull();
        result!.State.Should().Be("CA");
        result.Factor.Should().Be(1.2m);
    }

    [Test]
    public async Task GetByStateAsync_WhenNotExists_ShouldReturnNull()
    {
        // Act
        var result = await _service.GetByStateAsync("NonExistent");

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public async Task CreateAsync_ShouldCreateAndReturnStateFactor()
    {
        // Arrange
        var factor = new StateFactor { State = "CA", Factor = 1.2m };

        // Act
        var result = await _service.CreateAsync(factor);

        // Assert
        result.Should().BeEquivalentTo(factor);

        var retrieved = await _service.GetByStateAsync("CA");
        retrieved.Should().NotBeNull();
        retrieved!.Factor.Should().Be(1.2m);
    }

    [Test]
    public async Task UpdateAsync_WhenStateMatches_ShouldUpdateAndReturnTrue()
    {
        // Arrange
        await _service.CreateAsync(new StateFactor { State = "CA", Factor = 1.2m });
        var updatedFactor = new StateFactor { State = "CA", Factor = 1.5m };

        // Act
        var result = await _service.UpdateAsync("CA", updatedFactor);

        // Assert
        result.Should().BeTrue();

        var retrieved = await _service.GetByStateAsync("CA");
        retrieved!.Factor.Should().Be(1.5m);
    }

    [Test]
    public async Task UpdateAsync_WhenStateDoesNotMatch_ShouldReturnFalse()
    {
        // Arrange
        var factor = new StateFactor { State = "CA", Factor = 1.5m };

        // Act
        var result = await _service.UpdateAsync("TX", factor);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task UpdateAsync_WhenNotExists_ShouldReturnFalse()
    {
        // Arrange
        var factor = new StateFactor { State = "CA", Factor = 1.5m };

        // Act
        var result = await _service.UpdateAsync("CA", factor);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task DeleteAsync_WhenExists_ShouldDeleteAndReturnTrue()
    {
        // Arrange
        await _service.CreateAsync(new StateFactor { State = "CA", Factor = 1.2m });

        // Act
        var result = await _service.DeleteAsync("CA");

        // Assert
        result.Should().BeTrue();

        var retrieved = await _service.GetByStateAsync("CA");
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
        await _service.CreateAsync(new StateFactor { State = "CA", Factor = 1.2m });

        // Act
        var result = await _service.ExistsAsync("CA");

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
