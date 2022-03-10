using Bond.IO.Unsafe;
using NServiceBus;
using NServiceBus.Bond;
using NServiceBus.Serialization;

#pragma warning disable 618

class MessageSerializer :
    IMessageSerializer
{
    Func<Type, SerializationDelegates> serializeBuilder;
    ThreadLocal<OutputBuffer> threadLocal = new(() => new());
    ConcurrentDictionary<RuntimeTypeHandle, SerializationDelegates> delegateCache = new();

    public MessageSerializer(string? contentType, Func<Type, SerializationDelegates> serializeBuilder)
    {
        this.serializeBuilder = serializeBuilder;
        ContentType = contentType ?? "bond";
    }

    public void Serialize(object message, Stream stream)
    {
        var messageType = message.GetType();
        if (messageType.Name.EndsWith("__impl"))
        {
            throw new("Interface based message are not supported. Create a class that implements the desired interface.");
        }

        var output = threadLocal.Value;
        output.Position = 0;
        Serialize(message, messageType, output);
        var dataArray = output.Data.Array;
        stream.Write(dataArray, 0, dataArray.Length);
    }

    void Serialize(object message, Type messageType, OutputBuffer output)
    {
        if (message is not ScheduledTask task)
        {
            var delegates = GetDelegates(messageType);
            delegates.Serialize(output, message);
            return;
        }
        var scheduledTaskDelegates = GetDelegates(ScheduledTaskHelper.WrapperType);
        var wrapper = ScheduledTaskHelper.ToWrapper(task);
        scheduledTaskDelegates.Serialize(output, wrapper);
    }

    object DeserializeInner(Stream stream, IList<Type> messageTypes)
    {
        var bytes = stream.GetBytesFromMemoryStream();
        var messageType = messageTypes.First();
        var input = new InputBuffer(bytes);
        if (messageType.IsScheduleTask())
        {
            var scheduledTaskDelegates = GetDelegates(ScheduledTaskHelper.WrapperType);
            var scheduledTaskWrapper = (ScheduledTaskWrapper) scheduledTaskDelegates.Deserialize(input);
            return ScheduledTaskHelper.FromWrapper(scheduledTaskWrapper);
        }
        var delegates = GetDelegates(messageType);
        return delegates.Deserialize(input);
    }

    SerializationDelegates GetDelegates(Type messageType)
        => delegateCache.GetOrAdd(messageType.TypeHandle, _ => serializeBuilder(messageType));

    public object[] Deserialize(Stream stream, IList<Type> messageTypes) =>
        new[]
        {
            DeserializeInner(stream, messageTypes)
        };

    public string ContentType { get; }
}