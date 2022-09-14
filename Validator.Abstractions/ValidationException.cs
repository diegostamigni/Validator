using System.Text;

namespace Validator.Abstractions;

public class ValidationException : Exception
{
	public List<ValidationError> ValidationErrors { get; }

	public ValidationException(List<ValidationError> validationErrors)
		: base(GetErrorMessage(validationErrors))
	{
		this.ValidationErrors = validationErrors;
	}

	private static string GetErrorMessage(IEnumerable<ValidationError> validationErrors)
	{
		var stringBuilder = new StringBuilder();
		foreach (var error in validationErrors)
		{
			stringBuilder.AppendLine(error.ToString());
		}

		return stringBuilder.ToString();
	}
}