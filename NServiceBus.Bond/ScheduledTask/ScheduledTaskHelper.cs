using System;
using System.Reflection;

namespace NServiceBus.Bond
{
    static class ScheduledTaskHelper
    {
        static Type scheduledTaskType = typeof(IMessage).Assembly.GetType("NServiceBus.ScheduledTask");
        static PropertyInfo taskProperty;
        static PropertyInfo nameProperty;
        static PropertyInfo everyProperty;
        static ConstructorInfo constructor;
        public static Type WrapperType = typeof(ScheduledTaskWrapper);

        static ScheduledTaskHelper()
        {
            constructor = scheduledTaskType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, Type.EmptyTypes, null);
            taskProperty = scheduledTaskType.GetProperty("TaskId");
            nameProperty = scheduledTaskType.GetProperty("Name");
            everyProperty = scheduledTaskType.GetProperty("Every");
        }

        public static bool IsScheduleTask(this Type messageType)
        {
            return messageType == scheduledTaskType;
        }

        public static ScheduledTaskWrapper ToWrapper(object target)
        {
            var taskId = (Guid) taskProperty.GetValue(target, null);
            var name = (string) nameProperty.GetValue(target, null);
            var timeSpan = (TimeSpan) everyProperty.GetValue(target, null);
            return new ScheduledTaskWrapper
            {
                TaskId = taskId.ToString("D"),
                Name = name,
                Ticks = timeSpan.Ticks,
            };
        }

        public static object FromWrapper(ScheduledTaskWrapper target)
        {
            var instance = constructor.Invoke(null);
            taskProperty.SetValue(instance, Guid.ParseExact(target.TaskId, "D"));
            nameProperty.SetValue(instance, target.Name);
            everyProperty.SetValue(instance, TimeSpan.FromTicks(target.Ticks));
            return instance;
        }

    }
}