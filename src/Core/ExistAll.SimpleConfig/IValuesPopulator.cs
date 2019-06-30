using System;
using System.Collections.Generic;

namespace ExistAll.SimpleConfig
{
	internal interface IValuesPopulator
	{
		void PopulateInstanceWithValues(object instance,
			Type config,
			ConfigOptions options,
			IEnumerable<ISectionBinder> binders);
	}
}