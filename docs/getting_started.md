Getting Started
===============

SimpleConfig uses a `ConfigBuilder` in order to create a `ConfigCollection`. `ConfigCollection`holds a key value pair of `Type` and the generated implemintatin of the Config interface. Thus it can be easly registered to any IOC contianer of your liking.

`IConfigBuilder.Build` is the entry point to get all config files, the `Build` method accept two SimpleConfig classes:

[1. AssemblyCollection]()

[2. ConfigOptions]()

````C#
var collection = new AssemblyCollection()
				.AddFullAssemblyHolder(this.GetType().GetTypeInfo().Assembly);

var configCollection = new ConfigBuilder().Build(collection, new ConfigOptions());
````

the result is a new class [ConfigCollection]() where you can  iterate all of the implemintations of you config interfaces.

## First Config Interface

As written in the introduction SimpleConfig uses interfaces to pass values into services, thus we need to create our first interface.

````C#
[ConfigSection]
public interface IEmailSenderConfig

	[DefaultValue("SomeUrl")]
	string EmailServiceUrl { get; set; }

	[DefaultValue(3)]
	int Retries { get; set; }
}
````

When `ConfigBuilder.Build` invoked, it will search all indication of a config interfaces and use `Emit` to create a concreate class at run time. (Unforunatly Roslyn was not fast enough). The `DefaultValue` Attribute will set the value into the property.

Now as the builder returns the `ConfigCollection` we can explicitly request the interface like so

````C#
IEmailSenderConfig config = configCollection.GetConfig<IEmailSenderConfig>();
````
and get the values we used as deafults or simply iterate over the items like so

````C#
foreach (var configItem in configCollection)
{
	Type interfaceType = configItem.Key;
	object configImplemintation = configItem.Value;
}
````

SimpleConfig is highly extendable and we will explain how to work with it on the next [page](https://github.com/existall/SimpleConfig/blob/master/docs/building_the_collection.md)
