using System;

namespace ExistAll.SimpleConfig
{
	public abstract class ConditionalDefaultValueBaseAttribute : Attribute
	{
		public abstract object DefaultValue { get; set; }

		public abstract bool ShouldUse { get; }
	}
}