namespace ExistAll.SimpleConfig
{
	public class DefaultValueAttribute : DefaultValueBaseAttribute
	{
		public override bool ShouldUse => true;

		public DefaultValueAttribute(object defaultValue, params object[] defaultValues)
			:base(defaultValue, defaultValues)
		{
			
		}
	}
}