namespace ExistAll.SimpleConfig.Core.AspNet
{
	public class ProductionDefaultValue : EnvironmentValueAttribute
	{
		public ProductionDefaultValue(object defaultValue, params object[] defaultValues) :
			base(Environments.Production, defaultValue, defaultValues)
		{
		}
	}
}