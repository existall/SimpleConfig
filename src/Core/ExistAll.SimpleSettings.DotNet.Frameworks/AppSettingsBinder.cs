using System.Configuration;
using System.Linq;
using ExistAll.SimpleSettings;
using ExistAll.SimpleSettings.Binder;

namespace ExistAll.SimpleSettings.DotNet.Frameworks
{
	public class AppSettingsBinder : ISectionBinder
	{
		public NameFormatter VariableNameFormatter { get; set; }
		
		public void BindPropertySettings(BindingContext context)
		{
			var key = VariableNameFormatter != null ? VariableNameFormatter(context.Section, context.Key) : context.Key;

			if (!ConfigurationManager.AppSettings.AllKeys.Contains(key)) 
				return;
			
			var value = ConfigurationManager.AppSettings[key];
			context.SetNewValue(value);
		}
	}
}
