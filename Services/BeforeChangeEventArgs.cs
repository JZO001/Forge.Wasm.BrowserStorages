using System;

namespace Forge.Wasm.BrowserStorages.Services
{

    /// <summary>Represents the data changing event args, before it changed</summary>
    [Serializable]
    public class BeforeChangeEventArgs : AfterChangeEventArgs
    {

        /// <summary>Gets or sets a value indicating whether the event is canceled.</summary>
        /// <value>
        ///   <c>true</c> if this event is canceled; otherwise, <c>false</c>.</value>
        public bool IsCanceled { get; set; }

    }

}
