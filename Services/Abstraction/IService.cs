using System;
using System.Collections.Generic;

namespace Forge.Wasm.BrowserStorages.Services.Abstraction
{

    /// <summary>Synchronuous storage service definition</summary>
    public interface IService
    {

        /// <summary>Occurs when the data changing (before)</summary>
        event EventHandler<BeforeChangeEventArgs> OnBeforeChange;

        /// <summary>Occurs when the data changed (after)</summary>
        event EventHandler<AfterChangeEventArgs> OnAfterChange;

        /// <summary>Clears the content of the storage</summary>
        void Clear();

        /// <summary>Determines whether the specified key exists</summary>
        /// <param name="key">The key value</param>
        /// <returns>
        ///   <c>true</c> if the specified key exists; otherwise, <c>false</c>.</returns>
        bool ContainsKey(string key);

        /// <summary>Gets the value based on the specified key.</summary>
        /// <param name="key">The key.
        /// Cannot be null.</param>
        /// <returns>The data in strong format</returns>
        TData Get<TData>(string key);

        /// <summary>Gets the value based on the specified key. This method does not make deserialization.</summary>
        /// <param name="key">The key.
        /// Cannot be null.</param>
        /// <returns>The data in string format</returns>
        string GetAsString(string key);

        /// <summary>Gets the key itself from the specified index.</summary>
        /// <param name="index">The index.</param>
        /// <returns>The key</returns>
        string Key(int index);

        /// <summary>Return the keys from the storage</summary>
        /// <returns>The keys</returns>
        IEnumerable<string> Keys();

        /// <summary>Gets the number of the existing entries in the storage</summary>
        /// <returns>The number of the entries</returns>
        int Length();

        /// <summary>Removes the entry based on the specified key.</summary>
        /// <param name="key">The key.</param>
        void Remove(string key);

        /// <summary>Removes the entries based on the specified keys.</summary>
        /// <param name="keys">The keys.</param>
        void Remove(IEnumerable<string> keys);

        /// <summary>Saves an entry with the specified key.</summary>
        /// <param name="key">The key. Cannot be null.</param>
        /// <param name="data">The data.</param>
        void Set<TData>(string key, TData data);

        /// <summary>Saves an entry with the specified key. This method does not do serialization, it set the given string directly into the storage.</summary>
        /// <param name="key">The key. Cannot be null.</param>
        /// <param name="data">The data.</param>
        void SetAsString(string key, string data);

    }

}
