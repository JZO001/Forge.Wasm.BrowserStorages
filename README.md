# Forge.Wasm.BrowserStorages
Forge.Wasm.BrowserStorages is a library that provides access to the browsers local and session storage APIs for WASM applications.


## Installing

To install the package add the following line to you csproj file replacing x.x.x with the latest version number:

```
<PackageReference Include="Forge.Wasm.BrowserStorages" Version="x.x.x" />
```

You can also install via the .NET CLI with the following command:

```
dotnet add package Forge.Wasm.BrowserStorages
```

If you're using Visual Studio you can also install via the built in NuGet package manager.

## Setup

You will need to register the local storage services with the service collection in your _Startup.cs_ file in Blazor Server.

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddForgeLocalStorage();
    services.AddForgeSessionStorage();
}
``` 

Or in your _Program.cs_ file in Blazor WebAssembly.

```c#
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("app");

    builder.Services.AddForgeLocalStorage();
    builder.Services.AddForgeSessionStorage();

    await builder.Build().RunAsync();
}
```

In the case, if IJSInProcessRuntime does not available in your project type, you can not use the syncronized version of the storage.
It is possible to skip the registration of the sync providers and services which are requires the implementation of this interface,
use the following code to archieve it:

```c#
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("app");

    builder.Services.AddForgeLocalStorageAsyncOnly();
    builder.Services.AddForgeSessionStorageAsyncOnly();

    await builder.Build().RunAsync();
}
```


### Registering services as Singleton
If you would like to register LocalStorage or SessionStorage services as singletons, it is possible by using the following method:

```csharp
builder.Services.AddForgeLocalStorageAsSingleton();
builder.Services.AddForgeSessionStorageAsSingleton();
```

Or

```csharp
builder.Services.AddForgeLocalStorageAsyncOnlyAsSingleton();
builder.Services.AddForgeSessionStorageAsyncOnlyAsSingleton();
```

This method is not recommended in the most cases, try to avoid using it.


## Usage
To use Forge.Wasm.BrowserStorages in Blazor WebAssembly, inject the `ILocalStorageServiceAsync` | `ISessionStorageServiceAsync` per the example below.

```c#
@inject Forge.Wasm.BrowserStorages.Services.LocalStorage.ILocalStorageServiceAsync localStorage
@inject Forge.Wasm.BrowserStorages.Services.SessionStorage.ISessionStorageServiceAsync sessionStorage

@code {

    protected override async Task OnInitializedAsync()
    {
        await localStorage.SetAsync("Username", "johndoe");
        var usernameFromLocalStorage = await localStorage.GetAsync<string>("Username");

        await sessionStorage.SetAsync("Username", "johndoe");
        var usernameFromSessionStorage = await sessionStorage.GetAsync<string>("Username");
    }

}
```

It is possible to use synchonous version of API. Change the `ILocalStorageServiceAsync` for `ILocalStorageServiceSync` which allows you to avoid use of `async`/`await`.
Please keep it in mind, this is not the recommended way. Other issue can be, if the IJSInProcessRuntime is not available in your environment. 
If this is the case, you cannot use synchronous version of storage providers. 
It can be one of possible way to detect the synchronous mode support, if you try to case IJSRuntime to IJSInProcessRuntime. If it is possible, synchronous mode could be work.

```c#
@inject Forge.Wasm.BrowserStorages.Services.LocalStorage.ILocalStorageServiceAsync localStorage
@inject Forge.Wasm.BrowserStorages.Services.SessionStorage.ISessionStorageServiceAsync sessionStorage

@code {

    protected override void OnInitialized()
    {
        localStorage.Set("Username", "johndoe");
        var usernameFromLocalStorage = localStorage.Get<string>("Username");

        sessionStorage.Set("Username", "johndoe");
        var usernameFromSessionStorage = sessionStorage.Get<string>("Username");
    }

}
```

## Usage (Blazor Server)

**NOTE:** Due to pre-rendering in Blazor Server you can't perform any JS interop until the `OnAfterRender` lifecycle method.

```c#
@inject Forge.Wasm.BrowserStorages.Services.LocalStorage.ILocalStorageServiceSync localStorage
@inject Forge.Wasm.BrowserStorages.Services.SessionStorage.ISessionStorageServiceSync sessionStorage

@code {

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
	// localStorage example
        await localStorage.Set("Username", "john_doe");
        var usernameFromLocalStorage = await localStorage.Get<string>("Username");

	// sessionStorage example
        await sessionStorage.Set("Username", "john_doe");
        var usernameFromSessionStorage = await sessionStorage.Get<string>("Username");
    }

}
```

The following low-level API methods are exposed:

- asynchronous via `ILocalStorageServiceAsync` | `ISessionStorageServiceAsync`:
  - ContainsKeyAsync()
  - ClearAsync()
  - GetAsync()
  - GetAsStringAsync()
  - LengthAsync()
  - KeyAsync()
  - RemoveAsync()
  - SetAsync()
  - SetAsStringAsync()
  
- synchronous via `ILocalStorageServiceSync` | `ISessionStorageServiceSync`:
  - ContainsKey()
  - Clear()
  - Get()
  - GetAsString()
  - Length()
  - Key()
  - Remove()
  - Set()
  - SetAsString()

**Note:** LocalStorage \ SessionStorage methods will handle the serialisation and de-serialization of the data for you. Except the `SetAsString[Async]` and `GetAsString[Async]` methods which are save the string directly and return it as a string value from local \ session storage without using the serialization provider.

## Configuring default (built-in) JSON serialization provider options
You can configure the options for the default serialization provider (System.Text.Json) when calling the `AddForgeLocalStorage` or `AddForgeSessionStorage` method to register services.

```c#
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("app");

    // for localStorage
    builder.Services.AddForgeLocalStorage(config =>
	// these are the defaults, this code is for demonstration purposes
        config.SerializeOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        config.DeserializeOptions.PropertyNameCaseInsensitive = true;
    );

    // for sessionStorage
    builder.Services.AddForgeSessionStorage(config =>
	// these are the defaults, this code is for demonstration purposes
        config.SerializeOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        config.DeserializeOptions.PropertyNameCaseInsensitive = true;
    );

    await builder.Build().RunAsync();
}
```

## Using a custom JSON serialization provider
By default, the library uses `System.Text.Json`. If you would like to use an other JSON library as serialization provider, you can provide your own serialization provider which implements the `Forge.Wasm.BrowserStorages.Serialization.LocalStorage.ISerializationProvider` and/or `Forge.Wasm.BrowserStorages.Serialization.SessionStorage.ISerializationProvider` interface.

To register your own serializer in place of the default one, you can do the following:

```csharp
builder.Services.AddForgeLocalStorage();
builder.Services.Replace(ServiceDescriptor.Scoped<Forge.Wasm.BrowserStorages.Serialization.LocalStorage.ISerializationProvider, CustomSerializer>());
```

A Newtonsoft.Json serializer implementation for this library at Github created, called Forge.Wasm.BrowserStorages.NewtonSoft.Json

Please also check the following projects in my repositories:
- Forge.Yoda
- Forge.Security.Jwt.Service
- Forge.Security.Jwt.Service.Storage.SqlServer
- Forge.Security.Jwt.Client
- Forge.Security.Jwt.Client.Storage.Browser
- Forge.Wasm.BrowserStorages
- Forge.Wasm.BrowserStorages.NewtonSoft.Json
