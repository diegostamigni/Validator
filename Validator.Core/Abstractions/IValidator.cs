using System.Threading;
using System.Threading.Tasks;

namespace Validator.Core.Abstractions
{
	public interface IValidator<in TType>
	{
		Task<ValidationResult> ValidateAsync(TType validatingType, CancellationToken token = default);
	}
}