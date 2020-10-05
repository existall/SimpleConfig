using System;

namespace ExistForAll.SimpleSettings
{
	public class SettingsBindingException : Exception
	{
		public SettingsBindingException(ISectionBinder binder, BindingContext conextBindingContext, Exception e)
			: base(Resources.SettingsBindingExceptionMessage(binder, conextBindingContext.Section, conextBindingContext.Key),e)
		{
		}
	}
}