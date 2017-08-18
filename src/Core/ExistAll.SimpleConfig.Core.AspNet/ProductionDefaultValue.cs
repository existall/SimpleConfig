namespace ExistAll.SimpleConfig.Core.AspNet
{
	public class ProductionDefaultValue : EnvironmentValueAttribute
	{
		public ProductionDefaultValue(object defaultValue) :
			base(defaultValue, (string) Environments.Production)
		{
		}
	}
}