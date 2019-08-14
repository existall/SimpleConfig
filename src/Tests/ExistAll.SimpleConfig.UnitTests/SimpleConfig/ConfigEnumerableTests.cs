using System.Collections.Generic;
using System.ComponentModel;
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
            var sut = ConfigBuilder.CreateBuilder();

            var result = sut.GetConfig<IEnumerableInterface>();
            
            Assert.Equal(4, result.Values.Count());

            Assert.Contains(N12, result.Values);
            Assert.Contains(N3, result.Values);
            Assert.Contains(N4, result.Values);
            Assert.Contains(N103, result.Values);
        }

        [ConfigSection]
        public interface IEnumerableInterface
        {
            [ConfigProperty(DefaultValue = new[] {N12, N3, N4, N103})]
            IEnumerable<int> Values { get; set; }
        }
    }
}