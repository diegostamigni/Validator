using System;
using FluentValidation;
using Validator.Core;

namespace Validator.Samples
{
	public class HelloItemFieldValidator : BaseFieldValidator<HelloItem>
	{
		public HelloItemFieldValidator()
		{
			RuleFor(x => x.FirstName)
				.Length(10)
				.When(x => !string.IsNullOrEmpty(x.FirstName));

			RuleFor(x => x.LastName)
				.Length(10)
				.When(x => !string.IsNullOrEmpty(x.LastName));

			RuleFor(x => x.DateOfBirth)
				.LessThanOrEqualTo(DateTime.Today.AddYears(-100))
				.When(x => x.DateOfBirth != null);
		}
	}
}