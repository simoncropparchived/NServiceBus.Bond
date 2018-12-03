using System;
using System.IO;
using System.Reflection;

static class StreamExtensions
{
    static StreamExtensions()
    {
        field = typeof(MemoryStream).GetField("_buffer", BindingFlags.Instance | BindingFlags.NonPublic);
        if (field == null)
        {
            throw new Exception("Could not read _buffer field from MemoryStream.");
        }
    }

    static FieldInfo field;

    public static byte[] GetBytesFromMemoryStream(this Stream stream)
    {
        if (stream is MemoryStream memoryStream)
        {
            return (byte[]) field.GetValue(memoryStream);
        }
        throw new Exception($"Expected stream to be a MemoryStream but was a {stream.GetType().FullName}");

    }
}