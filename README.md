# ExistAll.SimpleConfig

## Introduction - or why SimpleConfig was created

With the release of Asp.Net Core and .Net Core Microsoft has introduced ````IOptions<>````.
While ````IOptions<>```` is a great concept it lacks in implemintation.
````IOptions<>````  provide a way to insert parameters into your app dynamicly.

For example you have an email service that requires some url to the email server.

```` 
public class EmailSender
{
    public EmailSender(string emailServiceUrl, ... )
    {

    }
}
````

All of the good Ioc containers out there will tell you that injecting a string into a service is a bad idea.
The best of them won't let you do it.

````IOptions<>```` to the resque, with ````IOptions<>```` you can request the option class that can provide the string you want provided from any data store you want (json file, database and so on).

```` 
public class EmailSender
{
    public EmailSender(IOption<EmailProviderConfiguratation> configuration, ... )
    {

    }
}
````

BUT ````IOptions<>```` is not the way to do this. 

Why you ask ?
1. As Uncle Bob said, your application must be independent from frameworks this the application code takes an unnecessary dependency on a framework abstraction, this is a violation of DIP.
2. In order to inject ````IOption<SomeClass>```` ````SomeClass```` have to be a concreate class and not an interface.
3. To use ````IOption<SomeClass>```` you must call ```` services.Configure<SomeClass> ```` in the ````Setup```` class, this is not scaleable in any way and the last thing we want to do is to manually configure each configuration class.
4. Registering ````IOption<>```` in any other DI container different from Microsoft new DI container won't be a ball park.

For better understanding you can read this [explenation](http://https://simpleinjector.readthedocs.io/en/latest/aspnetintegration.html#working-with-ioption-t) from the SimpleInjecor docs.

 
### TL;DR - or what SimpleConfig does?

Remember the ````IOption<EmailProviderConfiguratation> configuration````? 

what if we could build an interface like so 

````
public interface IEmailServiceConfig
{
    [DefaultValue("SomeUrl")]
    string ServiceUrl {get; set;}
}
````

and use it like so 

````
public class EmailSender : IEmailSender
{
    public EmailSender(IEmailServiceConfig emailServiceConfig, ... )
    {

    }

    public void SendEmail(...)
    {
        Send(emailServiceConfig.ServiceUrl, ...);
    }
}
````