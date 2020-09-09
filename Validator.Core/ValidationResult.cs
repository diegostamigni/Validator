using System.Collections.Generic;
using System.Linq;

namespace Validator.Core
{
	public class ValidationResult
	{
		public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

		public bool IsValid => this.ValidationErrors.Count == 0;

		public void ThrowIfInvalid()
		{
			if (this.ValidationErrors.Any())
			{
				throw new ValidationException(this.ValidationErrors);
			}
		}
	}
}