using System.Text.Json;

namespace Forge.Wasm.BrowserStorages.Serialization.DefaultSerializer.Abstraction
{

    /// <summary>Represents the options for the default built-in serializer</summary>
    public abstract class SerializerOptionsBase
    {

        /// <summary>Gets the json serializer options.</summary>
        /// <value>The default json serializer options.</value>
        public JsonSerializerOptions SerializeOptions { get; } = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        /// <summary>Gets the json deserializer options.</summary>
        /// <value>The default json serializer options.</value>
        public JsonSerializerOptions DeserializeOptions { get; } = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

    }

}
