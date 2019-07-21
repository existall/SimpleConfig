﻿using System.Configuration;
using System.Linq;
using ExistAll.SimpleConfig.Binder;

namespace ExistAll.SimpleConfig.DotNet.Frameworks
{
	public class AppSettingsBinder : ISectionBinder
	{
		public NameFormatter VariableNameFormatter { get; set; }
		
		public void BindPropertyConfig(BindingContext context)
		{
			var key = VariableNameFormatter != null ? VariableNameFormatter(context.Section, context.Key) : context.Key;

			if (!ConfigurationManager.AppSettings.AllKeys.Contains(key)) 
				return;
			
			var value = ConfigurationManager.AppSettings[key];
			context.SetNewValue(value);
		}
	}
}
