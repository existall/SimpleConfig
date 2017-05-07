
Why not IOptions<>
----------------------------------------------

With the release of Asp.Net Core and .Net Core Microsoft has introduced `IOptions<>`.
While `IOptions<>` is a great concept it lacks in implementation.
`IOptions<>` provide a way to insert parameters into your app dynamically.

For example you have an email service that requires some URL to the email server.

```csharp
public class EmailSender
{
    public EmailSender(string emailServiceUrl, ... )
    {

    }
}
```

All of the good IOC containers out there will tell you that injecting a string into a service is a bad idea.
The best of them won't let you do it.

`IOptions<>` to the rescue, with `IOptions<>` you can request the option class that can provide the string you want provided from any data store you want (json file, database and so on).

```csharp
    public class EmailSender
    {
        public EmailSender(IOption<EmailProviderConfiguratation> configuration, ... )
        {

        }
    }
```

BUT `IOptions<>` is not the way to do this.

Why you ask ?

1.  As Uncle Bob said, your application must be independent from frameworks this the application code takes an unnecessary dependency on a framework abstraction, this is a violation of DIP.
2.  In order to inject `IOption<SomeClass>` `SomeClass` have to be a concrete class and not an interface.
3.  To use `IOption<SomeClass>` you must call `services.Configure<SomeClass>` in the `Setup` class, this is not scale-able in any way and the last thing we want to do is to manually configure each configuration class.
4.  Registering `IOption<>` in any other DI container different from Microsoft new DI container won't be a ball park.

For better understanding you can read this [explenation](http://https://simpleinjector.readthedocs.io/en/latest/aspnetintegration.html#working-with-ioption-t) from the SimpleInjecor docs.
