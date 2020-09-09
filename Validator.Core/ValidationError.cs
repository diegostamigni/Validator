using System.Runtime.Serialization;

namespace Validator.Core
{
	[DataContract]
	public class ValidationError
	{
		[DataMember]
		public string ErrorMessage { get; set; }

		[DataMember]
		public string ErrorCode { get; set; }

		[DataMember]
		public string PropertyName { get; set; }

		public override string ToString() => $"{this.PropertyName}: [{this.ErrorCode}] {this.ErrorMessage}";
	}
}