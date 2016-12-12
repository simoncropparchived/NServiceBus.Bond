using System;
using NServiceBus;
using NServiceBus.Bond;

static class ScheduledTaskHelper
{
    static Type scheduledTaskType = typeof(ScheduledTask);
    public static Type WrapperType = typeof(ScheduledTaskWrapper);

    public static bool IsScheduleTask(this Type messageType)
    {
        return messageType == scheduledTaskType;
    }

    public static ScheduledTaskWrapper ToWrapper(ScheduledTask target)
    {
        return new ScheduledTaskWrapper
        {
            TaskId = target.TaskId.ToString("D"),
            Name = target.Name,
            Ticks = target.Every.Ticks,
        };
    }

    public static object FromWrapper(ScheduledTaskWrapper target)
    {
        return new ScheduledTask
        {
            TaskId = Guid.ParseExact(target.TaskId, "D"),
            Name = target.Name,
            Every = TimeSpan.FromTicks(target.Ticks)
        };
    }

}
