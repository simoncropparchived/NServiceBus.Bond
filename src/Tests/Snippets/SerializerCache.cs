using Bond;
using Bond.IO.Unsafe;
using Bond.Protocols;

#region SerializerCache
static class SerializerCache
{
    static ConcurrentDictionary<Type, Item> cache = new();

    public static Item GetSerializer(Type messageType)
    {
        return cache.GetOrAdd(messageType,
            type => new(
                new(type),
                new(type)
            ));
    }

    public class Item
    {
        public readonly Serializer<CompactBinaryWriter<OutputBuffer>> Serializer;
        public readonly Deserializer<CompactBinaryReader<InputBuffer>> Deserializer;

        public Item(
            Serializer<CompactBinaryWriter<OutputBuffer>> serializer,
            Deserializer<CompactBinaryReader<InputBuffer>> deserializer)
        {
            Serializer = serializer;
            Deserializer = deserializer;
        }
    }
}
#endregion