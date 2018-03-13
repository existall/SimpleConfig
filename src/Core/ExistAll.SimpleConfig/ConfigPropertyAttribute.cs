using System;

namespace ExistAll.SimpleConfig
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class ConfigPropertyAttribute : Attribute
	{
		public string Name { get; set; }
	}
}