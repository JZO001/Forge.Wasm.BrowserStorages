using Forge.Wasm.BrowserStorages.Storage.Abstraction;
using Microsoft.JSInterop;

namespace Forge.Wasm.BrowserStorages.Storage.LocalStorage
{

    /// <summary>Synchronous local storage provider</summary>
    public class LocalStorageProviderAsync : StorageProviderAsyncBase, ILocalStorageProviderAsync
    {

        /// <summary>Initializes a new instance of the <see cref="LocalStorageProviderSync" /> class.</summary>
        /// <param name="jSRuntime">The JSRuntime.</param>
        /// <exception cref="System.ArgumentNullException">jSRuntime</exception>
        public LocalStorageProviderAsync(IJSRuntime jSRuntime) : base(jSRuntime, "localStorage")
        {
        }

    }

}
