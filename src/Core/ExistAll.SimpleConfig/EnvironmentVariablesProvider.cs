using System;
using System.Collections;

namespace ExistAll.SimpleConfig
{
	internal class EnvironmentVariablesProvider : IEnvironmentVariablesProvider
	{
		public IDictionary GetEnvironmentVariables()
		{
			return Environment.GetEnvironmentVariables();
		}
	}
}