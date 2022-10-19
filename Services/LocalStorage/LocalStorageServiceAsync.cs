using Forge.Wasm.BrowserStorages.Serialization.LocalStorage;
using Forge.Wasm.BrowserStorages.Services.Abstraction;
using Forge.Wasm.BrowserStorages.Storage.LocalStorage;
using Microsoft.Extensions.Logging;

namespace Forge.Wasm.BrowserStorages.Services.LocalStorage
{

    /// <summary>LocalStorage service with asynhronous methods</summary>
    public class LocalStorageServiceAsync : StorageServiceBaseAsync, ILocalStorageServiceAsync
    {

        /// <summary>Initializes a new instance of the <see cref="LocalStorageServiceAsync" /> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="storageProvider">The storage provider.</param>
        /// <param name="serializationProvider">The serialization provider.</param>
        public LocalStorageServiceAsync(ILogger<LocalStorageServiceAsync> logger,
            ILocalStorageProviderAsync storageProvider,
            ISerializationProvider serializationProvider) : 
            base(logger, storageProvider, serializationProvider)
        {
            logger.LogDebug($"{this.GetType().Name}.ctor, ILocalStorageProviderAsync, hash: {storageProvider.GetHashCode()}");
            logger.LogDebug($"{this.GetType().Name}.ctor, ISerializationProvider, hash: {serializationProvider.GetHashCode()}");
        }

    }

}
