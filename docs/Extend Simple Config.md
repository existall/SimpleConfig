# Extend Simple Config
## ConfigOptions
Let's look at `ConfigOptions` which is the way you may change SimpleConfig default behavior.

````C#
public class ConfigOptions
{
		public Type AttributeType { get; set; } = typeof(ConfigSectionAttribute);
		public Type InterfaceBase { get; set; } = typeof(IConfigSection);
		public string ConfigSuffix { get; set; } = "Config";
		public string ArraySplitDelimiter { get; set; } = ",";
		public string DateTimeFormat { get; set; } = "yyyy-MM-dd";
		public Func<string, string> SectionNameFormater => (interfaceName) => interfaceName.Trim('I');
}
````

## Decoupling SimpleConfig
For good practice you always want your application to not be dependent on Frameworks such as SimpleConfig, except for the bootstrapping process (CompositionRoot) all of the data object, services and logic should be your own. This can be achieved using abstractions. SimpleConfig let you use your own types as indications of config interfaces.

### AttributeType
As mentioned in [Building the collection]() SimpleConfig scans all the assemblies provided in the `Build` method, any Types with `ConfigSectionAttribute` will be add to the ConfigCollection. By replacing the Type with your own, SimpleConfig will scan for your Attribute.

### InterfaceBase
Same as `AttributeType` SimpleConfig searches for `IConfigSection` interface as indication of config interface. By replacing the interface Type with your own, SimpleConfig will scan for your interface.

### ConfigSuffix
Same as `AttributeType` and `ConfigSectionAttribute` SimpleConfig searches for interfaces with names ending with the `Config` suffix, you can provide any suffix you want.

#### although SimpleConfig provides three ways as config interface indications that does not mean you have to use all three, simply choose one -> "KISS".

## Type Converter Options

### ArraySplitDelimiter 
SimpleConfig tries to convert the binders string value into the property type, when the type is an array SimpleConfig needs to know with what to split the values. By default SimpleConfig uses `','` but I love giving you the choice to decide for yourself.

### DateTimeFormat
When SimpleConfig tries to parse DateTime from string it uses this property as a format, you can change this to anything your application uses.

## Binders

### SectionNameFormater
SimpleConfig converts your config interface name into section name for the binders thus indicating and allowing you to know what to look for. As default SimpleConfig will trim the I from the interface name. If you like to replace this behavior simply provide a `Func<string,string>` of your own to do so.

# Future Features

I would like to continue and develop SimpleConfig as people will use it more and more thus helping and creating feature requests.

Few ideas for future features are:  
1. Diagnostics and reporting.
2. Roslyn support for those who will want to use it. (I did some performance tests with Roslyn and it was not good).
3. AOP.
