using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Validator.Core;
using Validator.Core.Abstractions;
using Validator.Core.DependencyInjection;

namespace Validator.Samples.DependencyInjection
{
	public class SamplesRegistry : ServiceRegistry
	{
		public SamplesRegistry()
			: this(ServiceLifetime.Scoped)
		{
		}

		public SamplesRegistry(ServiceLifetime serviceLifetime)
		{
			Scan(s =>
			{
				s.AssemblyContainingType<HelloItemBusinessRuleValidator>();
				s.WithDefaultConventions(serviceLifetime);
				s.With(new ValidatorRegistrationConvention(serviceLifetime));
			});

			For(typeof(IAggregateValidator<>))
				.Use(typeof(AggregateValidator<>))
				.Lifetime = serviceLifetime;

			IncludeRegistry<ValidatorRegistry>();
		}
	}
}