namespace Forge.Wasm.BrowserStorages.Serialization.Abstraction
{

    /// <summary>Represents the methods of the serialization provider</summary>
    public interface ISerializationProviderBase
    {

        /// <summary>Serializes the specified object.</summary>
        /// <typeparam name="TData">Type of the object</typeparam>
        /// <param name="data">The object.</param>
        /// <returns>
        ///   Object represented by a string
        /// </returns>
        string Serialize<TData>(TData data);

        /// <summary>Deserializes the specified text into an object or value</summary>
        /// <typeparam name="TData">The result type</typeparam>
        /// <param name="text">The text.</param>
        /// <returns>The result value</returns>
        TData Deserialize<TData>(string text);

    }

}
