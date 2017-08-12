namespace ExistAll.SimpleConfig
{
	public class EnvironmentVariableAttribute : EnvironmentVariableBaseAttribute
	{
		public override string Variable { get; }

		public EnvironmentVariableAttribute(string variable)
		{
			Variable = variable;
		}
	}
}