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
        Console.WriteLine("Press any key to stop program");
        Console.Read();
        await endpoint.Stop();
    }
}