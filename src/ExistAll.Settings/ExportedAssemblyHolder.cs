using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.Settings
{
	public class ExportedAssemblyHolder : IAssemblyHolder
	{
		public Assembly Assembly { get; }

		private readonly Lazy<IEnumerable<Type>> _lazyTypes;

		public ExportedAssemblyHolder(Assembly assembly)
		{
			Assembly = assembly;
			_lazyTypes = new Lazy<IEnumerable<Type>>(GetTypes);
		}

		public IEnumerable<Type> Types => _lazyTypes.Value;

		private Type[] GetTypes()
		{
			return Assembly.GetExportedTypes();
		}
	}
}