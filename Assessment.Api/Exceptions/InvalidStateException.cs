namespace Assessment.Api.Exceptions;

/// <summary>
/// Thrown when a request specifies a state that is not supported or when the carrier
/// determines the business type is not eligible in the given state.
/// Supported states: TX (Texas), OH (Ohio), FL (Florida).
/// </summary>
public class InvalidStateException : Exception
{
    public string State { get; }

    public InvalidStateException(string state)
        : base($"State '{state}' is not supported or not eligible for the requested business type.")
    {
        State = state;
    }
}
