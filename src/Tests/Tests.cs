using Bond;
using Bond.IO.Unsafe;
using Bond.Protocols;
using Examples;
using Xunit;

public class Tests
{
    [Fact]
    public void Foo2()
    {
        var output = new OutputBuffer();
        var writer = new CompactBinaryWriter<OutputBuffer>(output);

        var message = new Example {Name = "sdfsdf"};
        var serializer = new Serializer<CompactBinaryWriter<OutputBuffer>>(typeof(Example));
        serializer.Serialize(message, writer);

        var input = new InputBuffer(output.Data);
        var reader = new CompactBinaryReader<InputBuffer>(input);

        var deserializer = new Deserializer<CompactBinaryReader<InputBuffer>>(typeof(Example));

        var dst = (Example) deserializer.Deserialize(reader);
        Trace.WriteLine(dst.Name);
    }

    [Fact]
    public void Foo()
    {
        var output = new OutputBuffer();
        var writer = new CompactBinaryWriter<OutputBuffer>(output);

        Serialize.To(writer, new Example {Name = "sdfsdf"});

        var input = new InputBuffer(output.Data);
        var reader = new CompactBinaryReader<InputBuffer>(input);

        var dst = Deserialize<Example>.From(reader);
        Trace.WriteLine(dst.Name);
    }
}