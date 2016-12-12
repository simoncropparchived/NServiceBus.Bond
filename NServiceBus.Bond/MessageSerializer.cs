using System.Collections.Concurrent;
using System.Threading;
using Bond.IO.Unsafe;

namespace NServiceBus.Bond
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Serialization;

    class MessageSerializer : IMessageSerializer
    {
        Func<Type, SertializationDelegates> serializeBuilder;
        ThreadLocal<OutputBuffer> threadLocal = new ThreadLocal<OutputBuffer>(() => new OutputBuffer());
        ConcurrentDictionary<RuntimeTypeHandle, SertializationDelegates> delegateCache = new ConcurrentDictionary<RuntimeTypeHandle, SertializationDelegates>();

        public MessageSerializer(string contentType, Func<Type, SertializationDelegates> serializeBuilder)
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
            var task = message as ScheduledTask;
            if (task == null)
            {
                var delegates = GetDelgates(messageType);
                delegates.Serialize(output, message);
                return;
            }
            var scheduledTaskDelegates = GetDelgates(ScheduledTaskHelper.WrapperType);
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
                var scheduledTaskDelegates = GetDelgates(ScheduledTaskHelper.WrapperType);
                var scheduledTaskWrapper = (ScheduledTaskWrapper) scheduledTaskDelegates.Deserialize(input);
                return ScheduledTaskHelper.FromWrapper(scheduledTaskWrapper);
            }
            var delegates = GetDelgates(messageType);
            return delegates.Deserialize(input);
        }

        SertializationDelegates GetDelgates(Type messageType)
        {
            return delegateCache.GetOrAdd(messageType.TypeHandle, handle => serializeBuilder(messageType));
        }

        public object[] Deserialize(Stream stream, IList<Type> messageTypes)
        {
            var deserializeInner = DeserializeInner(stream, messageTypes);
            return new[]
            {
                deserializeInner
            };
        }

        public string ContentType { get; }
    }
}