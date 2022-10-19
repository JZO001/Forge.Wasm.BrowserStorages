using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Forge.Wasm.BrowserStorages.Storage.Abstraction
{

    /// <summary>Represents the asynchronious methods of a storage. Session and local storages will implements.</summary>
    public interface IStorageProviderAsync
    {

        /// <summary>Clears the content of the storage asynchroniously</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ValueTask
        /// </returns>
        ValueTask ClearAsync(CancellationToken? cancellationToken = null);

        /// <summary>Determines asynchroniously whether the specified key exists</summary>
        /// <param name="key">The key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   <c>true</c> if the specified key exists; otherwise, <c>false</c>.
        /// </returns>
        ValueTask<bool> ContainsKeyAsync(string key, CancellationToken? cancellationToken = null);

        /// <summary>Gets the value asynchroniously based on the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The data in string format
        /// </returns>
        ValueTask<string> GetAsync(string key, CancellationToken? cancellationToken = null);

        /// <summary>Gets the key itself asynchroniously from the specified index.</summary>
        /// <param name="index">The index.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The key
        /// </returns>
        ValueTask<string> KeyAsync(int index, CancellationToken? cancellationToken = null);

        /// <summary>Return the keys asynchroniously from the storage</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The keys
        /// </returns>
        ValueTask<IEnumerable<string>> KeysAsync(CancellationToken? cancellationToken = null);

        /// <summary>Gets the number of the existing entries asynchroniously in the storage</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The number of the entries
        /// </returns>
        ValueTask<int> LengthAsync(CancellationToken? cancellationToken = null);

        /// <summary>Removes the entry asynchroniously based on the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ValueTask
        /// </returns>
        ValueTask RemoveAsync(string key, CancellationToken? cancellationToken = null);

        /// <summary>Removes the entries asynchroniously based on the specified keys.</summary>
        /// <param name="keys">The keys.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ValueTask
        /// </returns>
        ValueTask RemoveAsync(IEnumerable<string> keys, CancellationToken? cancellationToken = null);

        /// <summary>Saves an entry asynchroniously with the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ValueTask
        /// </returns>
        ValueTask SetAsync(string key, string data, CancellationToken? cancellationToken = null);

    }

}
