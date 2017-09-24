using System;
using Bond;
using Bond.Protocols;
using NServiceBus.Bond;
using NServiceBus.Configuration.AdvancedExtensibility;
using NServiceBus.Serialization;
using NServiceBus.Settings;

namespace NServiceBus
{
    /// <summary>
    /// Extensions for <see cref="SerializationExtensions{T}"/> to manipulate how messages are serialized.
    /// </summary>
    public static class BondConfigurationExtensions
    {
        /// <summary>
        /// Sets a the convention to use for serializing and deserializing messages.
        /// For serialization: default constructors of <see cref="CompactBinaryWriter{I}"/> and <see cref="Serializer{W}"/>
        /// For deserialization: default constructors of <see cref="CompactBinaryReader{I}"/> and <see cref="Deserializer{W}"/>
        /// </summary>
        public static void SerializationDelegates(this SerializationExtensions<BondSerializer> config, Func<Type, SerializationDelegates> serializationDelegatesBuilder)
        {
            Guard.AgainstNull(config, nameof(config));
            Guard.AgainstNull(serializationDelegatesBuilder, nameof(serializationDelegatesBuilder));
            var settings = config.GetSettings();
            settings.Set<Func<Type, SerializationDelegates>>(serializationDelegatesBuilder);
        }

        internal static Func<Type, SerializationDelegates> SerializationDelegateBuilder(this ReadOnlySettings settings)
        {
            return settings.GetOrDefault<Func<Type, SerializationDelegates>>();
        }

        /// <summary>
        /// Configures string to use for <see cref="Headers.ContentType"/> headers.
        /// </summary>
        /// <remarks>
        /// Defaults to "bond".
        /// </remarks>
        /// <param name="config">The <see cref="SerializationExtensions{T}"/> instance.</param>
        /// <param name="contentTypeKey">The content type key to use.</param>
        public static void ContentTypeKey(this SerializationExtensions<BondSerializer> config, string contentTypeKey)
        {
            Guard.AgainstNull(config, nameof(config));
            Guard.AgainstNullOrEmpty(contentTypeKey, nameof(contentTypeKey));
            var settings = config.GetSettings();
            settings.Set("NServiceBus.Bond.ContentTypeKey", contentTypeKey);
        }

        internal static string GetContentTypeKey(this ReadOnlySettings settings)
        {
            return settings.GetOrDefault<string>("NServiceBus.Bond.ContentTypeKey");
        }
    }
}