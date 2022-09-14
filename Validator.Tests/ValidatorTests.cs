using System;
using System.Threading.Tasks;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using Validator.Abstractions;

namespace Validator.Tests;

[TestFixture]
public class ValidatorTests
{
	private Container container;

	[OneTimeSetUp]
	public void OneTimeSetUp()
	{
		this.container = new(cfg =>
		{
			cfg.Scan(s =>
			{
				s.AssemblyContainingType<HelloItemBusinessRuleValidator>();
				s.WithDefaultConventions(ServiceLifetime.Scoped);
				s.AddAllTypesOf(typeof(IValidator<>), ServiceLifetime.Scoped);
			});

			cfg.For(typeof(IAggregateValidator<>))
				.Use(typeof(AggregateValidator<>))
				.Lifetime = ServiceLifetime.Scoped;
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