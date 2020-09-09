using System.Threading;
using System.Threading.Tasks;
using Validator.Core;
using Validator.Core.Abstractions;

namespace Validator.Samples
{
	public class HelloItemBusinessRuleValidator : IBusinessRuleValidator<HelloItem>
	{
		public Task<ValidationResult> ValidateAsync(HelloItem validatingType, CancellationToken token = default)
		{
			return Task.FromResult(new ValidationResult());
		}
	}
}