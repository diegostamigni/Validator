using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Validator.Core.Abstractions;

namespace Validator.Core
{
	public abstract class BaseFieldValidator<TType> : AbstractValidator<TType>, IFieldValidator<TType>
	{
		// Hiding the base AbstractValidator<TType>.ValidateAsync(T, CancellationToken)
		// here as want to keep ours instead
		public new Task<ValidationResult> ValidateAsync(TType validatingType, CancellationToken token = default)
		{
			var result = Validate(validatingType);

			var validationErrors = result.Errors
				.Select(validationFailure => new ValidationError
				{
					ErrorMessage = validationFailure.ErrorMessage,
					ErrorCode = validationFailure.ErrorCode,
					PropertyName = validationFailure.PropertyName
				})
				.ToList();

			return Task.FromResult(new ValidationResult
			{
				ValidationErrors = validationErrors
			});
		}
	}
}