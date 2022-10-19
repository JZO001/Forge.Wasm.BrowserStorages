using Forge.Wasm.BrowserStorages.Serialization.SessionStorage;
using Forge.Wasm.BrowserStorages.Services.Abstraction;
using Forge.Wasm.BrowserStorages.Storage.SessionStorage;
using Microsoft.Extensions.Logging;
using System;

namespace Forge.Wasm.BrowserStorages.Services.SessionStorage
{

    /// <summary>SessionStorage service with synhronous methods</summary>
    public class SessionStorageServiceSync : StorageServiceBaseSync, ISessionStorageServiceSync
    {

        /// <summary>Initializes a new instance of the <see cref="SessionStorageServiceSync" /> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="storageProvider">The storage provider.</param>
        /// <param name="serializationProvider">The serialization provider.</param>
        /// <exception cref="ArgumentNullException">storageProvider
        /// or
        /// serializationProvider</exception>
        public SessionStorageServiceSync(ILogger<SessionStorageServiceSync> logger,
            ISessionStorageProviderSync storageProvider,
            ISerializationProvider serializationProvider) : 
            base(logger, storageProvider, serializationProvider)
        {
            logger.LogDebug($"{this.GetType().Name}.ctor, ISessionStorageProviderSync, hash: {storageProvider.GetHashCode()}");
            logger.LogDebug($"{this.GetType().Name}.ctor, ISerializationProvider, hash: {serializationProvider.GetHashCode()}");
        }

    }

}
