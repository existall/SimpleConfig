using System.Configuration;

namespace ExistAll.SimpleConfig.DotNet.Frameworks
{
    public class AppSettingsValue : ConditionalDefaultValueBaseAttribute
	{
		private readonly string _key;

		public AppSettingsValue(string key)
		{
			_key = key;
		}

		public override object DefaultValue => ConfigurationManager.AppSettings[_key];

		public override bool ShouldUse => true;
	}
}
