using Forge.Wasm.BrowserStorages.Serialization.DefaultSerializer.Abstraction;
using Forge.Wasm.BrowserStorages.Serialization.SessionStorage;
using Microsoft.Extensions.Options;

namespace Forge.Wasm.BrowserStorages.Serialization.DefaultSerializer.SessionStorage
{

    /// <summary>Built-in .NET Json serializer</summary>
    public class JsonSerializer : JsonSerializerBase<SerializerOptions>, ISerializationProvider
    {

        /// <summary>Initializes a new instance of the <see cref="JsonSerializer" /> class.</summary>
        /// <param name="options">The options.</param>
        public JsonSerializer(IOptions<SerializerOptions> options) : base(options)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="JsonSerializer" /> class.</summary>
        /// <param name="sessionStorageOptions">The session storage options.</param>
        public JsonSerializer(SerializerOptions sessionStorageOptions) : base(sessionStorageOptions)
        {
        }

    }

}
