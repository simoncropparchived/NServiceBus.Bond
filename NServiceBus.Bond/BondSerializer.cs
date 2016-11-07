using NServiceBus.MessageInterfaces;
using NServiceBus.Settings;
using System;
using Bond;
using Bond.IO.Unsafe;
using Bond.Protocols;
using NServiceBus.Serialization;

namespace NServiceBus.Bond
{

    /// <summary>
    /// Defines the capabilities of the Bond serializer
    /// </summary>
    public class BondSerializer : SerializationDefinition
    {

        /// <summary>
        /// <see cref="SerializationDefinition.Configure"/>
        /// </summary>
        public override Func<IMessageMapper, IMessageSerializer> Configure(ReadOnlySettings settings)
        {
            return mapper =>
            {
                var contentTypeKey = settings.GetContentTypeKey();

                var serializationDelegates = settings.SerializationDelegateBuilder();
                if (serializationDelegates == null)
                {
                    serializationDelegates = messageType =>
                    {
                        return new SertializationDelegates(
                            serialize: (buffer, message) =>
                            {
                                var writer = new CompactBinaryWriter<OutputBuffer>(buffer);
                                var serializer = new Serializer<CompactBinaryWriter<OutputBuffer>>(messageType);
                                serializer.Serialize(message, writer);
                            },
                            deserialize: buffer =>
                            {
                                var reader = new CompactBinaryReader<InputBuffer>(buffer);
                                var deserializer = new Deserializer<CompactBinaryReader<InputBuffer>>(messageType);
                                return deserializer.Deserialize(reader);
                            });
                    };
                }

                return new MessageSerializer(contentTypeKey, serializationDelegates);
            };
        }
    }
}