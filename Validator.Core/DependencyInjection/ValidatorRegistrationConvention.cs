using System.Collections.Generic;
using System.Linq;
using BaselineTypeDiscovery;
using Lamar;
using Lamar.Scanning.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Validator.Core.Abstractions;

namespace Validator.Core.DependencyInjection
{
	public class ValidatorRegistrationConvention : IRegistrationConvention
	{
		private readonly ServiceLifetime serviceLifetime;

		private static readonly HashSet<string> SupportedValidators = new HashSet<string>
		{
			typeof(IFieldValidator<>).Name,
			typeof(IBusinessRuleValidator<>).Name
		};

		public ValidatorRegistrationConvention()
			: this(ServiceLifetime.Scoped)
		{
		}

		public ValidatorRegistrationConvention(ServiceLifetime serviceLifetime)
		{
			this.serviceLifetime = serviceLifetime;
		}

		public void ScanTypes(TypeSet types, ServiceRegistry services)
		{
			var concreteValidators = types
				.AllTypes()
				.Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces()
					.Select(z => z.Name)
					.Intersect(SupportedValidators)
					.Any());

			foreach (var concreteValidator in concreteValidators)
			{
				var validInterfaces = concreteValidator
					.GetInterfaces()
					.Where(x => SupportedValidators.Contains(x.Name));

				foreach (var validInterface in validInterfaces)
				{
					var instance = services.For(validInterface).Use(concreteValidator);
					instance.Lifetime = serviceLifetime;
				}
			}
		}
	}
}