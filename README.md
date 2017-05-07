ExistAll.SimpleConfig
=====================

A config framework suitable for IOC.

## Installation

```
    $ Install-Package ExistAll.SimpleConfig
```

## Usage

### An interface declaring configuration keys
```csharp
    public interface IEmailSenderConfig
    {
        [DefaultValue("smtp.demo.com")]
        string SmtpHost { get; set; }

        [DefaultValue(25)]
        int SmtpPort { get; set; }
    }
```

### A config needy service
```csharp
    public class EmailSender : IEmailSender
    {
      private readonly _config;

      public EmailSender(IEmailSenderConfig emailSenderConfig)
      {
        _config = emailSenderConfig;
      }

      public void SendEmail()
      {
        var host = _config.SmtpHost;
        var port = _config.SmtpPort;

        // ...
      }
    }
```

Configuration can be loaded from a DB, consul, JSON files, or whatever.

## Documentation
- [Getting started](docs/getting-started.md)  
- [Building the collection](docs/build-a-collection.md)  
- [Building Config Interfaces](docs/build-a-config-interface.md)  
- [DefaultValue](docs/default-values.md)  
- [Build Section Binders](docs/build-a-section-binder.md)  
- [Extending SimpleConfig](docs/extend-simple-config.md)
- [Why not IOptions<>?](docs/ioptions.md)

## Contribute

PRs are most welcome.
