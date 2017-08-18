namespace ExistAll.SimpleConfig.Core.AspNet
{
	public class DevelopmentDefaultValue : EnvironmentValueAttribute
	{
		public DevelopmentDefaultValue(object defaultValue) :
			base(defaultValue, (string) Environments.Development)
		{
		}
	}
}