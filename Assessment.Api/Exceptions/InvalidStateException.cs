namespace Assessment.Api.Exceptions;

/// <summary>
/// Thrown when a request specifies a state that is not supported by the rating engine.
/// Supported states: TX (Texas), OH (Ohio), FL (Florida).
/// Note: Carrier ineligibility (e.g., Programmer in FL) is NOT an error — omit the state from the response instead.
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
