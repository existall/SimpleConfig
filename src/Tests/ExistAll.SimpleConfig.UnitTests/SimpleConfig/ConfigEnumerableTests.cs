using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{
	public class ConfigEnumerableTests
	{
		[Fact]
		public void Build_WhereVariableHasValue_ShouldSetProperty()
		{
			var sut = new ConfigBuilder();

			var result = sut.Build(new[] {typeof(IEnumerableInterface)});

			var enumerable = result.GetConfig<IEnumerableInterface>();

			Assert.Equal(enumerable.Values.Count(), 5);
		}

		[ConfigSection]
		public interface IEnumerableInterface
		{
			[DefaultValue(12,3,4,5,6)]
			IEnumerable<int> Values { get; set; }
		}

	}
}
