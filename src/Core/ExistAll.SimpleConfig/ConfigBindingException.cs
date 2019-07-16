using System;

namespace ExistAll.SimpleConfig
{
	public class ConfigBindingException : Exception
	{
		public ConfigBindingException(ISectionBinder binder, BindingContext conextBindingContext, Exception e)
			: base(Resources.ConfigBindingExceptionMessage(binder, conextBindingContext.Section, conextBindingContext.Key),e)
		{
		}
	}
}