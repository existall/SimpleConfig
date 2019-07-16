using ExistAll.SimpleConfig.Binder;
using ExistAll.SimpleConfig.Binders;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.Binders.CommandLine
{
    public class ArgumentsTests
    {
        private const string Args = " name=value age:3";

        [Fact]
        public void Build_WhenNameMatchKey_ShouldPlaceValue()
        {
            var sut = ConfigBuilder.CreateBuilder(x =>
                x.AddArguments(Args.Split(' ')));

            var result = sut.GetConfig<ICommandLineInterface>();

            Assert.Equal("value", result.Name);
            Assert.NotEqual(3, result.Age);
        }

        [Fact]
        public void Build_WhenNameMatchKeyInSensitive_ShouldPlaceValue()
        {
            var sut = ConfigBuilder.CreateBuilder(x =>
                x.AddArguments(Args.Split(' '),
                    o => o.SetCaseSensitivity(false)));

            var result = sut.GetConfig<ICommandLineInterface>();

            Assert.Equal("value", result.Name);
            Assert.Equal(3, result.Age);
        }

        [Fact]
        public void Build_WhenArgumentAreAfterInMemory_ShouldUseArgumentBinder()
        {
            var section = nameof(ICommandLineInterface).TrimStart('I');
            var collection = new InMemoryCollection();
            collection.Add(section, "name", "name");
            collection.Add(section, "Age", "15");

            var sut = ConfigBuilder.CreateBuilder(x =>
                x.AddInMemoryCollection(collection));

            var result = sut.GetConfig<ICommandLineInterface>();

            Assert.Equal("name", result.Name);
            Assert.Equal(15, result.Age);

            sut = ConfigBuilder.CreateBuilder(x =>
                x.AddInMemoryCollection(collection)
                    .AddArguments(Args.Split(' '),
                        o => o.SetCaseSensitivity(false)));

            result = sut.GetConfig<ICommandLineInterface>();

            Assert.Equal("value", result.Name);
            Assert.Equal(3, result.Age);
        }

        public interface ICommandLineInterface
        {
            [ConfigProperty("name")] string Name { get; set; }

            int Age { get; set; }
        }
    }
}