using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Bond;

class Program
{
    static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        var endpointConfiguration = new EndpointConfiguration("BondSerializerSample");
        endpointConfiguration.UseSerialization<BondSerializer>();
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        transport.NoPayloadSizeRestriction();

        var endpoint = await Endpoint.Start(endpointConfiguration);
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