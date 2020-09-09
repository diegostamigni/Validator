using System;
using System.Collections.Generic;

namespace Validator.Core
{
	public class ValidationException : Exception
	{
		public ValidationException(List<ValidationError> validationErrors)
		{
			// TODO: Use validationErrors to produce proper error message
		}
	}
}