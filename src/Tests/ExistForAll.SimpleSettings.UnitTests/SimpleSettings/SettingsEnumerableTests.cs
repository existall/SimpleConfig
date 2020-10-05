using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ExistForAll.SimpleSettings.UnitTests.SimpleSettings
{
    public class SettingsEnumerableTests
    {
        private const int N12 = 12;
        private const int N3 = 3;
        private const int N4 = 4;
        private const int N103 = 103;

        [Fact]
        public void Build_WhereVariableHasValue_ShouldSetProperty()
        {
            var sut = SettingsBuilder.CreateBuilder();

            var result = sut.GetSettings<IEnumerableInterface>();
            
            Assert.Equal(4, result.Values.Count());

            Assert.Contains(N12, result.Values);
            Assert.Contains(N3, result.Values);
            Assert.Contains(N4, result.Values);
            Assert.Contains(N103, result.Values);
        }

        [SettingsSection]
        public interface IEnumerableInterface
        {
            [SettingsProperty(DefaultValue = new[] {N12, N3, N4, N103})]
            IEnumerable<int> Values { get; set; }
        }
    }
}