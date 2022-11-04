using NServiceBus.MessageInterfaces;
using NServiceBus.Settings;
using Bond.IO.Unsafe;
using Bond.Protocols;
using NServiceBus.Serialization;

namespace NServiceBus.Bond;

/// <summary>
/// Defines the capabilities of the Bond serializer
/// </summary>
public class BondSerializer :
    SerializationDefinition
{
    /// <summary>
    /// <see cref="SerializationDefinition.Configure"/>
    /// </summary>
    public override Func<IMessageMapper, IMessageSerializer> Configure(IReadOnlySettings settings) =>
        _ =>
        {
            var contentTypeKey = settings.GetContentTypeKey();

            var serializationDelegates = settings.SerializationDelegateBuilder();
            if (serializationDelegates == null)
            {
                serializationDelegates = messageType =>
                {
                    var wrapper = SerializerCache.GetSerializer(messageType);
                    return new(
                        serialize: (buffer, message) =>
                        {
                            var writer = new CompactBinaryWriter<OutputBuffer>(buffer);
                            wrapper.Serializer.Serialize(message, writer);
                        },
                        deserialize: buffer =>
                        {
                            var reader = new CompactBinaryReader<InputBuffer>(buffer);
                            return wrapper.Deserializer.Deserialize(reader);
                        });
                };
            }

            return new MessageSerializer(contentTypeKey, serializationDelegates);
        };
}