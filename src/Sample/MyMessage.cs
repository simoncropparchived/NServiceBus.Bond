using Bond;
using NServiceBus;

[Schema]
public class MyMessage :
    IMessage
{
    [Id(0)]
    public string Name { get; set; }
}