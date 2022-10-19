using Forge.Wasm.BrowserStorages.Serialization.LocalStorage;
using Forge.Wasm.BrowserStorages.Services.Abstraction;
using Forge.Wasm.BrowserStorages.Storage.LocalStorage;
using Microsoft.Extensions.Logging;
using System;

namespace Forge.Wasm.BrowserStorages.Services.LocalStorage
{

    /// <summary>LocalStorage service with synhronous methods</summary>
    public class LocalStorageServiceSync : StorageServiceBaseSync, ILocalStorageServiceSync
    {

        /// <summary>Initializes a new instance of the <see cref="LocalStorageServiceSync" /> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="storageProvider">The storage provider.</param>
        /// <param name="serializationProvider">The serialization provider.</param>
        /// <exception cref="ArgumentNullException">storageProvider
        /// or
        /// serializationProvider</exception>
        public LocalStorageServiceSync(ILogger<LocalStorageServiceSync> logger,
            ILocalStorageProviderSync storageProvider,
            ISerializationProvider serializationProvider) : 
            base(logger, storageProvider, serializationProvider)
        {
            logger.LogDebug($"{this.GetType().Name}.ctor, ILocalStorageProviderSync, hash: {storageProvider.GetHashCode()}");
            logger.LogDebug($"{this.GetType().Name}.ctor, ISerializationProvider, hash: {serializationProvider.GetHashCode()}");
        }

    }

}
