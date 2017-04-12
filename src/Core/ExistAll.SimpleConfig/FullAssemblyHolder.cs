using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.SimpleConfig
{
	public class FullAssemblyHolder : IAssemblyHolder
	{
		public Assembly Assembly { get; }

		private readonly Lazy<IEnumerable<Type>> _lazyTypes;

		public FullAssemblyHolder(Assembly assembly)
		{
			Assembly = assembly;
			_lazyTypes = new Lazy<IEnumerable<Type>>(GetTypes);
		}

		public IEnumerable<Type> Types => _lazyTypes.Value;

		private Type[] GetTypes()
		{
			return Assembly.GetTypes();
		}
	}
}