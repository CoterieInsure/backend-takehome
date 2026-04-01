using Assessment.Api.Models.Requests;
using Bogus;

namespace Assessment.UnitTests.Fakers;

/// <summary>
/// Sample Bogus faker for generating test RatingRequest data.
/// Use this pattern to create fakers for other request/response types.
///
/// Usage:
///   var faker = new RatingRequestFaker();
///   var request = faker.Generate();
///   var requests = faker.Generate(5);
///
/// For more info: https://github.com/bchavez/Bogus
/// </summary>
public class RatingRequestFaker : Faker<RatingRequest>
{
    private static readonly string[] ValidBusinessTypes = ["Plumber", "Architect", "Programmer"];
    private static readonly string[] ValidStates = ["TX", "OH", "FL"];

    public RatingRequestFaker()
    {
        RuleFor(r => r.Business, f => f.PickRandom(ValidBusinessTypes));
        RuleFor(r => r.Revenue, f => f.Finance.Amount(100_000, 10_000_000));
        RuleFor(r => r.States, f => f.PickRandom(ValidStates, f.Random.Int(1, 3)).ToArray());
    }
}
