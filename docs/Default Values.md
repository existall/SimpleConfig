# Default Values

Default values allow you to instantiate properties with default values on run time, if no value has been set by the binders SimpleConfig will check to see if there is a default value attribute with value and set it to the property.

### Disclaimer: at this time your application is coupled with the DefaultValue attribute, yet SimpleConfig will check for inheritance thus you can inherit from `DefaultValueAttribute` and decouple your application this way.

### Supported Types and values 
DefaultValue allows you to pass one or more values, for example;

````C#

[DefaultValue("Moby Dick")]
public string Name { get; set; }

[DefaultValue(1,2,3,8)]
pubic int[] Ids { get; set; }
````

SimpleConfig checks what is the return property type and try convert it into the correct value.

DefaultValue also supports : `Enum`, `DateTime`, `Uri`, `Array` and `Nullable`


Since many developers use different `DateTime` formats, SimpleConfig allows you to change the Parser format. The default parser format is `"yyyy-MM-dd` yet you can change it by placing a different format in `ConfigOptions.DateTimeFormat`.

````C#
[DefaultValue("2012-09-19")]
pubic DateTime StartTime { get; set; }
````

In the next page we will discuss how to create [SectionBinder]()