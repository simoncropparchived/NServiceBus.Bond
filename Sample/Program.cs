using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Bond;

class Program
{
    static async Task Main()
    {
        var configuration = new EndpointConfiguration("BondSerializerSample");
        configuration.UseSerialization<BondSerializer>();
        configuration.EnableInstallers();
        configuration.SendFailedMessagesTo("error");
        configuration.UsePersistence<InMemoryPersistence>();
        var transport = configuration.UseTransport<LearningTransport>();
        transport.NoPayloadSizeRestriction();

        var endpoint = await Endpoint.Start(configuration);
        try
        {
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
                    })
                .ConfigureAwait(false);

            Console.WriteLine("\r\nPress any key to stop program\r\n");
            Console.Read();
        }
        finally
        {
            await endpoint.Stop();
        }
    }
}