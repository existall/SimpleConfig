namespace ExistAll.SimpleConfig.Core.AspNet
{
	public class StagingDefaultValue : EnvironmentValueAttribute
	{
		public StagingDefaultValue(object defaultValue) :
			base(defaultValue, (string) Environments.Staging)
		{
		}
	}
}