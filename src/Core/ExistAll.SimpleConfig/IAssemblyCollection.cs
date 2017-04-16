using System;
using System.Collections.Generic;

namespace ExistAll.SimpleConfig
{
	public interface IAssemblyCollection
	{
		void Add(IAssemblyHolder assemblyHolder);
		IEnumerable<Type> GetTypes();
	}
}