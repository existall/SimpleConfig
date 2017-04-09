using System;

namespace ExistAll.Settings
{
	public class SettingsBindingException : Exception
	{
		public SettingsBindingException(ISectionBinder binder, SettingsBindingContext conextBindingContext, Exception e)
			: base(Resources.SettingBindingExceptionMessage(binder, conextBindingContext.Section, conextBindingContext.Key),e)
		{
		}
	}
}