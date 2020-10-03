using System;

namespace ExistAll.SimpleSettings
{
	public class SettingsBindingException : Exception
	{
		public SettingsBindingException(ISectionBinder binder, BindingContext conextBindingContext, Exception e)
			: base(Resources.SettingsBindingExceptionMessage(binder, conextBindingContext.Section, conextBindingContext.Key),e)
		{
		}
	}
}