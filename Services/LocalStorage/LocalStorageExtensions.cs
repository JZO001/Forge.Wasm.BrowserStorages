using System;
using Forge.Wasm.BrowserStorages.Serialization.DefaultSerializer;
using Forge.Wasm.BrowserStorages.Serialization.DefaultSerializer.LocalStorage;
using Forge.Wasm.BrowserStorages.Serialization.LocalStorage;
using Forge.Wasm.BrowserStorages.Storage.LocalStorage;
using Microsoft.Extensions.DependencyInjection;

namespace Forge.Wasm.BrowserStorages.Services.LocalStorage
{

    /// <summary>Extension methods for IServiceCollection</summary>
    public static class LocalStorageExtensions
    {

        /// <summary>
        /// Registers the Forge LocalStorage services as scoped.
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeLocalStorage(this IServiceCollection services)
            => services.AddForgeLocalStorage(null);

        /// <summary>
        /// Registers the Forge LocalStorage services as scoped.
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeLocalStorage(this IServiceCollection services, Action<LocalStorageOptions> configure)
        {
            return services
                .AddScoped<ISerializationProvider, JsonSerializer>()
                .AddScoped<ILocalStorageProviderSync, LocalStorageProviderSync>()
                .AddScoped<ILocalStorageProviderAsync, LocalStorageProviderAsync>()
                .AddScoped<ILocalStorageServiceSync, LocalStorageServiceSync>()
                .AddScoped<ILocalStorageServiceAsync, LocalStorageServiceAsync>()
                .Configure<LocalStorageOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    configureOptions.SerializeOptions.Converters.Add(new TimespanJsonConverter());
                    configureOptions.DeserializeOptions.Converters.Add(new TimespanJsonConverter());
                });
        }

        /// <summary>
        /// Registers the Forge LocalStorage services as singletons. This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeLocalStorageAsSingleton(this IServiceCollection services)
            => services.AddForgeLocalStorageAsSingleton(null);

        /// <summary>
        /// Registers the Forge LocalStorage services as singletons. This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeLocalStorageAsSingleton(this IServiceCollection services, Action<LocalStorageOptions> configure)
        {
            return services
                .AddSingleton<ISerializationProvider, JsonSerializer>()
                .AddSingleton<ILocalStorageProviderSync, LocalStorageProviderSync>()
                .AddSingleton<ILocalStorageProviderAsync, LocalStorageProviderAsync>()
                .AddSingleton<ILocalStorageServiceSync, LocalStorageServiceSync>()
                .AddSingleton<ILocalStorageServiceAsync, LocalStorageServiceAsync>()
                .Configure<LocalStorageOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    configureOptions.SerializeOptions.Converters.Add(new TimespanJsonConverter());
                    configureOptions.DeserializeOptions.Converters.Add(new TimespanJsonConverter());
                });
        }

    }

}
