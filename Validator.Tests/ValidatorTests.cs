using System;
using System.Threading.Tasks;
using Lamar;
using NUnit.Framework;
using Shouldly;
using Validator.Core;
using Validator.Core.Abstractions;
using Validator.Samples;
using Validator.Samples.DependencyInjection;

namespace Validator.Tests
{
	[TestFixture]
	public class ValidatorTests
	{
		private Container container;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.container = new Container(cfg =>
			{
				cfg.IncludeRegistry<SamplesRegistry>();
			});

			var containerScan = this.container.WhatDidIScan();
			Console.WriteLine(containerScan);

			var containerContent = this.container.WhatDoIHave();
			Console.WriteLine(containerContent);
		}

		[Test]
		public void AssertConfigurationIsValid()
		{
			this.container.AssertConfigurationIsValid();
		}

		[Test]
		public async Task ShouldResolveFieldValidator()
		{
			var item = new HelloItem
			{
				FirstName = "Bruce",
				LastName = "Wayne",
				DateOfBirth = DateTime.Today.AddYears(-40)
			};

			var validator = this.container.GetInstance<IFieldValidator<HelloItem>>();
			var result = await validator.ValidateAsync(item);
			result.IsValid.ShouldBeFalse();
		}

		[Test]
		public async Task ShouldResolveFieldValidator_ShouldThrow()
		{
			var item = new HelloItem
			{
				FirstName = "Bruce",
				LastName = "Wayne",
				DateOfBirth = DateTime.Today.AddYears(-40)
			};

			var validator = this.container.GetInstance<IFieldValidator<HelloItem>>();
			var result = await validator.ValidateAsync(item);
			result.IsValid.ShouldBeFalse();

			var ex = Should.Throw<ValidationException>(result.ThrowIfInvalid);
			ex.ValidationErrors.ShouldNotBeEmpty();
		}

		[Test]
		public async Task ShouldResolveBusinessRuleValidator()
		{
			var item = new HelloItem
			{
				FirstName = "Bruce",
				LastName = "Wayne",
				DateOfBirth = DateTime.Today.AddYears(-40)
			};

			var validator = this.container.GetInstance<IBusinessRuleValidator<HelloItem>>();
			var result = await validator.ValidateAsync(item);
			result.IsValid.ShouldBeTrue();
		}

		[Test]
		public async Task ShouldResolveAggregateValidator()
		{
			var item = new HelloItem
			{
				FirstName = "Bruce",
				LastName = "Wayne",
				DateOfBirth = DateTime.Today.AddYears(-40)
			};

			var validator = this.container.GetInstance<IAggregateValidator<HelloItem>>();
			var result = await validator.ValidateAsync(item);
			result.IsValid.ShouldBeFalse();
		}

		[Test]
		public async Task ShouldResolveAggregateValidator_ShouldThrow()
		{
			var item = new HelloItem
			{
				FirstName = "Bruce",
				LastName = "Wayne",
				DateOfBirth = DateTime.Today.AddYears(-40)
			};

			var validator = this.container.GetInstance<IAggregateValidator<HelloItem>>();
			var result = await validator.ValidateAsync(item);
			result.IsValid.ShouldBeFalse();

			var ex = Should.Throw<ValidationException>(result.ThrowIfInvalid);
			ex.ValidationErrors.ShouldNotBeEmpty();
		}
	}
}