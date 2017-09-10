using System.Runtime.Remoting.Metadata.W3cXsd2001;
using ExistAll.SimpleConfig.DotNet.Frameworks;

namespace ExistAll.SimpleConfig.Tests.Frameworks
{
	[ConfigSection]
	public interface IWithConfigurationValue
	{
		[AppSettingsValue(TestsConstanst.AppSettingsKeyWithValue)]
		string WithValue { get; set; }

		[AppSettingsValue(TestsConstanst.AppSettingsKeyWithoutValue)]
		string WithoutValue { get; set; }
	}

	public class TestsConstanst
	{
		public const string AppSettingsKeyWithValue = "key";

		public const string AppSettingsValue = "someValue";

		public const string AppSettingsKeyWithoutValue = "noValue";
	}
}
