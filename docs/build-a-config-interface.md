# Build Config Interface

To Create a Config Interface you only need to build an interface with the property you need like so.

````C#
public interface ISomeInterface
{
    string SomeProperty { get; set; }
    
    [DefaultValue(1,2,3,4,5)]
    int[] ArrayOfIds { get; set; }
}

````

Now we need to mark the interface that this is a Config Interface, SimpleConfig let you do this in three ways:  
1. An Interface.
````C#
public interface ISomeInterface : IConfigSection
{
    string SomeProperty { get; set; }
    
    [DefaultValue(1,2,3,4,5)]
    int[] ArrayOfIds { get; set; }
}

`````
2. An Attribute.
````C#
[ConfigSection]
public interface ISomeInterface
{
    string SomeProperty { get; set; }
    
    [DefaultValue(1,2,3,4,5)]
    int[] ArrayOfIds { get; set; }
}
````

3. Interface name Suffix.  
By default SimpleConfig scan for interfaces with name ending with Config and adds it to the ConfigCollection. ( The suffix option can be change using `ConfigOptions` to know more look at the [Extend section]())

````C#
[ConfigSection]
public interface ISomeInterfaceConfig
{
    string SomeProperty { get; set; }
    
    [DefaultValue(1,2,3,4,5)]
    int[] ArrayOfIds { get; set; }
}
````

## SimpleConfig is built to make your application independent Frameworks so every indication of config interface can be changed using `ConfigOptions`

In the next page we will explain about [DefaultValue Attribute](https://github.com/existall/SimpleConfig/blob/master/docs/Default%20Values.md)