<!--
GENERATED FILE - DO NOT EDIT
This file was generated by [MarkdownSnippets](https://github.com/SimonCropp/MarkdownSnippets).
Source File: /readme.source.md
To change this file edit the source file and then run MarkdownSnippets.
-->

# <img src="/src/icon.png" height="30px"> NServiceBus.Bond

[![Build status](https://ci.appveyor.com/api/projects/status/qf24j875v1ple12e/branch/master?svg=true)](https://ci.appveyor.com/project/SimonCropp/nservicebus-Bond)
[![NuGet Status](https://img.shields.io/nuget/v/NServiceBus.Bond.svg)](https://www.nuget.org/packages/NServiceBus.Bond/)

Add support for [NServiceBus](https://docs.particular.net/nservicebus/) message serialization via [Microsoft Bond](https://microsoft.github.io/bond/manual/bond_cs.html)

> Bond is a cross-platform framework for working with schematized data. It supports cross-language serialization/deserialization and powerful generic mechanisms for efficiently manipulating data. Bond is broadly used at Microsoft in high-scale services.

<!--- StartOpenCollectiveBackers -->

[Already a Patron? skip past this section](#endofbacking)


## Community backed

**It is expected that all developers either [become a Patron](https://opencollective.com/nservicebusextensions/contribute/patron-6976) to use NServiceBusExtensions. [Go to licensing FAQ](https://github.com/NServiceBusExtensions/Home/#licensingpatron-faq)**


### Sponsors

Support this project by [becoming a Sponsor](https://opencollective.com/nservicebusextensions/contribute/sponsor-6972). The company avatar will show up here with a website link. The avatar will also be added to all GitHub repositories under the [NServiceBusExtensions organization](https://github.com/NServiceBusExtensions).


### Patrons

Thanks to all the backing developers. Support this project by [becoming a patron](https://opencollective.com/nservicebusextensions/contribute/patron-6976).

<img src="https://opencollective.com/nservicebusextensions/tiers/patron.svg?width=890&avatarHeight=60&button=false">

<a href="#" id="endofbacking"></a>

<!--- EndOpenCollectiveBackers -->


## Usage

<!-- snippet: BondSerialization -->
<a id='snippet-bondserialization'></a>
```cs
configuration.UseSerialization<BondSerializer>();
```
<sup><a href='/src/Tests/Snippets/Usage.cs#L10-L14' title='Snippet source file'>snippet source</a> | <a href='#snippet-bondserialization' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

This serializer does not support [messages defined as interfaces](https://docs.particular.net/nservicebus/messaging/messages-as-interfaces). If an explicit interface is sent, an exception will be thrown with the following message:

```
Interface based message are not supported.
Create a class that implements the desired interface
```

Instead, use a public class with the same contract as the interface. The class can optionally implement any required interfaces.


### SerializationDelegates

Customizes the cached delegates that serialize and deserialize message types. This is an optional setting.

The default serialization delegates are as follows.

<!-- snippet: BondSerializationDelegates -->
<a id='snippet-bondserializationdelegates'></a>
```cs
var serialization = configuration.UseSerialization<BondSerializer>();
serialization.SerializationDelegates(
    serializationDelegatesBuilder: messageType =>
    {
        var item = SerializerCache.GetSerializer(messageType);
        return new(
            serialize: (buffer, message) =>
            {
                var writer = new CompactBinaryWriter<OutputBuffer>(buffer);
                item.Serializer.Serialize(message, writer);
            },
            deserialize: buffer =>
            {
                var reader = new CompactBinaryReader<InputBuffer>(buffer);
                return item.Deserializer.Deserialize(reader);
            });
    });
```
<sup><a href='/src/Tests/Snippets/Usage.cs#L19-L39' title='Snippet source file'>snippet source</a> | <a href='#snippet-bondserializationdelegates' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

The serializers are cached as per the [Bond performance guidance](https://microsoft.github.io/bond/manual/bond_cs.html#performance).

<!-- snippet: SerializerCache -->
<a id='snippet-serializercache'></a>
```cs
static class SerializerCache
{
    static ConcurrentDictionary<Type, Item> cache = new();

    public static Item GetSerializer(Type messageType)
    {
        return cache.GetOrAdd(messageType,
            type => new(
                new(type),
                new(type)
            ));
    }

    public class Item
    {
        public readonly Serializer<CompactBinaryWriter<OutputBuffer>> Serializer;
        public readonly Deserializer<CompactBinaryReader<InputBuffer>> Deserializer;

        public Item(
            Serializer<CompactBinaryWriter<OutputBuffer>> serializer,
            Deserializer<CompactBinaryReader<InputBuffer>> deserializer)
        {
            Serializer = serializer;
            Deserializer = deserializer;
        }
    }
}
```
<sup><a href='/src/Tests/Snippets/SerializerCache.cs#L5-L33' title='Snippet source file'>snippet source</a> | <a href='#snippet-serializercache' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Custom content key

When using [additional deserializers](https://docs.particular.net/nservicebus/serialization/#specifying-additional-deserializers) or transitioning between different versions of the same serializer it can be helpful to take explicit control over the content type a serializer passes to NServiceBus (to be used for the [ContentType header](https://docs.particular.net/nservicebus/messaging/headers#serialization-headers-nservicebus-contenttype)).

<!-- snippet: BondContentTypeKey -->
<a id='snippet-bondcontenttypekey'></a>
```cs
var serialization = configuration.UseSerialization<BondSerializer>();
serialization.ContentTypeKey("custom-key");
```
<sup><a href='/src/Tests/Snippets/Usage.cs#L44-L49' title='Snippet source file'>snippet source</a> | <a href='#snippet-bondcontenttypekey' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Icon

[Chemistry](https://thenounproject.com/term/Chemistry/107944/) designed by [Rafa Bosch](https://thenounproject.com/Externografico/) from [The Noun Project](https://thenounproject.com).
