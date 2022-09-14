namespace Validator.Abstractions;

public interface IAggregateValidator<in TType> : IValidator<TType>
{
}