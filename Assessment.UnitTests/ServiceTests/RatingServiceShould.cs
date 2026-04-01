using Assessment.UnitTests.Fakers;
using NUnit.Framework;

namespace Assessment.UnitTests.ServiceTests;

/// <summary>
/// Sample test class showing the expected testing patterns.
/// TODO: Implement your RatingService, then write tests covering:
///   - Happy path: valid request returns correct premiums for each state
///   - Each business type calculates differently
///   - State name to abbreviation conversion works
///   - Invalid business type throws appropriate exception
///   - Invalid state throws appropriate exception
///   - Empty states array is rejected
///   - Negative revenue is rejected
///   - Endorsement pro-rata calculation is correct
///
/// Use the RatingRequestFaker for generating test data.
/// Use Moq to mock the ICarrierApiClient dependency.
/// </summary>
[TestFixture]
public class RatingServiceShould
{
    private RatingRequestFaker _ratingRequestFaker = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        _ratingRequestFaker = new RatingRequestFaker();

        // TODO: Set up your service under test here.
        // Example:
        //   var mockCarrierClient = new Mock<ICarrierApiClient>();
        //   mockCarrierClient.Setup(c => c.ValidateEligibilityAsync(It.IsAny<string>(), It.IsAny<string>()))
        //       .ReturnsAsync(true);
        //   _ratingService = new RatingService(configuration, mockCarrierClient.Object);
    }

    [Test]
    public void BeImplemented()
    {
        // This test exists so the project compiles and tests run out of the box.
        // Replace it with real tests once you implement your RatingService.
        Assert.Pass("Replace this with your actual tests");
    }

    // TODO: Add your test methods here. Example structure:
    //
    // [Test]
    // public async Task ReturnCorrectPremiumForPlumberInTexas()
    // {
    //     // Arrange
    //     var request = new RatingRequest { Business = "Plumber", Revenue = 6000000, States = ["TX"] };
    //
    //     // Act
    //     var result = await _ratingService.CalculatePremiumAsync(request);
    //
    //     // Assert
    //     Assert.That(result.Premiums, Has.Count.EqualTo(1));
    //     Assert.That(result.Premiums[0].Premium, Is.EqualTo(11316));
    //     Assert.That(result.Premiums[0].State, Is.EqualTo("TX"));
    // }
}
