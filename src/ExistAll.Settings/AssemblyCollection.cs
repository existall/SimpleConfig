using System;
using System.Collections.Generic;
using System.Linq;

namespace ExistAll.Settings
{
	public class AssemblyCollection
	{
		private readonly HashSet<IAssemblyHolder> _assemblyHolders = new HashSet<IAssemblyHolder>();

		public void Add(IAssemblyHolder assemblyHolder)
		{
			if (assemblyHolder == null) throw new ArgumentNullException(nameof(assemblyHolder));
			_assemblyHolders.Add(assemblyHolder);
		}

		public IEnumerable<Type> GeTypes()
		{
			return _assemblyHolders.SelectMany(x => x.Types)
				.ToArray();
		}
	}
}