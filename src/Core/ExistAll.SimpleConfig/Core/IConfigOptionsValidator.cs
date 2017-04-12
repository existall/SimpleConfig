namespace ExistAll.SimpleConfig.Core
{
	internal interface IConfigOptionsValidator
	{
		void ValidateOptions(ConfigOptions configOptions);
	}

	internal class ConfigOptionsValidator : IConfigOptionsValidator
	{
		public void ValidateOptions(ConfigOptions configOptions)
		{
			if (configOptions.AttributeType == null &&
				configOptions.InterfaceBase == null &&
				string.IsNullOrWhiteSpace(configOptions.ConfigSufix))
			{
				throw new ConfigOptionsArgumentNullException();
			}

			if(string.IsNullOrWhiteSpace(configOptions.ArraySplitDelimiter))
				throw new ConfigOptionsArgumentMissingException(nameof(configOptions.ArraySplitDelimiter));

			if(string.IsNullOrWhiteSpace(configOptions.DateTimeFormat))
				throw new ConfigOptionsArgumentMissingException(nameof(configOptions.DateTimeFormat));
		}
	}
}