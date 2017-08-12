using System.Collections;

namespace ExistAll.SimpleConfig
{
	internal interface IEnvironmentVariablesProvider
	{
		IDictionary GetEnvironmentVariables();
	}
}