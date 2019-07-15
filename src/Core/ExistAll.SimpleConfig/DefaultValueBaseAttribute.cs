using System;

namespace ExistAll.SimpleConfig
{
	[AttributeUsage(AttributeTargets.Property)]
	public abstract class DefaultValueBaseAttribute : Attribute
	{
		public object DefaultValue { get; protected set; }
	}
}