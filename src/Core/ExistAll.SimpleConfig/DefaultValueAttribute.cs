using System;

namespace ExistAll.SimpleConfig
{
	public class DefaultValueAttribute : ConditionalDefaultValueBaseAttribute
	{
		public sealed override object DefaultValue { get; }

		public override bool ShouldUse => true;

		public DefaultValueAttribute(object defaultValue, params object[] defaultValues)
		{
			if (defaultValues == null || defaultValues.Length == 0)
			{
				DefaultValue = defaultValue;
				return;
			}

			var newLength = defaultValues.Length + 1;

			var newArray = new object[newLength];

			newArray[0] = defaultValue;

			Array.Copy(defaultValues, 0, newArray, 1, defaultValues.Length);

			DefaultValue = newArray;
		}
	}
}