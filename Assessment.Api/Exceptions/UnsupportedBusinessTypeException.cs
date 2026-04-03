namespace Assessment.Api.Exceptions;

/// <summary>
/// Thrown when a request specifies a business type that is not supported by the rating engine.
/// Supported types: Plumber, Architect, Programmer.
/// </summary>
public class UnsupportedBusinessTypeException : Exception
{
    public string BusinessType { get; }

    public UnsupportedBusinessTypeException(string businessType)
        : base($"Business type '{businessType}' is not supported.")
    {
        BusinessType = businessType;
    }
}
