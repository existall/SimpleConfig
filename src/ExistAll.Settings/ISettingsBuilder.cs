using System;

namespace ExistAll.Settings
{
	public interface ISettingsBuilder
	{
		void Add(ISectionBinder sectionBinder);
		SettingsCollection Build(AssemblyCollection assemblies, SettingsOptions options);
	}

	public class SettingsOptionsArgumentNullException : Exception
	{
		public SettingsOptionsArgumentNullException() : base(Resources.SettingsOptionsArgumentNullMessage)
		{
		}
	}
}