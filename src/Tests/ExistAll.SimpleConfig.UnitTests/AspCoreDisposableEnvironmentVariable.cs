namespace ExistAll.SimpleConfig.UnitTests
{
	internal class AspCoreDisposableEnvironmentVariable : DisposableEnvironmentVariable
	{
		public AspCoreDisposableEnvironmentVariable(string value) 
			: base("ASPNETCORE_ENVIRONMENT", value)
		{
		}
	}
}