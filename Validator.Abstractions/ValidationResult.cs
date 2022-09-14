namespace Validator.Abstractions;

public class ValidationResult
{
	public List<ValidationError> ValidationErrors { get; set; } = new();

	public bool IsValid => this.ValidationErrors.Count == 0;

	public void ThrowIfInvalid()
	{
		if (this.ValidationErrors.Any())
		{
			throw new ValidationException(this.ValidationErrors);
		}
	}
}