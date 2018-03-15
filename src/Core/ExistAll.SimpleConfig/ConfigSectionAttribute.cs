using System;

namespace ExistAll.SimpleConfig
{
	[AttributeUsage(AttributeTargets.Interface,AllowMultiple = false)]
	public class ConfigSectionAttribute : Attribute
	{
		public string Name { get; set; }
	}
}