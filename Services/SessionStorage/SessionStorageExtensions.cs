using System;
using Forge.Wasm.BrowserStorages.Serialization.DefaultSerializer;
using Forge.Wasm.BrowserStorages.Serialization.DefaultSerializer.SessionStorage;
using Forge.Wasm.BrowserStorages.Serialization.SessionStorage;
using Forge.Wasm.BrowserStorages.Storage.SessionStorage;
using Microsoft.Extensions.DependencyInjection;

namespace Forge.Wasm.BrowserStorages.Services.SessionStorage
{

    /// <summary>Extension methods for IServiceCollection</summary>
    public static class SessionStorageExtensions
    {

        /// <summary>
        /// Registers the Forge SessionStorage services as scoped.
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeSessionStorage(this IServiceCollection services)
            => services.AddForgeSessionStorage(null);

        /// <summary>
        /// Registers the Forge SessionStorage services as scoped.
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeSessionStorage(this IServiceCollection services, Action<SessionStorageOptions> configure)
        {
            return services
                .AddScoped<ISerializationProvider, JsonSerializer>()
                .AddScoped<ISessionStorageProviderSync, SessionStorageProviderSync>()
                .AddScoped<ISessionStorageProviderAsync, SessionStorageProviderAsync>()
                .AddScoped<ISessionStorageServiceSync, SessionStorageServiceSync>()
                .AddScoped<ISessionStorageServiceAsync, SessionStorageServiceAsync>()
                .Configure<SessionStorageOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    configureOptions.SerializeOptions.Converters.Add(new TimespanJsonConverter());
                    configureOptions.DeserializeOptions.Converters.Add(new TimespanJsonConverter());
                });
        }

        /// <summary>
        /// Registers the Forge SessionStorage services as singletons. This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeSessionStorageAsSingleton(this IServiceCollection services)
            => services.AddForgeSessionStorageAsSingleton(null);

        /// <summary>
        /// Registers the Forge SessionStorage services as singletons. This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeSessionStorageAsSingleton(this IServiceCollection services, Action<SessionStorageOptions> configure)
        {
            return services
                .AddSingleton<ISerializationProvider, JsonSerializer>()
                .AddSingleton<ISessionStorageProviderSync, SessionStorageProviderSync>()
                .AddSingleton<ISessionStorageProviderAsync, SessionStorageProviderAsync>()
                .AddSingleton<ISessionStorageServiceSync, SessionStorageServiceSync>()
                .AddSingleton<ISessionStorageServiceAsync, SessionStorageServiceAsync>()
                .Configure<SessionStorageOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    configureOptions.SerializeOptions.Converters.Add(new TimespanJsonConverter());
                    configureOptions.DeserializeOptions.Converters.Add(new TimespanJsonConverter());
                });
        }

        /// <summary>
        /// Registers the Forge SessionStorage services as scoped. Only the async providers will be registered.
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeSessionStorageAsyncOnly(this IServiceCollection services)
            => services.AddForgeSessionStorageAsyncOnly(null);

        /// <summary>
        /// Registers the Forge SessionStorage services as scoped.
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeSessionStorageAsyncOnly(this IServiceCollection services, Action<SessionStorageOptions> configure)
        {
            return services
                .AddScoped<ISerializationProvider, JsonSerializer>()
                .AddScoped<ISessionStorageProviderAsync, SessionStorageProviderAsync>()
                .AddScoped<ISessionStorageServiceAsync, SessionStorageServiceAsync>()
                .Configure<SessionStorageOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    configureOptions.SerializeOptions.Converters.Add(new TimespanJsonConverter());
                    configureOptions.DeserializeOptions.Converters.Add(new TimespanJsonConverter());
                });
        }

        /// <summary>
        /// Registers the Forge SessionStorage services as singletons. Only the async providers will be registered. 
        /// This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeSessionStorageAsyncOnlyAsSingleton(this IServiceCollection services)
            => services.AddForgeSessionStorageAsyncOnlyAsSingleton(null);

        /// <summary>
        /// Registers the Forge SessionStorage services as singletons. Only the async providers will be registered.
        /// This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeSessionStorageAsyncOnlyAsSingleton(this IServiceCollection services, Action<SessionStorageOptions> configure)
        {
            return services
                .AddSingleton<ISerializationProvider, JsonSerializer>()
                .AddSingleton<ISessionStorageProviderAsync, SessionStorageProviderAsync>()
                .AddSingleton<ISessionStorageServiceAsync, SessionStorageServiceAsync>()
                .Configure<SessionStorageOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    configureOptions.SerializeOptions.Converters.Add(new TimespanJsonConverter());
                    configureOptions.DeserializeOptions.Converters.Add(new TimespanJsonConverter());
                });
        }

    }

}
