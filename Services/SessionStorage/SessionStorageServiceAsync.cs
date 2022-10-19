using Forge.Wasm.BrowserStorages.Serialization.SessionStorage;
using Forge.Wasm.BrowserStorages.Services.Abstraction;
using Forge.Wasm.BrowserStorages.Storage.SessionStorage;
using Microsoft.Extensions.Logging;

namespace Forge.Wasm.BrowserStorages.Services.SessionStorage
{

    /// <summary>SessionStorage service with asynhronous methods</summary>
    public class SessionStorageServiceAsync : StorageServiceBaseAsync, ISessionStorageServiceAsync
    {

        /// <summary>Initializes a new instance of the <see cref="SessionStorageServiceAsync" /> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="storageProvider">The storage provider.</param>
        /// <param name="serializationProvider">The serialization provider.</param>
        public SessionStorageServiceAsync(ILogger<SessionStorageServiceAsync> logger, 
            ISessionStorageProviderAsync storageProvider,
            ISerializationProvider serializationProvider) : 
            base(logger, storageProvider, serializationProvider)
        {
            logger.LogDebug($"{this.GetType().Name}.ctor, ISessionStorageProviderAsync, hash: {storageProvider.GetHashCode()}");
            logger.LogDebug($"{this.GetType().Name}.ctor, ISerializationProvider, hash: {serializationProvider.GetHashCode()}");
        }

    }

}
