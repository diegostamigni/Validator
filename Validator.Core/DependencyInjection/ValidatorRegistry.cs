using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Validator.Core.Abstractions;

namespace Validator.Core.DependencyInjection
{
	public class ValidatorRegistry : ServiceRegistry
	{
		public ValidatorRegistry()
			: this(ServiceLifetime.Scoped)
		{
		}

		public ValidatorRegistry(ServiceLifetime serviceLifetime)
		{
			Scan(s =>
			{
				s.AssemblyContainingType(typeof(IValidator<>));
				s.WithDefaultConventions(serviceLifetime);
			});
		}
	}
}