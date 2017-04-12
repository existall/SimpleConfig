using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.SimpleConfig
{
	public interface IAssemblyHolder
	{
		Assembly Assembly { get; }
		IEnumerable<Type> Types { get; }
	}
}