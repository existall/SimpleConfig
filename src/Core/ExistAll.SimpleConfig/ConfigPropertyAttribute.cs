using System;

namespace ExistAll.SimpleConfig
{
	[AttributeUsage(AttributeTargets.Property)]
	public class ConfigPropertyAttribute : Attribute
	{
		public object DefaultValue { get; set; }

		public string Name { get; set; }

		public Type ConverterType { get; set; }

		public bool AllowEmpty { get; set; } = true;
		
		public ConfigPropertyAttribute(string name)
		{
			Name = name;
		}
		
		public ConfigPropertyAttribute() { }
	}
}