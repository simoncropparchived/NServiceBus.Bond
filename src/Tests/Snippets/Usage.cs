using Bond.IO.Unsafe;
using Bond.Protocols;
using NServiceBus;
using NServiceBus.Bond;

class Usage
{
    Usage(EndpointConfiguration configuration)
    {
        #region BondSerialization

        configuration.UseSerialization<BondSerializer>();

        #endregion
    }

    void SerializationDelegates(EndpointConfiguration configuration)
    {
        #region BondSerializationDelegates

        var serialization = configuration.UseSerialization<BondSerializer>();
        serialization.SerializationDelegates(
            serializationDelegatesBuilder: messageType =>
            {
                var item = SerializerCache.GetSerializer(messageType);
                return new(
                    serialize: (buffer, message) =>
                    {
                        var writer = new CompactBinaryWriter<OutputBuffer>(buffer);
                        item.Serializer.Serialize(message, writer);
                    },
                    deserialize: buffer =>
                    {
                        var reader = new CompactBinaryReader<InputBuffer>(buffer);
                        return item.Deserializer.Deserialize(reader);
                    });
            });

        #endregion
    }

    void ContentTypeKey(EndpointConfiguration configuration)
    {
        #region BondContentTypeKey

        var serialization = configuration.UseSerialization<BondSerializer>();
        serialization.ContentTypeKey("custom-key");

        #endregion
    }
}