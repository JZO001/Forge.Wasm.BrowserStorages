using System;

namespace Forge.Wasm.BrowserStorages.Services
{

    /// <summary>Represents the data changing event args, after it changed</summary>
    [Serializable]
    public class AfterChangeEventArgs : EventArgs
    {

        /// <summary>Gets the key.</summary>
        /// <value>The key.</value>
        public string Key { get; set; }

        /// <summary>Gets the previous value.</summary>
        /// <value>The previous value.</value>
        public object PreviousValue { get; set; }

        /// <summary>The new value.</summary>
        /// <value>The new value.</value>
        public object NewValue { get; set; }

    }

}