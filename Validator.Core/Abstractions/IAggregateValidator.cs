namespace Validator.Core.Abstractions
{
	public interface IAggregateValidator<in TType> : IValidator<TType>
	{
	}
}