# TigrSettings

Simple yet extensible .NET Standard 2.0 library for mapping settings to strong types.

## Getting Started

**TigrSettings** supports 3 different builders: `PocoSettingsBuilder<T>`, `StaticSettingsBuilder` and `DynamicSettingsBuilder<T>` (within separate `TigrSettings.Dynamic` package).

### POCO builder
Lets say we have following `App.config`:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <appSettings>
        <add key="Int" value="5" />
        <add key="String" value="foobar" />
        <add key="Guid" value="9d50f0fb-4127-4433-b062-7ecf211a2adb" />
        <add key="Enum" value="Foo|Baz" />  <!-- Note that [Flags] are supported -->
        <add key="DateTime" value="2018-04-21T12:34:56.789Z" />
        <add key="ArrayOfTimeSpans" value="00:00:05, 00:00:10, 00:00:15" />
        <add key="NullableBoolean" value="null" />
    </appSettings>
</configuration>
```

And a POCO settings class:
```csharp
public class AppSettings
{
    public int Int { get; set; }
    public string String { get; set; }
    public Guid Guid { get; set; }
    public EnumSetting Enum { get; set; }
    public DateTime DateTime { get; set; }
    public TimeSpan[] ArrayOfTimeSpans { get; set; }
    public bool? NullableBoolean { get; set; }
}

[Flags]
public enum EnumSetting
{
    Foo = 1,
    Bar = 2,
    Baz = 4
}
```

Usage:
```csharp
ISettingsProvider settingsProvider = new AppSettingsProvider(); 

var pocoBuilder = new PocoSettingsBuilder<AppSettings>(settingsProvider);
AppSettings appSettings = pocoBuilder.Create();
```

**Note.** `AppSettingsProvider` (which provides settings from `App.config` / `Web.config`) is located in separate `TigrSettings.Providers.ConfigurationManager` package.

### Static builder

Static settings class:
```csharp
public static class AppSettings
{
    public static int Int { get; set; }
    public static string String { get; set; }
    public static Guid Guid { get; set; }
    public static EnumSetting Enum { get; set; }
    public static DateTime DateTime { get; set; }
    public static TimeSpan[] ArrayOfTimeSpans { get; set; }
    public static bool? NullableBoolean { get; set; }
}
```

Usage:
```csharp
ISettingsProvider settingsProvider = new AppSettingsProvider(); 

var staticBuilder = new StaticSettingsBuilder(settingsProvider);
staticBuilder.MapTo(typeof(AppSettings));
```
### Dynamic builder

Dynamic builder allows to create dynamic settings object that acts like specified interface. This may be useful for DI and/or mocking.

Interface:
```csharp
public interface IAppSettings
{
    int Int { get; }
    string String { get; }
    Guid Guid { get; }
    EnumSetting Enum { get; }
    DateTime DateTime { get; }
    TimeSpan[] ArrayOfTimeSpans { get; }
    bool? NullableBoolean { get; }
}
```

Usage:
```csharp
ISettingsProvider settingsProvider = new AppSettingsProvider(); 

var dynamicBuilder = new DynamicSettingsBuilder<IAppSettings>(settingsProvider);
IAppSettings appSettings = dynamicBuilder.Create();
```

**Note.** `DynamicSettingsBuilder<T>` located in separate `TigrSettings.Dynamic` package.


### Converters
**TigrSettings** comes with set of default converters,  but if they are not enough, you can implement either `ISettingsProvider` interface or `SettingValueConverterBase<TValue>` class and pass additional converters to settings builder constructor:

```csharp
ISettingsProvider myConverter = new MySettingValueConverter();
ISettingsProvider myAnotherConverter = new MyAnotherSettingValueConverter();
var builder = new PocoSettingsBuilder<AppSettings>(settingsProvider, myConverter, myAnotherConverter);
```
Custom converter of some type has higher priority than default one, so if some default converter provides insufficient functionality - it can be overwritten.

**Special case:** Built-in `NullableConverter` and `ArrayConverter` will automatically support new custom converters. For example, if custom converter provides conversion behavior for `MyType`, you don't need to create separate `MyType?` or `MyType[]` (or even `MyType?[]`) converters.


## Installing
Core library:
```
Install-Package TigrSettings
```

Dynamic support:

```
Install-Package TigrSettings.Dynamic
```

`ConfigurationManager` provider:

```
Install-Package TigrSettings.Providers.ConfigurationManager
```

Environment variables provider:
```
Install-Package TigrSettings.Providers.EnvironmentVariables
```


## Built With

* [ImpromptuInterface](https://github.com/ekonbenefits/impromptu-interface) - for `TigrSettings.Dynamic` implementation


## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
