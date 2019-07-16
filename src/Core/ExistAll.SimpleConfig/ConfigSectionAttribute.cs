using System;

namespace ExistAll.SimpleConfig
{
	[AttributeUsage(AttributeTargets.Interface)]
	public class ConfigSectionAttribute : Attribute
	{
		public string Name { get; set; }

		public ConfigSectionAttribute()
		{
			
		}
		
		public ConfigSectionAttribute(string name)
		{
			Name = name;
		}
	}
}