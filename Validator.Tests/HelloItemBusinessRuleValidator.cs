using System.Threading;
using System.Threading.Tasks;
using Validator.Abstractions;

namespace Validator.Tests;

public class HelloItemBusinessRuleValidator : IBusinessRuleValidator<HelloItem>
{
	public Task<ValidationResult> ValidateAsync(HelloItem validatingType, CancellationToken token = default)
	{
		return Task.FromResult(new ValidationResult());
	}
}