using NServiceBus;
using NServiceBus.Bond;
#pragma warning disable 618

static class ScheduledTaskHelper
{
    static Type scheduledTaskType = typeof(ScheduledTask);
    public static Type WrapperType = typeof(ScheduledTaskWrapper);

    public static bool IsScheduleTask(this Type messageType) =>
        messageType == scheduledTaskType;

    public static ScheduledTaskWrapper ToWrapper(ScheduledTask target) =>
        new()
        {
            TaskId = target.TaskId.ToString("D"),
            Name = target.Name,
            Ticks = target.Every.Ticks,
        };

    public static object FromWrapper(ScheduledTaskWrapper target) =>
        new ScheduledTask
        {
            TaskId = Guid.ParseExact(target.TaskId, "D"),
            Name = target.Name,
            Every = TimeSpan.FromTicks(target.Ticks)
        };
}