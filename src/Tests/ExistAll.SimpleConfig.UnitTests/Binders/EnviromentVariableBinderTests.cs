using ExistAll.SimpleConfig.Binder;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.Binders
{
	public class EnviromentVariableBinderTests
	{
		[Fact]
		public void TryGetValue_WhereVariableNotPresent_ShouldReturnFalse()
		{
			var sut = new EnvironmentVariablesBinder();
			string value;

			var result = sut.TryGetValue(new ConfigBindingContext("section", "key"), out value);
			
			Assert.False(result);
			Assert.Null(value);
		}
		
		[Fact]
		public void TryGetValue_WhereVariablePresent_ShouldReturnTrue()
		{
			var sut = new EnvironmentVariablesBinder();
			string value;

			var result = sut.TryGetValue(new ConfigBindingContext("section", "key"), out value);
			
			Assert.False(result);
			Assert.Null(value);
		}
	}
}