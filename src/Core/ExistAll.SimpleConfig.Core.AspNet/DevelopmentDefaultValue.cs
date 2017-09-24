namespace ExistAll.SimpleConfig.Core.AspNet
{
	public class DevelopmentDefaultValue : EnvironmentValueAttribute
	{
		public DevelopmentDefaultValue(object defaultValue, params object[] defaultValues) :
			base(Environments.Development, defaultValue, defaultValues)
		{
		}
	}
}