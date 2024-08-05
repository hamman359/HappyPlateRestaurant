namespace HappyPlate.Domain.Shared;

/// <summary>
/// Represents the Validation result being returned by the Validation Pipeline Behavior
/// </summary>
public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(default, false, IValidationResult.ValidationError) =>
        Errors = errors;

    public Error[] Errors { get; }

    /// <summary>
    /// Allows for creating a validation result by specifying an array validation errors.
    /// </summary>
    public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
}