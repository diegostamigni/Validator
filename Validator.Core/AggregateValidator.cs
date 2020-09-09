using System.Threading;
using System.Threading.Tasks;
using Validator.Core.Abstractions;

namespace Validator.Core
{
	public class AggregateValidator<TType> : IAggregateValidator<TType>
	{
		private readonly IFieldValidator<TType>[] fieldValidators;
		private readonly IBusinessRuleValidator<TType>[] businessRuleValidators;

		public AggregateValidator(
			IFieldValidator<TType>[] fieldValidators,
			IBusinessRuleValidator<TType>[] businessRuleValidators)
		{
			this.fieldValidators = fieldValidators;
			this.businessRuleValidators = businessRuleValidators;
		}

		public async Task<ValidationResult> ValidateAsync(TType validatingType, CancellationToken token = default)
		{
			var result = new ValidationResult();

			if (this.fieldValidators != null)
			{
				foreach (var fieldValidator in this.fieldValidators)
				{
					var fResult = await fieldValidator
						.ValidateAsync(validatingType, token)
						.ConfigureAwait(false);

					result.ValidationErrors.AddRange(fResult.ValidationErrors);
					if (!fResult.IsValid)
					{
						break;
					}
				}

				if (!result.IsValid)
				{
					return result;
				}
			}

			if (this.businessRuleValidators != null)
			{
				foreach (var businessRuleValidator in this.businessRuleValidators)
				{
					var brResult = await businessRuleValidator
						.ValidateAsync(validatingType, token)
						.ConfigureAwait(false);

					result.ValidationErrors.AddRange(brResult.ValidationErrors);
					if (!brResult.IsValid)
					{
						break;
					}
				}
			}

			return result;
		}
	}
}