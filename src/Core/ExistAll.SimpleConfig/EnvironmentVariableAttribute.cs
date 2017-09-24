using System;

namespace ExistAll.SimpleConfig
{
	public class EnvironmentVariableAttribute : ConditionalValueBaseAttribute
	{
		private readonly string _environmentVariable;

		public EnvironmentVariableAttribute(string environmentVariable)
		{
			_environmentVariable = environmentVariable;
		}

		public override object DefaultValue
		{
			get { return Environment.GetEnvironmentVariable(_environmentVariable); }
			protected set { }
		}

		public override bool ShouldUse => true;
	}
}
