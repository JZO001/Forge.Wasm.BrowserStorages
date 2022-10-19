using Forge.Wasm.BrowserStorages.Storage.Abstraction;
using Microsoft.JSInterop;

namespace Forge.Wasm.BrowserStorages.Storage.SessionStorage
{

    /// <summary>Asynchronous session storage provider</summary>
    public class SessionStorageProviderAsync : StorageProviderAsyncBase, ISessionStorageProviderAsync
    {

        /// <summary>Initializes a new instance of the <see cref="SessionStorageProviderAsync" /> class.</summary>
        /// <param name="jSRuntime">The JSRuntime.</param>
        /// <exception cref="System.ArgumentNullException">jSRuntime</exception>
        public SessionStorageProviderAsync(IJSRuntime jSRuntime) : base(jSRuntime, "sessionStorage")
        {
        }

    }

}
