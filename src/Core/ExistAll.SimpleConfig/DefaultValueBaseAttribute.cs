using System;

namespace ExistAll.SimpleConfig
{
	public abstract class DefaultValueBaseAttribute : Attribute
	{
		public abstract object DefaultValue { get; set; }
	}
}