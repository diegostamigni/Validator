namespace Validator.Abstractions;

public record ValidationError
{
	public string? ErrorMessage { get; init; }

	public string? ErrorCode { get; init; }

	public string? PropertyName { get; init; }
}