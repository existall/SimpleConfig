using System;

namespace ExistAll.SimpleConfig
{
	[AttributeUsage(AttributeTargets.Property)]
	public class ConfigPropertyAttribute : Attribute
	{
		public string Name { get; set; }

		public Type ConvertorType { get; set; }

		public bool AllowEmpty { get; set; } = true;
	}
}