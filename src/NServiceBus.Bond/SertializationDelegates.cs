using System;
using Bond.IO.Unsafe;

namespace NServiceBus.Bond
{
    /// <summary>
    /// Used by <see cref="BondConfigurationExtensions.SerializationDelegates"/>
    /// </summary>
    public class SerializationDelegates
    {
        /// <summary>
        /// Initialize a new instance of <see cref="SerializationDelegates"/>.
        /// </summary>
        public SerializationDelegates(Action<OutputBuffer, object> serialize, Func<InputBuffer, object> deserialize)
        {
            Serialize = serialize;
            Deserialize = deserialize;
        }

        /// <summary>
        /// The delegate used to serialize.
        /// </summary>
        public Action<OutputBuffer, object> Serialize { get; }

        /// <summary>
        /// The delegate used to deserialize.
        /// </summary>
        public Func<InputBuffer, object> Deserialize { get; }
    }
}