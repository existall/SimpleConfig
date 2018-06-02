using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{
	public class ConfigEnumerableTests
	{
		private const int N12 = 12;
		private const int N3 = 3;
		private const int N4 = 4;
		private const int N103 = 103;

		[Fact]
		public void Build_WhereVariableHasValue_ShouldSetProperty()
		{
			var sut = new ConfigBuilder();

			var result = sut.Build(new[] {typeof(IEnumerableInterface)});

			var enumerable = result.GetConfig<IEnumerableInterface>();

			Assert.Equal(enumerable.Values.Count(), 4);

			Assert.Contains(N12, enumerable.Values);
			Assert.Contains(N3, enumerable.Values);
			Assert.Contains(N4, enumerable.Values);
			Assert.Contains(N103, enumerable.Values);
		}

		[ConfigSection]
		public interface IEnumerableInterface
		{
			[DefaultValue(N12, N3, N4, N103)]
			IEnumerable<int> Values { get; set; }
		}
	}
}
