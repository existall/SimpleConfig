using System;
using System.Reflection;

namespace ExistAll.SimpleConfig.Core
{
	internal class ConfigOptionsValidator : IConfigOptionsValidator
	{
		public void ValidateOptions(ConfigOptions configOptions)
		{
			if (configOptions.AttributeType == null &&
			    configOptions.InterfaceBase == null &&
			    string.IsNullOrWhiteSpace(configOptions.ConfigSuffix))
			{
				throw new ConfigOptionsArgumentNullException();
			}

            if (!typeof(Attribute).GetTypeInfo().IsAssignableFrom(configOptions.AttributeType)) 
                throw new ConfigOptionNonAttributeException(configOptions.AttributeType);


            if (string.IsNullOrWhiteSpace(configOptions.ArraySplitDelimiter))
				throw new ConfigOptionsArgumentMissingException(nameof(configOptions.ArraySplitDelimiter));

			if (string.IsNullOrWhiteSpace(configOptions.DateTimeFormat))
				throw new ConfigOptionsArgumentMissingException(nameof(configOptions.DateTimeFormat));

			if (configOptions.SectionNameFormatter == null)
				throw new ConfigOptionsArgumentMissingException("SectionNameFormatter");
		}
	}
}