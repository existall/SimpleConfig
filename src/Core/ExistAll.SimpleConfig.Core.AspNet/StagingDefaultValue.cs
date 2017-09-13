namespace ExistAll.SimpleConfig.Core.AspNet
{
	public class StagingDefaultValue : EnvironmentValueAttribute
	{
		public StagingDefaultValue(object defaultValue, params object[] defaultValues) :
			base(Environments.Staging, defaultValue, defaultValues)
		{
		}
	}
}