using Bond;

namespace NServiceBus.Bond
{
    [Schema]
    public class ScheduledTaskWrapper
    {
        [Id(0)]
        public string TaskId { get; set; }
        [Id(1)]
        public string Name { get; set; }
        [Id(2)]
        public long Ticks { get; set; }
    }
}