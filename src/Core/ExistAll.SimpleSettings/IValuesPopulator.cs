using System;
using System.Collections.Generic;

namespace ExistAll.SimpleSettings
{
	internal interface IValuesPopulator
	{
		void PopulateInstanceWithValues(object instance,
			Type settings,
			SettingsOptions options,
			IEnumerable<ISectionBinder> binders);
	}
}