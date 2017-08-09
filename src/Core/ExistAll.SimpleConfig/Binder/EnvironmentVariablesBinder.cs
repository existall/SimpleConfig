using System;
using System.Collections;

namespace ExistAll.SimpleConfig.Binder
{
	public class EnvironmentVariablesBinder : ISectionBinder
	{
		private readonly IDictionary _environmentVariables;
		
		public EnvironmentVariablesBinder()
		{
			_environmentVariables = Environment.GetEnvironmentVariables();
		}

		public bool TryGetValue(ConfigBindingContext bindingContext, out string value)
		{
			value = null;
			
			var preset = _environmentVariables.Contains(bindingContext.Key);

			if (!preset)
				return false;
			
			value = Environment.GetEnvironmentVariable(bindingContext.Key);

			return true;
		}
	}
}