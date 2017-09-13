using System;

namespace ExistAll.SimpleConfig
{
	public abstract class ConditionalValueBaseAttribute : Attribute
	{
		public abstract object DefaultValue { get; protected set; }

		public abstract bool ShouldUse { get; }
	}
}