using System.Collections.Concurrent;
using System.Threading;
using Bond.IO.Unsafe;
using NServiceBus;
using NServiceBus.Bond;
using NServiceBus.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#pragma warning disable 618

class MessageSerializer : IMessageSerializer
{
    Func<Type, SerializationDelegates> serializeBuilder;
    ThreadLocal<OutputBuffer> threadLocal = new ThreadLocal<OutputBuffer>(() => new OutputBuffer());
    ConcurrentDictionary<RuntimeTypeHandle, SerializationDelegates> delegateCache = new ConcurrentDictionary<RuntimeTypeHandle, SerializationDelegates>();

    public MessageSerializer(string contentType, Func<Type, SerializationDelegates> serializeBuilder)
    {
        this.serializeBuilder = serializeBuilder;
        if (contentType == null)
        {
            ContentType = "bond";
        }
        else
        {
            ContentType = contentType;
        }
    }

    public void Serialize(object message, Stream stream)
    {
        var messageType = message.GetType();
        if (messageType.Name.EndsWith("__impl"))
        {
            throw new Exception("Interface based message are not supported. Create a class that implements the desired interface.");
        }

        var output = threadLocal.Value;
        output.Position = 0;
        Serialize(message, messageType, output);
        var dataArray = output.Data.Array;
        stream.Write(dataArray, 0, dataArray.Length);
    }

    void Serialize(object message, Type messageType, OutputBuffer output)
    {
        if (!(message is ScheduledTask task))
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
    {
        return delegateCache.GetOrAdd(messageType.TypeHandle, handle => serializeBuilder(messageType));
    }

    public object[] Deserialize(Stream stream, IList<Type> messageTypes)
    {
        return new[]
        {
            DeserializeInner(stream, messageTypes)
        };
    }

    public string ContentType { get; }
}