using Assessment.Api.Features.CarrierEligibility.Services;
using Assessment.UnitTests.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace Assessment.UnitTests.Features.CarrierEligibility.Services;

[TestFixture]
public class CarrierEligibilityServiceTests : DatabaseTestFixture
{
    private CarrierEligibilityService _service;

    [SetUp]
    public void SetUp()
    {
        _service = new CarrierEligibilityService(DbFactory);
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
    public async Task GetAllAsync_WhenHasData_ShouldReturnAllEligibilities()
    {
        // Arrange
        await _service.CreateAsync(new Api.Features.CarrierEligibility.Models.CarrierEligibility
        {
            Business = "Tech",
            State = "CA",
            IsEligible = true
        });
        await _service.CreateAsync(new Api.Features.CarrierEligibility.Models.CarrierEligibility
        {
            Business = "Healthcare",
            State = "TX",
            IsEligible = false
        });

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(e => e.Business == "Tech" && e.State == "CA" && e.IsEligible);
        result.Should().Contain(e => e.Business == "Healthcare" && e.State == "TX" && !e.IsEligible);
    }

    [Test]
    public async Task GetByIdAsync_WhenExists_ShouldReturnEligibility()
    {
        // Arrange
        var created = await _service.CreateAsync(new Api.Features.CarrierEligibility.Models.CarrierEligibility
        {
            Business = "Tech",
            State = "CA",
            IsEligible = true
        });

        // Act
        var result = await _service.GetByIdAsync(created.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(created.Id);
        result.Business.Should().Be("Tech");
        result.State.Should().Be("CA");
        result.IsEligible.Should().BeTrue();
    }

    [Test]
    public async Task GetByIdAsync_WhenNotExists_ShouldReturnNull()
    {
        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public async Task GetByBusinessAndStateAsync_WhenExists_ShouldReturnEligibility()
    {
        // Arrange
        await _service.CreateAsync(new Api.Features.CarrierEligibility.Models.CarrierEligibility
        {
            Business = "Tech",
            State = "CA",
            IsEligible = true
        });

        // Act
        var result = await _service.GetByBusinessAndStateAsync("Tech", "CA");

        // Assert
        result.Should().NotBeNull();
        result!.Business.Should().Be("Tech");
        result.State.Should().Be("CA");
        result.IsEligible.Should().BeTrue();
    }

    [Test]
    public async Task GetByBusinessAndStateAsync_WhenNotExists_ShouldReturnNull()
    {
        // Act
        var result = await _service.GetByBusinessAndStateAsync("NonExistent", "CA");

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public async Task CreateAsync_ShouldCreateAndReturnEligibilityWithId()
    {
        // Arrange
        var eligibility = new Api.Features.CarrierEligibility.Models.CarrierEligibility
        {
            Business = "Tech",
            State = "CA",
            IsEligible = true
        };

        // Act
        var result = await _service.CreateAsync(eligibility);

        // Assert
        result.Id.Should().BeGreaterThan(0);
        result.Business.Should().Be("Tech");
        result.State.Should().Be("CA");
        result.IsEligible.Should().BeTrue();

        var retrieved = await _service.GetByIdAsync(result.Id);
        retrieved.Should().NotBeNull();
        retrieved.Should().BeEquivalentTo(result);
    }

    [Test]
    public async Task UpdateAsync_WhenIdMatches_ShouldUpdateAndReturnTrue()
    {
        // Arrange
        var created = await _service.CreateAsync(new Api.Features.CarrierEligibility.Models.CarrierEligibility
        {
            Business = "Tech",
            State = "CA",
            IsEligible = true
        });

        var updated = new Api.Features.CarrierEligibility.Models.CarrierEligibility
        {
            Id = created.Id,
            Business = "Healthcare",
            State = "TX",
            IsEligible = false
        };

        // Act
        var result = await _service.UpdateAsync(created.Id, updated);

        // Assert
        result.Should().BeTrue();

        var retrieved = await _service.GetByIdAsync(created.Id);
        retrieved!.Business.Should().Be("Healthcare");
        retrieved.State.Should().Be("TX");
        retrieved.IsEligible.Should().BeFalse();
    }

    [Test]
    public async Task UpdateAsync_WhenIdDoesNotMatch_ShouldReturnFalse()
    {
        // Arrange
        var eligibility = new Api.Features.CarrierEligibility.Models.CarrierEligibility
        {
            Id = 1,
            Business = "Tech",
            State = "CA",
            IsEligible = true
        };

        // Act
        var result = await _service.UpdateAsync(2, eligibility);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task UpdateAsync_WhenNotExists_ShouldReturnFalse()
    {
        // Arrange
        var eligibility = new Api.Features.CarrierEligibility.Models.CarrierEligibility
        {
            Id = 999,
            Business = "Tech",
            State = "CA",
            IsEligible = true
        };

        // Act
        var result = await _service.UpdateAsync(999, eligibility);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task DeleteAsync_WhenExists_ShouldDeleteAndReturnTrue()
    {
        // Arrange
        var created = await _service.CreateAsync(new Api.Features.CarrierEligibility.Models.CarrierEligibility
        {
            Business = "Tech",
            State = "CA",
            IsEligible = true
        });

        // Act
        var result = await _service.DeleteAsync(created.Id);

        // Assert
        result.Should().BeTrue();

        var retrieved = await _service.GetByIdAsync(created.Id);
        retrieved.Should().BeNull();
    }

    [Test]
    public async Task DeleteAsync_WhenNotExists_ShouldReturnFalse()
    {
        // Act
        var result = await _service.DeleteAsync(999);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task ExistsAsync_WhenExists_ShouldReturnTrue()
    {
        // Arrange
        var created = await _service.CreateAsync(new Api.Features.CarrierEligibility.Models.CarrierEligibility
        {
            Business = "Tech",
            State = "CA",
            IsEligible = true
        });

        // Act
        var result = await _service.ExistsAsync(created.Id);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public async Task ExistsAsync_WhenNotExists_ShouldReturnFalse()
    {
        // Act
        var result = await _service.ExistsAsync(999);

        // Assert
        result.Should().BeFalse();
    }
}
