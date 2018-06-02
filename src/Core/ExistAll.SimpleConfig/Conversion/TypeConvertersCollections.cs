using System.Collections.Generic;

namespace ExistAll.SimpleConfig.Conversion
{
	internal class TypeConvertersCollections : LinkedList<IConfigTypeConverter>
	{
		public TypeConvertersCollections(ConfigOptions configOptions)
		{
			AddLast(new DateTimeTypeConverter(configOptions));
			AddLast(new UriTypeConvertor());
			AddLast(new ArrayTypeConverter(configOptions, this));
			AddLast(new EnumerableTypeConverter(configOptions, this));
			AddLast(new DefaultTypeConverter());
		}
	}
}