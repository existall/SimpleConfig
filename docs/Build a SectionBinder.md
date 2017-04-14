# Build a SectionBinder
SectionBinders are SimpleConfig way to pass values into properties not only from `DefaultValue` but from other data sources.

Out of the box SimpleConfig provides two Binders:  
1. InMemoryCollection
2. ConfigurationBinder - which is a binder that extracts values from .Net Core `IConfiguration`.


### Building a SectionProvider

SectionProvider is an implementation of `ISectionBinder` interface.

SimpleConfig invokes the next method

`bool TryGetValue(ConfigBindingContext bindingContext, out string value);`

- SimpleConfig ask you to tell him rather or not you successfully get the value from the data source by returning a true or false result.
- The Binder `TryGetValue` has an `out` parameter of `string` which SimpleConfig will try to convert and set into the property.
- TryGetValue provide a context with the current Section, Current Key and the current Value up till now.


### Section 
The section name the context provides is the name of the interface with some manipulation, for example the interface `ISomeInterface` will be provided by the context as `SomeInterface`. SimpleConfig removes the `I`.  
#### this is configurable via `ConfigOptions` and can be view on the [Extended section]()

### Key
The Key name the context provides is the property name as is, for example the interface `ISomeInterface.SomeProperty` will be provided as `SomeProperty`.

### Current Value

Since Order between the binders is important the context will provide the current value as `string` that the former binders has set already.

In the next page we will learn how to [extend]() SimpleConfig and future features.



