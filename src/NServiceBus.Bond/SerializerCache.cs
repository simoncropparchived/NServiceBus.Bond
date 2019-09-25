using System;
using System.Collections.Concurrent;
using Bond;
using Bond.IO.Unsafe;
using Bond.Protocols;

static class SerializerCache
{
    static ConcurrentDictionary<Type, SerializeWrapper> cache = new ConcurrentDictionary<Type, SerializeWrapper>();

    public static SerializeWrapper GetSerializer(Type messageType)
    {
        return cache.GetOrAdd(messageType,
            type => new SerializeWrapper
            (
                serializer: new Serializer<CompactBinaryWriter<OutputBuffer>>(type),
                deserializer: new Deserializer<CompactBinaryReader<InputBuffer>>(type)
            ));
    }
}