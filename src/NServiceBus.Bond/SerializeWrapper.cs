using Bond;
using Bond.IO.Unsafe;
using Bond.Protocols;

class SerializeWrapper
{
    public Serializer<CompactBinaryWriter<OutputBuffer>> Serializer { get; }
    public Deserializer<CompactBinaryReader<InputBuffer>> Deserializer { get; }

    public SerializeWrapper(Serializer<CompactBinaryWriter<OutputBuffer>> serializer, Deserializer<CompactBinaryReader<InputBuffer>> deserializer)
    {
        Serializer = serializer;
        Deserializer = deserializer;
    }
}