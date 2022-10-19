using Forge.Wasm.BrowserStorages.Serialization.Abstraction;
using Forge.Wasm.BrowserStorages.Storage.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.Wasm.BrowserStorages.Services.Abstraction
{

    /// <summary>Storage service with asynhronous methods</summary>
    public abstract class StorageServiceBaseAsync : IServiceAsync
    {

        private readonly ILogger _logger;
        private readonly IStorageProviderAsync _storageProvider;
        private readonly ISerializationProviderBase _serializationProvider;

        /// <summary>Occurs when the data changing (before)</summary>
        public event EventHandler<BeforeChangeEventArgs> OnBeforeChange;

        /// <summary>Occurs when the data changed (after)</summary>
        public event EventHandler<AfterChangeEventArgs> OnAfterChange;

        /// <summary>Initializes a new instance of the <see cref="StorageServiceBaseAsync" /> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="storageProvider">The storage provider.</param>
        /// <param name="serializationProvider">The serialization provider.</param>
        protected StorageServiceBaseAsync(ILogger logger, IStorageProviderAsync storageProvider, ISerializationProviderBase serializationProvider)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (storageProvider == null) throw new ArgumentNullException(nameof(storageProvider));
            if (serializationProvider == null) throw new ArgumentNullException(nameof(serializationProvider));
            _logger = logger;
            _storageProvider = storageProvider;
            _serializationProvider = serializationProvider;
        }

        /// <summary>Clears the content of the storage asynchroniously</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ValueTask
        /// </returns>
        public ValueTask ClearAsync(CancellationToken? cancellationToken = null)
            => _storageProvider.ClearAsync(cancellationToken);

        /// <summary>Determines asynchroniously whether the specified key exists</summary>
        /// <param name="key">The key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   <c>true</c> if the specified key exists; otherwise, <c>false</c>.
        /// </returns>
        public ValueTask<bool> ContainsKeyAsync(string key, CancellationToken? cancellationToken = null)
            => _storageProvider.ContainsKeyAsync(key, cancellationToken);

        /// <summary>Gets the value asynchroniously based on the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The data in string format
        /// </returns>
        public async ValueTask<TData> GetAsync<TData>(string key, CancellationToken? cancellationToken = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            string serializedData = await _storageProvider.GetAsync(key, cancellationToken);

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

        /// <summary>Gets the value asynchroniously based on the specified key. This method does not deserialize the content.</summary>
        /// <param name="key">The key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The data in string format
        /// </returns>
        public ValueTask<string> GetAsStringAsync(string key, CancellationToken? cancellationToken = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            return _storageProvider.GetAsync(key, cancellationToken);
        }

        /// <summary>Gets the key itself asynchroniously from the specified index.</summary>
        /// <param name="index">The index.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The key
        /// </returns>
        public ValueTask<string> KeyAsync(int index, CancellationToken? cancellationToken = null)
            => _storageProvider.KeyAsync(index, cancellationToken);

        /// <summary>Return the keys asynchroniously from the storage</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The keys
        /// </returns>
        public ValueTask<IEnumerable<string>> KeysAsync(CancellationToken? cancellationToken = null)
            => _storageProvider.KeysAsync(cancellationToken);

        /// <summary>Gets the number of the existing entries asynchroniously in the storage</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The number of the entries
        /// </returns>
        public ValueTask<int> LengthAsync(CancellationToken? cancellationToken = null)
            => _storageProvider.LengthAsync(cancellationToken);

        /// <summary>Removes the entry asynchroniously based on the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ValueTask
        /// </returns>
        public ValueTask RemoveAsync(string key, CancellationToken? cancellationToken = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            return _storageProvider.RemoveAsync(key, cancellationToken);
        }

        /// <summary>Removes the entries asynchroniously based on the specified keys.</summary>
        /// <param name="keys">The keys.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ValueTask
        /// </returns>
        public ValueTask RemoveAsync(IEnumerable<string> keys, CancellationToken? cancellationToken = null)
            => _storageProvider.RemoveAsync(keys, cancellationToken);

        /// <summary>Saves an entry asynchroniously with the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ValueTask
        /// </returns>
        public async ValueTask SetAsync<TData>(string key, TData data, CancellationToken? cancellationToken = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            BeforeChangeEventArgs e = await RaiseOnBeforeChangeAsync(key, data, true).ConfigureAwait(false);

            if (e.IsCanceled) return;

            string serializedData = _serializationProvider.Serialize(data);
            await _storageProvider.SetAsync(key, serializedData, cancellationToken).ConfigureAwait(false);

            RaiseOnAfterChange(key, e.PreviousValue, data);
        }

        /// <summary>Saves an entry asynchroniously with the specified key. This method does not do serialization, it set the given string directly into the storage.</summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ValueTask
        /// </returns>
        public async ValueTask SetAsStringAsync(string key, string data, CancellationToken? cancellationToken = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            BeforeChangeEventArgs e = await RaiseOnBeforeChangeAsync(key, data, false).ConfigureAwait(false);

            if (e.IsCanceled) return;

            await _storageProvider.SetAsync(key, data, cancellationToken).ConfigureAwait(false);

            RaiseOnAfterChange(key, e.PreviousValue, data);
        }

        private async Task<BeforeChangeEventArgs> RaiseOnBeforeChangeAsync<TData>(string key, TData data, bool isDeserializationAllowedForPrevValue)
        {
            var e = new BeforeChangeEventArgs
            {
                Key = key,
                PreviousValue = await GetInternalAsync<TData>(key, isDeserializationAllowedForPrevValue).ConfigureAwait(false),
                NewValue = data
            };

            OnBeforeChange?.Invoke(this, e);

            return e;
        }

        private async Task<TData> GetInternalAsync<TData>(string key, bool isDeserializationAllowedForPrevValue, CancellationToken? cancellationToken = null)
        {
            string serialisedData = await _storageProvider.GetAsync(key, cancellationToken).ConfigureAwait(false);

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
