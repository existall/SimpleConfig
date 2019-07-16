using System;

namespace ExistAll.SimpleConfig
{
	[AttributeUsage(AttributeTargets.Property)]
	public class ConfigPropertyAttribute : Attribute
	{
		public ConfigPropertyAttribute(string name)
		{
			Name = name;
		}

		public ConfigPropertyAttribute() { }
		
		public string Name { get; set; }

		public Type ConvertorType { get; set; }

		public bool AllowEmpty { get; set; } = true;
	}
}