using Bond;
using NServiceBus;

[Schema]
[System.CodeDom.Compiler.GeneratedCode("gbc", "0.6.0.0")]
public class MyMessage :
    IMessage
{

    [Id(0)]
    public string Name { get; set; }
}