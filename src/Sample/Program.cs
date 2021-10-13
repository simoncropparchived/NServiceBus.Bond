using NServiceBus;
using NServiceBus.Bond;

class Program
{
    static async Task Main()
    {
        var configuration = new EndpointConfiguration("BondSerializerSample");
        configuration.UseSerialization<BondSerializer>();
        var transport = configuration.UseTransport<LearningTransport>();
        transport.NoPayloadSizeRestriction();

        var endpoint = await Endpoint.Start(configuration);
        var message = new MyMessage
        {
            Name = "immediate",
        };
        await endpoint.SendLocal(message);

        await endpoint.ScheduleEvery(
            timeSpan: TimeSpan.FromSeconds(5),
            task: pipelineContext =>
            {
                var delayed = new MyMessage
                {
                    Name = "delayed",
                };
                return pipelineContext.SendLocal(delayed);
            });

        Console.WriteLine("Press any key to stop program");
        Console.Read();
        await endpoint.Stop();
    }
}