using System;

namespace ExistAll.SimpleConfig
{
	public abstract class EnvironmentVariableBaseAttribute : Attribute
	{
		public abstract string Variable { get; }
	}
}