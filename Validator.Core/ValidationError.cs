namespace Validator.Core
{
	public class ValidationError
	{
		public string ErrorMessage { get; set; }

		public string ErrorCode { get; set; }

		public string PropertyName { get; set; }
	}
}