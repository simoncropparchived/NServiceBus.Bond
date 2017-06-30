using Bond;
using Bond.IO.Unsafe;
using Bond.Protocols;

class SerializeWrapper
{
    public Serializer<CompactBinaryWriter<OutputBuffer>> Serializer;
    public Deserializer<CompactBinaryReader<InputBuffer>> Deserializer;
}