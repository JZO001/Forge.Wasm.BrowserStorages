using Microsoft.JSInterop;
using System;
using System.Collections.Generic;

namespace Forge.Wasm.BrowserStorages.Storage.Abstraction
{

    /// <summary>Provides the base functionalities</summary>
    public abstract class StorageProviderSyncBase : IStorageProviderSync
    {

        private readonly IJSInProcessRuntime _jsInProcessRuntime;
        private readonly string _objectName;

        /// <summary>Initializes a new instance of the <see cref="StorageProviderSyncBase" /> class.</summary>
        /// <param name="jsInProcessRuntime">The IJSInProcessRuntime.
        /// IJSRuntime must be IJSInProcessRuntime.</param>
        /// <param name="objectName">Name of the storage.</param>
        /// <exception cref="ArgumentNullException">jSRuntime
        /// or
        /// objectName</exception>
        /// <exception cref="InvalidOperationException">
        /// Unable to cast {typeof(IJSRuntime).Name} to {typeof(IJSInProcessRuntime).Name}. The provided object is not correct. Please make sure the {typeof(IJSInProcessRuntime).Name} is available in your project type.
        /// </exception>
        protected StorageProviderSyncBase(IJSInProcessRuntime jsInProcessRuntime, string objectName)
        {
            if (jsInProcessRuntime == null) throw new ArgumentNullException(nameof(jsInProcessRuntime));
            if (string.IsNullOrWhiteSpace(objectName)) throw new ArgumentNullException(nameof(objectName));
            _jsInProcessRuntime = jsInProcessRuntime;
            _objectName = objectName;
        }

        /// <summary>Clears the content of the storage</summary>
        public void Clear()
        {
            _jsInProcessRuntime.InvokeVoid($"{_objectName}.clear");
        }

        /// <summary>Determines whether the specified key exists</summary>
        /// <param name="key">The key value</param>
        /// <returns>
        ///   <c>true</c> if the specified key exists; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(string key)
        {
            return _jsInProcessRuntime.Invoke<bool>($"{_objectName}.hasOwnProperty", key);
        }

        /// <summary>Gets the value based on the specified key.</summary>
        /// <param name="key">The key.
        /// Cannot be null.</param>
        /// <returns>The data in strong format</returns>
        public string Get(string key)
        {
            return _jsInProcessRuntime.Invoke<string>($"{_objectName}.getItem", key);
        }

        /// <summary>Gets the key itself from the specified index.</summary>
        /// <param name="index">The index.</param>
        /// <returns>The key</returns>
        public string Key(int index)
        {
            return _jsInProcessRuntime.Invoke<string>($"{_objectName}.key", index);
        }

        /// <summary>Return the keys from the storage</summary>
        /// <returns>The keys</returns>
        public IEnumerable<string> Keys()
        {
            return _jsInProcessRuntime.Invoke<IEnumerable<string>>("eval", $"Object.keys({_objectName})");
        }

        /// <summary>Gets the number of the existing entries in the storage</summary>
        /// <returns>The number of the entries</returns>
        public int Length()
        {
            return _jsInProcessRuntime.Invoke<int>("eval", $"{_objectName}.length");
        }

        /// <summary>Removes the entry based on the specified key.</summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            _jsInProcessRuntime.InvokeVoidAsync($"{_objectName}.removeItem", key);
        }

        /// <summary>Removes the entries based on the specified keys.</summary>
        /// <param name="keys">The keys.</param>
        public void Remove(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                _jsInProcessRuntime.InvokeVoid($"{_objectName}.removeItem", key);
            }
        }

        /// <summary>Saves an entry with the specified key.</summary>
        /// <param name="key">The key. Cannot be null.</param>
        /// <param name="data">The data.</param>
        public void Set(string key, string data)
        {
            _jsInProcessRuntime.InvokeVoid($"{_objectName}.setItem", key, data);
        }

    }

}
