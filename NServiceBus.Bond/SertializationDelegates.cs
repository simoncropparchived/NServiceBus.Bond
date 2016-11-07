using System;
using Bond.IO.Unsafe;

namespace NServiceBus.Bond
{
    public class SertializationDelegates
    {
        public SertializationDelegates(Action<OutputBuffer, object> serialize, Func<InputBuffer, object> deserialize)
        {
            Serialize = serialize;
            Deserialize = deserialize;
        }

        public Action<OutputBuffer, object> Serialize { get; }
        public Func<InputBuffer, object> Deserialize { get; }
    }
}