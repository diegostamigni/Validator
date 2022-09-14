namespace Validator.Abstractions;

public interface IValidator<in TType>
{
	Task<ValidationResult> ValidateAsync(TType validatingType, CancellationToken token = default);
}