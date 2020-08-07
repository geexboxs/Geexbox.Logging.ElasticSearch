# Geexbox.Logging

[![NuGet Elasticsearch](https://img.shields.io/nuget/v/Geexbox.Logging.ElasticSearch.svg)](https://www.nuget.org/packages/Geexbox.Logging.ElasticSearch/)

## Geexbox.Logging.ElasticSearch

A rolling file provider for ASP.NET Core 2.1 [Microsoft.Extensions.Logging](https://www.nuget.org/packages/Microsoft.Extensions.Logging), the logging subsystem used by ASP.NET Core. Writes logs to a set of text files, one per day.

### Getting Started 

**First** Install the [Geexbox.Logging.ElasticSearch](https://nuget.org/packages/Geexbox.Logging.ElasticSearch) package from NuGet, either using powershell:

```powershell
Install-Package Geexbox.Logging.ElasticSearch
```

or using the .NET CLI:

```powershell
dotnet add package Geexbox.Logging.ElasticSearch
```

**Next** configure the provider by calling `AddElasticsearch()` on an `ILoggingBuilder` during logger configuration in _Program.cs_.

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(builder => builder.AddElasticsearch()) // <- Add this line
            .UseStartup<Startup>()
            .Build();
}
```

It will read `appsettings.json` for configurations
```jsonc
{
  //...
  "Logging": {
    "Elasticsearch": {
      "LogLevel": {
        "Default": "Information"
      },
      // other options goes here...
    },
  },
  //...
}
```

You can pass additional options to the Add File by passing an `Action<FileLoggerOptions>`, for example:

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(builder => builder.AddElasticsearch(options => {
                // change some options here
            })) 
            .UseStartup<Startup>()
            .Build();
}
```

**Finally** The provider will write to elasticsearch engine with named index `logstash` by default.

Logs will look something like the following:

## Credits

This provider is _heavily_ cribbed from the [Azure App Service Logging Provider](https://github.com/aspnet/logging/blob/dev/src/Microsoft.Extensions.Logging.AzureAppServices/Internal/BatchingLoggerProvider.cs) from the ASP.NET team.
