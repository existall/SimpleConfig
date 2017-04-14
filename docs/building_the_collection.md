# Building The Collection
To build the `ConfigCollection` you must provide assemblies to which SimpleConfig scans them and get all the config interfaces.

Thus SimpleConfig provides an AssemblyCollection for you to set all assemblies you want it to scan.
We know that many applications uses different architectures so `AssemblyCollection` has one method

`public void Add(IAssemblyHolder assemblyHolder)`

`SimpleConfig` don't expect you to implement the `IAssemblyHolder` interface, it provide two extension methods.

1. `AddExportedTypesAssembly` - Get the public config interfaces in the assemblies.
2. `AddFullTypesAssembly` - Get all of the config interfaces in the assemblies, even the internal.


````C#
var assemblyCollection = new AssemblyCollection()
				.AddFullTypesAssembly(this.GetType().GetTypeInfo().Assembly)
				.AddExportedTypesAssembly(typeof(SomeType).GetTypeInfo().Assembly);
````

## Options

To the build method we need to pass `ConfigOptions`, this Options class allows you to replace some of SimpleConfig defaults. To better understand the ConfigOptions see the [Extend]() section.

## Binders
SimpleConfig can populate properties with values not only from `DefaultValue` attribute but from any `Binder` you can create.

To add a Binder use the `Add` method on the `ConfigBuilder` like so.
````C#
ConfigBuilder.Add(ISectionBinder sectionBinder);
````

#### The order in which binders are added is important, the last binder will set the value into the property. If no value was set the deafult value will be set.

SimpleConfig provides two Binders out of the box, `ConfigurationBinder` which can extract values from Microsoft .Net Core `IConfiguraton` and `InMemoryCollection`. We are currently working on `SqlBinder`.

For more information about the Binders click on them.
1. [ConfigurationBinder]()
2. [InMemoryBinder]()

To create new Binders see [Extend section]()

To continue on to Config Interfaces click [here]()
