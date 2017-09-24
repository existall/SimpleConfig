using System.Configuration;

namespace ExistAll.SimpleConfig.DotNet.Frameworks
{
    public class AppSettingsValue : ConditionalValueBaseAttribute
	{
		private readonly string _key;

		public AppSettingsValue(string key)
		{
			_key = key;
		}

		public override object DefaultValue
		{
			get => ConfigurationManager.AppSettings[_key];
			protected set { }
		}

		public override bool ShouldUse => true;
	}
}
