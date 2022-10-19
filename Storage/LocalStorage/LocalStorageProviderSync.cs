using Forge.Wasm.BrowserStorages.Storage.Abstraction;
using Microsoft.JSInterop;

namespace Forge.Wasm.BrowserStorages.Storage.LocalStorage
{

    /// <summary>Synchronous local storage provider</summary>
    public class LocalStorageProviderSync : StorageProviderSyncBase, ILocalStorageProviderSync
    {

        /// <summary>Initializes a new instance of the <see cref="LocalStorageProviderSync" /> class.</summary>
        /// <param name="jsInProcessRuntime">The IJSInProcessRuntime.</param>
        /// <exception cref="System.ArgumentNullException">jSRuntime</exception>
        /// <exception cref="System.InvalidOperationException">
        /// Unable to cast {typeof(IJSRuntime).Name} to {typeof(IJSInProcessRuntime).Name}. The provided object is not correct. Please make sure the {typeof(IJSInProcessRuntime).Name} is available in your project type.
        /// </exception>
        public LocalStorageProviderSync(IJSInProcessRuntime jsInProcessRuntime) : base(jsInProcessRuntime, "localStorage")
        {
        }

    }

}
