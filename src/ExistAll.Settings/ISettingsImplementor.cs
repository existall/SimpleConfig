using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ExistAll.Settings
{
	public interface ISettingsImplementor
	{
		void SetValues(IDictionary<string, object> values = null);
	}

	public abstract class SettingsImplementor<T> : ISettingsImplementor
	{
		public void SetValues(IDictionary<string, object> values = null)
		{
			//foreach (var property in SettingsImplementorGenerator.GetInterfaceProperties(typeof(T)))
			//	SetValue(property, values ?? new Dictionary<string, object>());
		}

		private void SetValue(PropertyInfo property, IDictionary<string, object> values)
		{
			object value = null;
			try
			{
				var loaded =
					values.TryGetValue(typeof(T).Name.Substring(1) + "." + property.Name, out value) ||
					values.TryGetValue(property.DeclaringType.Name.Substring(1) + "." + property.Name, out value);

				if (!loaded)
				{
					var defaultAttribute = property.GetCustomAttribute<DefaultAttribute>();
					if (defaultAttribute != null)
						value = defaultAttribute.DefaultValue;
				}

				else if (property.PropertyType.IsArray)
				{
					value = ((string)value).Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToArray();
				}

				GetType().GetTypeInfo().GetMethod("set_" + property.Name).Invoke(this, new[] { ConvertValue(value, property.PropertyType) });
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}.{1} of type {2} with value {3}", property.DeclaringType.FullName, property.Name, property.PropertyType, value), ex);
			}
		}

		private static object ConvertValue(object value, Type propertyType)
		{
			if (value == null)
				return propertyType.GetTypeInfo().IsValueType ? Activator.CreateInstance(propertyType) : null;

			var strippedType = StripNullable(propertyType);

			if (strippedType == typeof(Uri))
				return new Uri((string)value);

			if (strippedType == typeof(DateTime))
				return ConvertFromDateTime(value);

			if (strippedType.GetTypeInfo().IsEnum)
				return ConvertEnumType(strippedType, value);

			if (strippedType.IsArray)
				return ConvertToArray(value, strippedType);

			return Convert.ChangeType(value, strippedType);
		}

		private static object ConvertToArray(object value, Type strippedType)
		{
			var values = value.GetType().IsArray ? (object[])value : new[] { value };

			var objects = values.Select(x => ConvertValue(x, strippedType.GetElementType())).ToArray();

			var instance = Array.CreateInstance(strippedType.GetElementType(), objects.Length);

			for (var i = 0; i < instance.Length; i++)
			{
				instance.SetValue(objects[i], i);
			}

			return instance;
		}

		private static Type StripNullable(Type type)
		{
			return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) ?
				type.GetTypeInfo().GetGenericArguments()[0] :
				type;
		}

		private static DateTime ConvertFromDateTime(object value)
		{
			return DateTime.ParseExact((string)value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		private static object ConvertEnumType(Type propertyType, object value)
		{
			if (value.GetType() == propertyType)
				return value;

			return Enum.Parse(propertyType, (string)value);
		}
	}
}