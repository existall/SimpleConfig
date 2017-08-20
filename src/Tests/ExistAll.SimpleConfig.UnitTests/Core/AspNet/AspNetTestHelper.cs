using System;

namespace ExistAll.SimpleConfig.UnitTests.Core.AspNet
{
	internal static class AspNetTestHelper
	{
		public const string VariableName = "ASPNETCORE_ENVIRONMENT";


		public static void SetEnvVariable(string env)
		{
			Environment.SetEnvironmentVariable(VariableName, env);
		}

		public static void ResetEnvVariable()
		{
			Environment.SetEnvironmentVariable(VariableName, null);
		}
	}
}