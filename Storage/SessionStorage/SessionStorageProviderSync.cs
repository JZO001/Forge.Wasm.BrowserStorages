using Forge.Wasm.BrowserStorages.Storage.Abstraction;
using Microsoft.JSInterop;

namespace Forge.Wasm.BrowserStorages.Storage.SessionStorage
{

    /// <summary>Synchronous session storage provider</summary>
    public class SessionStorageProviderSync : StorageProviderSyncBase, ISessionStorageProviderSync
    {

        /// <summary>Initializes a new instance of the <see cref="SessionStorageProviderSync" /> class.</summary>
        /// <param name="jsInProcessRuntime">The IJSInProcessRuntime.</param>
        /// <exception cref="System.ArgumentNullException">jSRuntime</exception>
        /// <exception cref="System.InvalidOperationException">
        /// Unable to cast {typeof(IJSRuntime).Name} to {typeof(IJSInProcessRuntime).Name}. The provided object is not correct. Please make sure the {typeof(IJSInProcessRuntime).Name} is available in your project type.
        /// </exception>
        public SessionStorageProviderSync(IJSInProcessRuntime jsInProcessRuntime) : base(jsInProcessRuntime, "sessionStorage")
        {
        }

    }

}
