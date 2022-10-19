using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Forge.Wasm.BrowserStorages.Storage.Abstraction
{

    /// <summary>Provides the base functionalities</summary>
    public abstract class StorageProviderAsyncBase : IStorageProviderAsync
    {

        private readonly IJSRuntime _jSRuntime;
        private readonly string _objectName;

        /// <summary>Initializes a new instance of the <see cref="StorageProviderAsyncBase" /> class.</summary>
        /// <param name="jSRuntime">The JSRuntime.</param>
        /// <param name="objectName">Name of the storage</param>
        /// <exception cref="ArgumentNullException">jSRuntime
        /// or
        /// objectName</exception>
        protected StorageProviderAsyncBase(IJSRuntime jSRuntime, string objectName)
        {
            if (jSRuntime == null) throw new ArgumentNullException(nameof(jSRuntime));
            if (string.IsNullOrWhiteSpace(objectName)) throw new ArgumentNullException(nameof(objectName));
            _jSRuntime = jSRuntime;
            _objectName = objectName;
        }

        /// <summary>Clears the content of the storage asynchroniously</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ValueTask
        /// </returns>
        public ValueTask ClearAsync(CancellationToken? cancellationToken = null)
            => _jSRuntime.InvokeVoidAsync($"{_objectName}.clear", cancellationToken ?? CancellationToken.None);

        /// <summary>Determines asynchroniously whether the specified key exists</summary>
        /// <param name="key">The key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   <c>true</c> if the specified key exists; otherwise, <c>false</c>.
        /// </returns>
        public ValueTask<bool> ContainsKeyAsync(string key, CancellationToken? cancellationToken = null)
            => _jSRuntime.InvokeAsync<bool>($"{_objectName}.hasOwnProperty", cancellationToken ?? CancellationToken.None, key);

        /// <summary>Gets the value asynchroniously based on the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The data in strong format
        /// </returns>
        public ValueTask<string> GetAsync(string key, CancellationToken? cancellationToken = null)
            => _jSRuntime.InvokeAsync<string>($"{_objectName}.getItem", cancellationToken ?? CancellationToken.None, key);

        /// <summary>Gets the key itself asynchroniously from the specified index.</summary>
        /// <param name="index">The index.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The key
        /// </returns>
        public ValueTask<string> KeyAsync(int index, CancellationToken? cancellationToken = null)
            => _jSRuntime.InvokeAsync<string>($"{_objectName}.key", cancellationToken ?? CancellationToken.None, index);

        /// <summary>Return the keys asynchroniously from the storage</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The keys
        /// </returns>
        public ValueTask<IEnumerable<string>> KeysAsync(CancellationToken? cancellationToken = null)
            => _jSRuntime.InvokeAsync<IEnumerable<string>>("eval", cancellationToken ?? CancellationToken.None, $"Object.keys({_objectName})");

        /// <summary>Gets the number of the existing entries asynchroniously in the storage</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The number of the entries
        /// </returns>
        public ValueTask<int> LengthAsync(CancellationToken? cancellationToken = null)
            => _jSRuntime.InvokeAsync<int>("eval", cancellationToken ?? CancellationToken.None, $"{_objectName}.length");

        /// <summary>Removes the entry asynchroniously based on the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ValueTask
        /// </returns>
        public ValueTask RemoveAsync(string key, CancellationToken? cancellationToken = null)
            => _jSRuntime.InvokeVoidAsync($"{_objectName}.removeItem", cancellationToken ?? CancellationToken.None, key);

        /// <summary>Removes the entries asynchroniously based on the specified keys.</summary>
        /// <param name="keys">The keys.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ValueTask
        /// </returns>
        public ValueTask RemoveAsync(IEnumerable<string> keys, CancellationToken? cancellationToken = null)
        {
            if (keys != null)
            {
                foreach (var key in keys)
                {
                    _jSRuntime.InvokeVoidAsync($"{_objectName}.removeItem", cancellationToken ?? CancellationToken.None, key);
                }
            }

            return new ValueTask(Task.CompletedTask);
        }

        /// <summary>Saves an entry asynchroniously with the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ValueTask
        /// </returns>
        public ValueTask SetAsync(string key, string data, CancellationToken? cancellationToken = null)
            => _jSRuntime.InvokeVoidAsync($"{_objectName}.setItem", cancellationToken ?? CancellationToken.None, key, data);

    }

}
