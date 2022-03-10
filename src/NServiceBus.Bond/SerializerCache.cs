static class SerializerCache
{
    static ConcurrentDictionary<Type, SerializeWrapper> cache = new();

    public static SerializeWrapper GetSerializer(Type messageType) =>
        cache.GetOrAdd(messageType,
            type => new(
                serializer: new(type),
                deserializer: new(type)
            ));
}