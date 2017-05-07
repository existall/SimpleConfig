ExistAll.SimpleConfig
=====================

A config framework suitable for IOC.

## Installation
`Install-Package ExistAll.SimpleConfig`

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
- [Getting started](docs/getting_started.md)  
- [Building the collection](docs/building_the_collection.md)  
- [Building Config Interfaces](docs/Build%20Config%20Interface.md)  
- [DefaultValue](docs/Default%20Values.md)  
- [Build Section Binders](docs/Build%20a%20SectionBinder.md)  
- [Extending SimpleConfig](docs/Extend%20Simple%20Config.md)
- [Why not IOptions<>?](docs/IOptions.md)

## Contribute

PRs are most welcome.
