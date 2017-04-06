using System;
using System.Collections.Generic;

namespace ExistAll.Settings
{
	public interface ISettingsBuilder
	{
		void Add(ISectionBinder sectionBinder);
		SettingsCollection Build(AssemblyCollection assemblies, SettingsOptions options);
	}

	public interface ISectionBinder
	{
		void Bind(SettingsBindingContext bindingContext);
	}

	public class SettingsBuilder : ISettingsBuilder
	{
		private int _counter = 0;
		private readonly SortedList<int, ISectionBinder> _binders = new SortedList<int, ISectionBinder>();

		public SettingsCollection Build(AssemblyCollection assemblies, SettingsOptions options)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));
			ValidateOptions(options);



			return null;
		}

		public void Add(ISectionBinder sectionBinder)
		{
			if (sectionBinder == null) throw new ArgumentNullException(nameof(sectionBinder));
			_binders.Add(_counter++, sectionBinder);
		}

		private void ValidateOptions(SettingsOptions settingsOptions)
		{
			if (settingsOptions.AttributeType == null &&
				settingsOptions.InterfaceBase == null &&
			    string.IsNullOrWhiteSpace(settingsOptions.SettingSufix))
			{
				throw new SettingsOptionsArgumentNullException();
			}

		}
	}

	public class SettingsOptionsArgumentNullException : Exception
	{
		public SettingsOptionsArgumentNullException() : base(Resources.SettingsOptionsArgumentNullMessage)
		{
		}
	}
}