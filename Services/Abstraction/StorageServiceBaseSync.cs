using Forge.Wasm.BrowserStorages.Serialization.Abstraction;
using Forge.Wasm.BrowserStorages.Storage.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Forge.Wasm.BrowserStorages.Services.Abstraction
{

    /// <summary>Storage service with synhronous methods</summary>
    public abstract class StorageServiceBaseSync : IService
    {

        private readonly ILogger _logger;
        private readonly IStorageProviderSync _storageProvider;
        private readonly ISerializationProviderBase _serializationProvider;

        /// <summary>Occurs when the data changing (before)</summary>
        public event EventHandler<BeforeChangeEventArgs> OnBeforeChange;

        /// <summary>Occurs when the data changed (after)</summary>
        public event EventHandler<AfterChangeEventArgs> OnAfterChange;

        /// <summary>Initializes a new instance of the <see cref="StorageServiceBaseSync" /> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="storageProvider">The storage provider.</param>
        /// <param name="serializationProvider">The serialization provider.</param>
        /// <exception cref="ArgumentNullException">storageProvider
        /// or
        /// serializationProvider</exception>
        protected StorageServiceBaseSync(ILogger logger, IStorageProviderSync storageProvider, ISerializationProviderBase serializationProvider)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (storageProvider == null) throw new ArgumentNullException(nameof(storageProvider));
            if (serializationProvider == null) throw new ArgumentNullException(nameof(serializationProvider));
            _logger = logger;
            _storageProvider = storageProvider;
            _serializationProvider = serializationProvider;
        }

        /// <summary>Clears the content of the storage</summary>
        public void Clear() => _storageProvider.Clear();

        /// <summary>Determines whether the specified key exists</summary>
        /// <param name="key">The key value</param>
        /// <returns>
        ///   <c>true</c> if the specified key exists; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(string key) => _storageProvider.ContainsKey(key);

        /// <summary>Gets the value based on the specified key.</summary>
        /// <param name="key">The key.
        /// Cannot be null.</param>
        /// <returns>The data in strong format</returns>
        public TData Get<TData>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            string serializedData = _storageProvider.Get(key);

            if (string.IsNullOrWhiteSpace(serializedData)) return default;

            try
            {
                return _serializationProvider.Deserialize<TData>(serializedData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return default;
            }
        }

        /// <summary>Gets the value based on the specified key. This method does not make deserialization.</summary>
        /// <param name="key">The key.
        /// Cannot be null.</param>
        /// <returns>The data in string format</returns>
        public string GetAsString(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            return _storageProvider.Get(key);
        }

        /// <summary>Gets the key itself from the specified index.</summary>
        /// <param name="index">The index.</param>
        /// <returns>The key</returns>
        public string Key(int index) => _storageProvider.Key(index);

        /// <summary>Return the keys from the storage</summary>
        /// <returns>The keys</returns>
        public IEnumerable<string> Keys() => _storageProvider.Keys();

        /// <summary>Gets the number of the existing entries in the storage</summary>
        /// <returns>The number of the entries</returns>
        public int Length() => _storageProvider.Length();

        /// <summary>Removes the entry based on the specified key.</summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            _storageProvider.Remove(key);
        }

        /// <summary>Removes the entries based on the specified keys.</summary>
        /// <param name="keys">The keys.</param>
        public void Remove(IEnumerable<string> keys)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));

            foreach (var key in keys)
            {
                _storageProvider.Remove(key);
            }
        }

        /// <summary>Saves an entry with the specified key.</summary>
        /// <param name="key">The key. Cannot be null.</param>
        /// <param name="data">The data.</param>
        public void Set<TData>(string key, TData data)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            BeforeChangeEventArgs e = RaiseOnBeforeChange(key, data, true);

            if (e.IsCanceled) return;

            string serializedData = _serializationProvider.Serialize(data);
            _storageProvider.Set(key, serializedData);

            RaiseOnAfterChange(key, e.PreviousValue, data);
        }

        /// <summary>Saves an entry with the specified key. This method does not do serialization, it set the given string directly into the storage.</summary>
        /// <param name="key">The key. Cannot be null.</param>
        /// <param name="data">The data.</param>
        public void SetAsString(string key, string data)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            BeforeChangeEventArgs e = RaiseOnBeforeChange(key, data, false);

            if (e.IsCanceled) return;

            _storageProvider.Set(key, data);

            RaiseOnAfterChange(key, e.PreviousValue, data);
        }

        private BeforeChangeEventArgs RaiseOnBeforeChange<TData>(string key, TData data, bool isDeserializationAllowedForPrevValue)
        {
            var e = new BeforeChangeEventArgs
            {
                Key = key,
                PreviousValue = GetInternal<TData>(key, isDeserializationAllowedForPrevValue),
                NewValue = data
            };

            OnBeforeChange?.Invoke(this, e);

            return e;
        }

        private TData GetInternal<TData>(string key, bool isDeserializationAllowedForPrevValue)
        {
            string serialisedData = _storageProvider.Get(key);

            if (!isDeserializationAllowedForPrevValue) return (TData)(object)serialisedData;

            if (string.IsNullOrWhiteSpace(serialisedData)) return default;

            try
            {
                return _serializationProvider.Deserialize<TData>(serialisedData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return default;
            }
        }

        private void RaiseOnAfterChange(string key, object oldValue, object data)
        {
            OnAfterChange?.Invoke(this, new AfterChangeEventArgs
            {
                Key = key,
                PreviousValue = oldValue,
                NewValue = data
            });
        }

    }

}
